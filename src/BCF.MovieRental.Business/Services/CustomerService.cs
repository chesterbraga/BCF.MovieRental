using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCF.MovieRental.Business.Interfaces;
using BCF.MovieRental.Business.Models;
using BCF.MovieRental.Business.Models.Validations;

namespace BCF.MovieRental.Business.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        private bool Validate(Customer customer)
        {
            bool valid = false;

            if (IsValid(new CustomerValidation(), customer))
            {
                valid = true;

                if (_customerRepository.Get(f => f.Email == customer.Email).Result.Any())
                {
                    Notify("Já existe um locador com o E-Mail informado.");
                    valid = false;
                }

            }

            return valid;
        }

        public CustomerService(
            ICustomerRepository customerRepository,
            INotifier notifier) : base(notifier)
        {
            _customerRepository = customerRepository;            
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            if (Validate(customer))
            {
                await _customerRepository.Add(customer);
            }

            return customer;
        }

        public async Task<Customer> GetCustomer(int customerId)
        {
            return await _customerRepository.Get(customerId);
        }
        
        public async Task<IEnumerable<Customer>> GetCustomer()        
        {
            return await _customerRepository.Get();
        }

        public void Dispose()
        {
            _customerRepository?.Dispose();
        }
    }
}