using System.ComponentModel.DataAnnotations;
using BCF.MovieRental.Business.Models.Enums;

namespace BCF.MovieRental.Api.ViewModels
{
    /// <summary>
    /// Filme
    /// </summary>
    public class MovieViewModel
    {
        /// <summary>
        /// Id Filme
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Título Filme
        /// </summary>
        [Required(ErrorMessage = "O Título é obrigatório")]
        [StringLength(100, ErrorMessage = "O Título precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Title { get; set; }

        /// <summary>
        /// Status Filme (1 - Alugado, 2 - Disponível) 
        /// </summary>        
        public MovieStatus Status { get; set; } 
    }
}