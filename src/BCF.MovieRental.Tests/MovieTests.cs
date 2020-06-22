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
    public class MovieTests
    {
        private IMovieService _service;
        private MovieRentalDbContext dbctx;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<MovieRentalDbContext>().Options;
            dbctx = new MovieRentalDbContext(options);

            _service = new MovieService(new MovieRepository(dbctx), new Notifier());
        }

        [TestCleanup]
        public void Cleanup()
        {
            dbctx.Database.EnsureDeleted();            
            dbctx?.Dispose();
        }

        [TestCategory("Cadastro Filme")]
        [TestMethod]
        public void TestAddMovie()
        {
            Movie movie = new Movie
            {
                Title = "Titulo Filme"
            };

            movie = _service.AddMovie(movie).Result;
            movie = _service.GetMovie(movie.Id).Result;

            Assert.AreEqual(1, movie.Id, "Id");
            Assert.AreEqual("Titulo Filme", movie.Title, "Título");
            Assert.AreEqual(MovieStatus.Available, movie.Status, "Status");
        }
    }
}