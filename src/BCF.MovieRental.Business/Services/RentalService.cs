using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCF.MovieRental.Business.Enums;
using BCF.MovieRental.Business.Interfaces;
using BCF.MovieRental.Business.Models;
using BCF.MovieRental.Business.Models.Enums;
using BCF.MovieRental.Business.Models.Validations;

namespace BCF.MovieRental.Business.Services
{
    public class RentalService : BaseService, IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMovieService _movieService;
        private readonly ICustomerService _customerService;

        private async Task<bool> ValidateRental(Rental rental)
        {            
            bool valid = false;

            if (IsValid(new RentalValidation(), rental))
            {
                valid = true;

                Customer customer = await _customerService.GetCustomer(rental.CustomerId);
                if (customer == null)
                {
                    Notify("Este Locador não está cadastrado.");
                    valid = false;
                }

                Movie movie = await _movieService.GetMovie(rental.MovieId);
                if (movie == null)
                {
                    Notify("Este filme não faz parte do nosso catálogo.");
                    valid = false;                   
                }
                else
                {
                    if (movie.Status == MovieStatus.Rented)
                    {
                        Notify("Este filme já está locado.");
                        valid = false;
                    }
                }
            };

            return valid;
        }

        private bool ValidateReturn(Rental rental)
        {
            bool valid = true;

            if (rental == null)
            {
                Notify("Registro de Locação inexistente ou o filme já foi devolvido.");
                valid = false;
            }            

            return valid;
        }

        public RentalService(
            IRentalRepository rentalRepository,            
            IMovieService movieService,
            ICustomerService customerService,
            INotifier notifier) : base(notifier)
        {
            _rentalRepository = rentalRepository;            
            _movieService = movieService;
            _customerService = customerService;
        }

        public async Task<Rental> RentMovie(Rental rental, int returnDays)
        {
            if (ValidateRental(rental).Result)
            {
                rental.RentalDate = DateTime.Now;
                rental.ExpectedReturnDate = rental.RentalDate.AddDays(returnDays);
                rental.Status = RentalStatus.Open;                

                await _rentalRepository.Add(rental);

                Movie movie = await _movieService.GetMovie(rental.MovieId);
                movie.Status = MovieStatus.Rented;
                await _movieService.UpdateMovie(movie);                
            }

            return rental;
        }

        public async Task<Rental> ReturnMovie(int customerId, int movieId)
        {
            Rental rental = await GetOpenRental(customerId, movieId);

            if (ValidateReturn(rental))
            {
                if (DateTime.Now > rental.ExpectedReturnDate)
                {
                    Notify("Devolução com atraso.", MessageType.Alert);
                }

                rental.ReturnDate = DateTime.Now;
                rental.Status = RentalStatus.Closed;

                await _rentalRepository.Update(rental);

                Movie movie = await _movieService.GetMovie(rental.MovieId);
                movie.Status = MovieStatus.Available;
                await _movieService.UpdateMovie(movie);
            }

            return rental;
        }

        public async Task<Rental> GetRental(int rentalId)
        {
            return await _rentalRepository.Get(rentalId);
        }

        public async Task<IEnumerable<Rental>> GetRental()
        {
            return await _rentalRepository.Get();
        }

        public async Task<IEnumerable<Rental>> GetRentalsCustomer(int customerId)
        {
            return await _rentalRepository.GetRentalsCustomer(customerId);
        }

        public async Task<IEnumerable<Rental>> GetRentalsMovie(int movieId)
        {
            return await _rentalRepository.GetRentalsMovie(movieId);
        }

        public async Task<Rental> GetOpenRental(int customerId, int movieId)
        {
            return await _rentalRepository.GetOpenRental(customerId, movieId);
        }

        public async Task<Rental> GetRentalCustomerMovie(int rentalId)
        {
            return await _rentalRepository.GetRentalCustomerMovie(rentalId);
        }

        public async Task<IEnumerable<Rental>> GetRentalCustomerMovie()
        {
            return await _rentalRepository.GetRentalCustomerMovie();
        }

        public void Dispose()
        {
            _rentalRepository?.Dispose();
        }
    }
}