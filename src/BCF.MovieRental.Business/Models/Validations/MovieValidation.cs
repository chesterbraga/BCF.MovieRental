using FluentValidation;

namespace BCF.MovieRental.Business.Models.Validations
{
    public class MovieValidation : AbstractValidator<Movie>
    {
        public MovieValidation()
        {
            RuleFor(f => f.Title)
                .NotEmpty().WithMessage("O Título é obrigatório")
                .Length(3, 100)
                .WithMessage("O Título precisa ter entre {MinLength} e {MaxLength} caracteres");            
        }
    }
}