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
    public class RentalRepository : Repository<Rental>, IRentalRepository
    {
        public RentalRepository(MovieRentalDbContext context) : base(context) { }

        public async Task<IEnumerable<Rental>> GetRentalsCustomer(int customerId)
        {
            return await Db.Rentals.AsNoTracking()
                .Include(p => p.Customer)
                .Include(p => p.Movie)                
                .Where(p => p.CustomerId == customerId)
                .OrderByDescending(p => p.RentalDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Rental>> GetRentalsMovie(int movieId)
        {
            return await Db.Rentals.AsNoTracking()
                .Include(p => p.Customer)
                .Include(p => p.Movie)
                .Where(p => p.MovieId == movieId)
                .OrderBy(p => p.Customer.Name).ThenByDescending(p => p.RentalDate)
                .ToListAsync();
        }

        public async Task<Rental> GetOpenRental(int customerId, int movieId)
        {            
            return await Db.Rentals
                .Include(p => p.Customer)
                .Include(p => p.Movie)
                .Where(p => p.CustomerId == customerId && p.MovieId == movieId && p.Status == RentalStatus.Open)
                .SingleOrDefaultAsync();
        }

        public async Task<Rental> GetRentalCustomerMovie(int rentalId)
        {
            return await Db.Rentals.AsNoTracking()
                .Include(p => p.Customer)
                .Include(p => p.Movie)
                .Where(p => p.Id == rentalId)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Rental>> GetRentalCustomerMovie()
        {
            return await Db.Rentals.AsNoTracking()
                .Include(p => p.Customer)
                .Include(p => p.Movie)
                .ToListAsync();
        }
    }
}