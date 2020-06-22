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
    /// Locação
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/rentals")]
    public class RentalsController : MainController
    {
        private readonly IRentalService _rentalService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        
        public RentalsController(
            IRentalService rentalService,
            IMapper mapper,
            INotifier notifier,
            ILogger<RentalsController> logger) : base(notifier)
        {
            _rentalService = rentalService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Registra Locação Filme
        /// </summary>
        /// <param name="rental1ViewModel">Locação</param>        
        [HttpPost]        
        public async Task<ActionResult<RentalViewModel>> RentMovie(Rental1ViewModel rental1ViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            RentalViewModel rentalViewModel =
                _mapper.Map<RentalViewModel>(
                    await _rentalService.RentMovie(_mapper.Map<Rental>(rental1ViewModel), rental1ViewModel.ReturnDays));            

            return CustomResponse(rentalViewModel);
        }

        /// <summary>
        /// Registra Devolução Filme
        /// </summary>
        /// <param name="rental2ViewModel">Locação</param>
        [HttpPut]
        public async Task<ActionResult<RentalViewModel>> ReturnMovie(Rental2ViewModel rental2ViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            RentalViewModel rentalViewModel = _mapper.Map<RentalViewModel>(
                await _rentalService.ReturnMovie(rental2ViewModel.CustomerId, rental2ViewModel.MovieId));

            return CustomResponse(rentalViewModel);
        }

        /// <summary>
        /// Consulta Locação
        /// </summary>
        /// <param name="rentalId">Id Locação</param>        
        [HttpGet("rentalId:int")]
        public async Task<ActionResult<RentalViewModel>> GetRental(int rentalId)
        {            
            var rental = _mapper.Map<RentalViewModel>(await _rentalService.GetRentalCustomerMovie(rentalId));

            if (rental == null)
            {
                return NotFound();
            }

            return rental;
        }

        /// <summary>
        /// Lista Locações
        /// </summary>        
        [HttpGet]
        public async Task<IEnumerable<RentalViewModel>> GetRental()
        {
            return _mapper.Map<IEnumerable<RentalViewModel>>(await _rentalService.GetRentalCustomerMovie());
        }

        /// <summary>
        /// Lista Locações Locador
        /// </summary>
        /// <param name="customerId">Id Locador</param>
        [HttpGet("customer/{customerId:int}")]
        public async Task<IEnumerable<RentalViewModel>> GetRentalsCustomer(int customerId)
        {
            return _mapper.Map<IEnumerable<RentalViewModel>>(await _rentalService.GetRentalsCustomer(customerId));
        }

        /// <summary>
        /// Lista Locações Filme
        /// </summary>
        /// <param name="movieId">Id Filme</param>
        [HttpGet("movie/{movieId:int}")]
        public async Task<IEnumerable<RentalViewModel>> GetRentalsMovie(int movieId)
        {
            return _mapper.Map<IEnumerable<RentalViewModel>>(await _rentalService.GetRentalsMovie(movieId));
        }
    }
}