using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using BCF.MovieRental.Business.Models;

namespace BCF.MovieRental.Business.Interfaces
{
    public interface ICustomerService : IDisposable
    {
        Task<Customer> AddCustomer(Customer customer);
        Task<Customer> GetCustomer(int customerId);
        Task<IEnumerable<Customer>> GetCustomer();
    }
}