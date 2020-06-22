using System.ComponentModel.DataAnnotations;

namespace BCF.MovieRental.Api.ViewModels
{
    /// <summary>
    /// Filme
    /// </summary>
    public class Movie1ViewModel
    {        
        /// <summary>
        /// Título Filme
        /// </summary>
        [Required(ErrorMessage = "O Título é obrigatório")]
        [StringLength(100, ErrorMessage = "O Título precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Title { get; set; }
    }
}