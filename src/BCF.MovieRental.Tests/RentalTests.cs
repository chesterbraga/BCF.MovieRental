using Microsoft.VisualStudio.TestTools.UnitTesting;
using BCF.MovieRental.Business.Interfaces;
using BCF.MovieRental.Business.Models;
using BCF.MovieRental.Business.Services;
using BCF.MovieRental.Data.Repository;
using BCF.MovieRental.Business.Notifications;
using BCF.MovieRental.Data.Context;
using Microsoft.EntityFrameworkCore;
using BCF.MovieRental.Business.Models.Enums;

namespace BCF.MovieRental.Tests
{
    [TestClass]
    public class RentalTests
    {
        private IRentalService _service;
        private IMovieService _movieService;
        private ICustomerService _customerService;
        private MovieRentalDbContext dbctx;


        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<MovieRentalDbContext>().Options;
            dbctx = new MovieRentalDbContext(options);

            var rentalRepository = new RentalRepository(dbctx);
            var movieRepository = new MovieRepository(dbctx);
            var customerRepository = new CustomerRepository(dbctx);

            var notifier = new Notifier();

            _movieService = new MovieService(movieRepository, notifier);
            _customerService = new CustomerService(customerRepository, notifier);

            _service = new RentalService(rentalRepository, _movieService, _customerService, notifier);
        }

        [TestCleanup]
        public void Cleanup()
        {
            dbctx.Database.EnsureDeleted();
            dbctx?.Dispose();
        }

        private Customer AddCustomer()
        {
            Customer customer = new Customer
            {
                Email = "xxx@xxx.com",
                Name = "Locador X"
            };

            return _customerService.AddCustomer(customer).Result;
        }

        private Movie AddMovie()
        {
            Movie movie = new Movie
            {
                Title = "Titulo Filme"
            };

            return _movieService.AddMovie(movie).Result;
        }


        [TestCategory("Locar Filme")]
        [TestMethod]
        public void TestRentMovie()
        {
            Customer customer = AddCustomer();
            Movie movie = AddMovie();

            Rental rental = new Rental
            {
                CustomerId = customer.Id,
                MovieId = movie.Id
            };

            rental = _service.RentMovie(rental, 1).Result;
            rental = _service.GetRental(rental.Id).Result;

            Assert.AreEqual(1, rental.Id, "Id");
            Assert.AreEqual(1, rental.CustomerId, "Id Locador");
            Assert.AreEqual(1, rental.MovieId, "Id Filme");
            Assert.AreEqual(RentalStatus.Open, rental.Status, "Status da Locação");            

            movie = _movieService.GetMovie(movie.Id).Result;
            Assert.AreEqual(MovieStatus.Rented, movie.Status, "Status Filme");
        }

        [TestCategory("Devolver Filme")]
        [TestMethod]
        public void TestReturnMovie()
        {
            Customer customer = AddCustomer();
            Movie movie = AddMovie();

            Rental rental = new Rental
            {
                CustomerId = customer.Id,
                MovieId = movie.Id
            };

            rental = _service.RentMovie(rental, 1).Result;
            rental = _service.GetRental(rental.Id).Result;

            rental = _service.ReturnMovie(rental.CustomerId, rental.MovieId).Result;
            rental = _service.GetRental(rental.Id).Result;

            Assert.AreEqual(RentalStatus.Closed, rental.Status, "Status Locação");            

            Movie movie1 = _movieService.GetMovie(movie.Id).Result;
            Assert.AreEqual(MovieStatus.Available, movie1.Status, "Status Filme");
        }
    }
}