using BCF.MovieRental.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BCF.MovieRental.Business.Interfaces
{
    public interface IRentalRepository : IRepository<Rental>
    {
        Task<IEnumerable<Rental>> GetRentalsCustomer(int customerId);
        Task<IEnumerable<Rental>> GetRentalsMovie(int movieId);
        Task<Rental> GetOpenRental(int customerId, int movieId);
        Task<Rental> GetRentalCustomerMovie(int rentalId);
        Task<IEnumerable<Rental>> GetRentalCustomerMovie();
    }
}