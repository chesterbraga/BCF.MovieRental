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
    /// Filmes
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/movies")]
    public class MoviesController : MainController
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public MoviesController(
            IMovieService movieService,
            IMapper mapper,
            INotifier notifier,
            ILogger<MoviesController> logger) : base(notifier)
        {
            _movieService = movieService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Cadastra Filme
        /// </summary>
        /// <param name="movie1ViewModel">Filme</param>
        [HttpPost]
        public async Task<ActionResult<MovieViewModel>> AddMovie(Movie1ViewModel movie1ViewModel)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            MovieViewModel movieViewModel =
                _mapper.Map<MovieViewModel>(
                    await _movieService.AddMovie(_mapper.Map<Movie>(movie1ViewModel)));

            return CustomResponse(movieViewModel);
        }

        /// <summary>
        /// Consulta Filme
        /// </summary>
        /// <param name="movieId">Id Filme</param>        
        [HttpGet("{movieId:int}")]
        public async Task<ActionResult<MovieViewModel>> GetMovie(int movieId)
        {
            var movie = _mapper.Map<MovieViewModel>(await _movieService.GetMovie(movieId));

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        /// <summary>
        /// Lista Filmes
        /// </summary>        
        [HttpGet]
        public async Task<IEnumerable<MovieViewModel>> GetMovie()
        {
            return _mapper.Map<IEnumerable<MovieViewModel>>(await _movieService.GetMovie());
        }

        /// <summary>
        /// Lista Filmes Disponíveis
        /// </summary>        
        [HttpGet("availables")]
        public async Task<IEnumerable<MovieViewModel>> GetAvailableMovies()
        {
            return _mapper.Map<IEnumerable<MovieViewModel>>(await _movieService.GetAvailableMovies());
        }

        /// <summary>
        /// Lista Filmes Alugados
        /// </summary>        
        [HttpGet("rental")]
        public async Task<IEnumerable<MovieViewModel>> GetRentalMovies()
        {
            return _mapper.Map<IEnumerable<MovieViewModel>>(await _movieService.GetRentalMovies());
        }        
    }
}