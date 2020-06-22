using System;
using System.ComponentModel.DataAnnotations;

namespace BCF.MovieRental.Api.ViewModels
{
    /// <summary>
    /// Devolver Filme
    /// </summary>
    public class Rental2ViewModel
    {
        /// <summary>
        /// Id Locador
        /// </summary>
        [Required(ErrorMessage = "O Id do Locador é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Locador não cadastrado")]
        public int CustomerId { get; set; }

        /// <summary>
        /// Id Filme
        /// </summary>
        [Required(ErrorMessage = "O Id do Filme é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Filme não cadastrado")]
        public int MovieId { get; set; }
    }
}