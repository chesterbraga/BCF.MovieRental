using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCF.MovieRental.Business.Interfaces;
using BCF.MovieRental.Business.Models;
using BCF.MovieRental.Data.Context;
using Microsoft.EntityFrameworkCore;
using BCF.MovieRental.Business.Models.Enums;

namespace BCF.MovieRental.Data.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieRentalDbContext context) : base(context) { }

        public async Task<IEnumerable<Movie>> GetAvailableMovies()
        {
            return await Db.Movies.AsNoTracking().Where(p => p.Status == MovieStatus.Available).ToListAsync();            
        }

        public async Task<IEnumerable<Movie>> GetRentalMovies()
        {
            return await Db.Movies.AsNoTracking().Where(p => p.Status == MovieStatus.Rented).ToListAsync();            
        }        
    }
}