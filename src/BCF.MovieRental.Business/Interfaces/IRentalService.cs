using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCF.MovieRental.Business.Models;

namespace BCF.MovieRental.Business.Interfaces
{
    public interface IRentalService : IDisposable
    {        
        Task<Rental> RentMovie(Rental rental, int returnDays);
        Task<Rental> ReturnMovie(int customerId, int movieId);
        Task<Rental> GetRental(int rentalId);
        Task<IEnumerable<Rental>> GetRental();
        Task<IEnumerable<Rental>> GetRentalsCustomer(int customerId);
        Task<IEnumerable<Rental>> GetRentalsMovie(int movieId);
        Task<Rental> GetOpenRental(int customerId, int movieId);
        Task<Rental> GetRentalCustomerMovie(int rentalId);
        Task<IEnumerable<Rental>> GetRentalCustomerMovie();
    }
}