using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BCF.MovieRental.Api.ViewModels;
using BCF.MovieRental.Business.Interfaces;
using BCF.MovieRental.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BCF.MovieRental.Api.Controllers.V1
{
    /// <summary>
    /// Locadores
    /// </summary>    
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/customers")]    
    public class CustomersController : MainController
    {
        private readonly ICustomerService _customerService;        
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CustomersController(
            ICustomerService customerService,            
            IMapper mapper,
            INotifier notifier,
            ILogger<CustomersController> logger) : base(notifier)
        {            
            _customerService = customerService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Cadastra Locador
        /// </summary>
        /// <param name="customer1ViewModel">Locador</param>        
        [HttpPost]
        public async Task<ActionResult<CustomerViewModel>> AddCustomer(Customer1ViewModel customer1ViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            CustomerViewModel customerViewModel =
                _mapper.Map<CustomerViewModel>(
                    await _customerService.AddCustomer(_mapper.Map<Customer>(customer1ViewModel)));            
            
            return CustomResponse(customerViewModel);
        }

        /// <summary>
        /// Consulta Locador
        /// </summary>
        /// <param name="customerId">Id ocador</param>
        [HttpGet("{customerId:int}")]
        public async Task<ActionResult<CustomerViewModel>> GetCustomer(int customerId)
        {
            var customer = _mapper.Map<CustomerViewModel>(await _customerService.GetCustomer(customerId));

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        /// <summary>
        /// Lista Locadores
        /// </summary>        
        [HttpGet]
        public async Task<IEnumerable<CustomerViewModel>> GetCustomer()
        {
            return _mapper.Map<IEnumerable<CustomerViewModel>>(await _customerService.GetCustomer());
        }        
    }
}