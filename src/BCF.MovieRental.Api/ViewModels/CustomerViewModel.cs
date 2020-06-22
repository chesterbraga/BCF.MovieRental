using System.ComponentModel.DataAnnotations;

namespace BCF.MovieRental.Api.ViewModels
{
    /// <summary>
    /// Locador
    /// </summary>
    public class CustomerViewModel
    {
        /// <summary>
        /// Id Locador
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// E-mail Locador
        /// </summary>
        [Required(ErrorMessage = "O E-Mail é obrigatório")]
        [StringLength(100)]
        [RegularExpression(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$", ErrorMessage = "E-Mail inválido")]
        public string Email { get; set; }

        /// <summary>
        /// Nome Locador
        /// </summary>
        [Required(ErrorMessage = "O Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O Nome precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Name { get; set; }
    }
}