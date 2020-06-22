using System;
using System.ComponentModel.DataAnnotations;
using BCF.MovieRental.Business.Models.Enums;

namespace BCF.MovieRental.Api.ViewModels
{
    /// <summary>
    /// Locação
    /// </summary>
    public class RentalViewModel
    {
        /// <summary>
        /// Id Locação
        /// </summary>
        [Key]
        public int Id { get; set; }

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
        /// Data Locação
        /// </summary>
        public DateTime RentalDate { get; set; }

        /// <summary>
        /// Data Prevista Devolução
        /// </summary>
        public DateTime ExpectedReturnDate { get; set; }

        /// <summary>
        /// Data Efetiva Devolução
        /// </summary>
        public DateTime ReturnDate { get; set; }

        /// <summary>
        /// Status Locação (1 - Aberto, 2 - Fechado) 
        /// </summary>
        public RentalStatus Status { get; set; }

        /// <summary>
        /// Mome Locador
        /// </summary>
        [ScaffoldColumn(false)]
        public string CustomerName { get; set; }

        /// <summary>
        /// Nome Filme
        /// </summary>
        [ScaffoldColumn(false)]
        public string MovieName { get; set; }
    }
}