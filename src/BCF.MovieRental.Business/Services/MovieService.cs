using System.Collections.Generic;
using System.Threading.Tasks;
using BCF.MovieRental.Business.Interfaces;
using BCF.MovieRental.Business.Models;
using BCF.MovieRental.Business.Models.Enums;
using BCF.MovieRental.Business.Models.Validations;

namespace BCF.MovieRental.Business.Services
{
    public class MovieService : BaseService, IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        private bool Validate(Movie movie)
        {
            if (!IsValid(new MovieValidation(), movie))
            {
                return false;
            }
            return true;
        }

        public MovieService(
            IMovieRepository movieRepository,
            INotifier notifier) : base(notifier)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Movie> AddMovie(Movie movie)
        {
            if (Validate(movie))
            {
                movie.Status = MovieStatus.Available;
                await _movieRepository.Add(movie);                
            }
            
            return movie;
        }

        public async Task<bool> UpdateMovie(Movie movie)
        {
            if (!Validate(movie))
            {
                return false;
            }

            await _movieRepository.Update(movie);
            return true;
        }

        public async Task<Movie> GetMovie(int movieId)
        {
            return await _movieRepository.Get(movieId);
        }

        public async Task<IEnumerable<Movie>> GetMovie()
        {
            return await _movieRepository.Get();
        }
        
        public async Task<IEnumerable<Movie>> GetAvailableMovies()
        {
            return await _movieRepository.GetAvailableMovies();         
        }
        public async Task<IEnumerable<Movie>> GetRentalMovies()
        {
            return await _movieRepository.GetRentalMovies();            
        }

        public void Dispose()
        {
            _movieRepository?.Dispose();
        }
    }
}