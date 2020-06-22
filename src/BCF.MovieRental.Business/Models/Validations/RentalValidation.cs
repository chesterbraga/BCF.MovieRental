using FluentValidation;

namespace BCF.MovieRental.Business.Models.Validations
{
    public class RentalValidation : AbstractValidator<Rental>
    {
        public RentalValidation()
        {
            RuleFor(f => f.CustomerId)
                .NotEmpty().WithMessage("O Id do Locador é obrigatório");

            RuleFor(f => f.MovieId)
                .NotEmpty().WithMessage("O Id do Filme é obrigatório");                
        }
    }
}