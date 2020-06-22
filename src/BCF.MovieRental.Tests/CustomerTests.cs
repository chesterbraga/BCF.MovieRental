using Microsoft.VisualStudio.TestTools.UnitTesting;
using BCF.MovieRental.Business.Interfaces;
using BCF.MovieRental.Business.Models;
using BCF.MovieRental.Business.Services;
using BCF.MovieRental.Data.Repository;
using BCF.MovieRental.Business.Notifications;
using BCF.MovieRental.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BCF.MovieRental.Tests
{
    [TestClass]
    public class CustomerTests
    {
        private ICustomerService _service;
        private MovieRentalDbContext dbctx;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<MovieRentalDbContext>().Options;
            dbctx = new MovieRentalDbContext(options);

            _service = new CustomerService(new CustomerRepository(dbctx), new Notifier());
        }

        [TestCleanup]
        public void Cleanup()
        {
            dbctx.Database.EnsureDeleted();            
            dbctx?.Dispose();
        }

        [TestCategory("Cadastro Locador")]
        [TestMethod]
        public void TestAddCustomer()
        {
            Customer customer = new Customer
            {
                Email = "xxx@xxx.com",
                Name = "Locador X"
            };

            customer = _service.AddCustomer(customer).Result;
            customer = _service.GetCustomer(customer.Id).Result;

            Assert.AreEqual(1, customer.Id, "Id");
            Assert.AreEqual("xxx@xxx.com", customer.Email, "E-mail");
            Assert.AreEqual("Locador X", customer.Name, "Nome");
        }       
    }
}