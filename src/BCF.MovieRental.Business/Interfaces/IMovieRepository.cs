using BCF.MovieRental.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BCF.MovieRental.Business.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetAvailableMovies();
        Task<IEnumerable<Movie>> GetRentalMovies();
    }
}