using System.Threading.Tasks;
using System.Collections.Generic;
using BCF.MovieRental.Business.Models;

namespace BCF.MovieRental.Business.Interfaces
{
    public interface IMovieService
    {
        Task<Movie> AddMovie(Movie movie);
        Task<bool> UpdateMovie(Movie movie);
        Task<Movie> GetMovie(int movieId);
        Task<IEnumerable<Movie>> GetMovie();
        Task<IEnumerable<Movie>> GetAvailableMovies();
        Task<IEnumerable<Movie>> GetRentalMovies();        
    }
}