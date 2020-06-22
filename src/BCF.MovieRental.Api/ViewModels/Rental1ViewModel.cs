using System;
using System.ComponentModel.DataAnnotations;

namespace BCF.MovieRental.Api.ViewModels
{
    /// <summary>
    /// Locar Filme
    /// </summary>
    public class Rental1ViewModel
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

        /// <summary>
        /// Quantidade Dias Devolução
        /// </summary>                
        [Required(ErrorMessage = "A Quantidade Dias para Devolução é obrigatória")]
        [Range(1, 5, ErrorMessage = "A quantidade de dias para devolução deve ser entre {1} e {2} dias")]
        public int ReturnDays { get; set; }        
    }
}