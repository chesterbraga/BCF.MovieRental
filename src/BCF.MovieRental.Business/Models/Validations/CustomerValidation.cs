using FluentValidation;
using BCF.MovieRental.Business.Models.Validations.Documents;

namespace BCF.MovieRental.Business.Models.Validations
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage("O Nome é obrigatório")
                .Length(3, 100)
                .WithMessage("O Nome precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.Email)
                .NotEmpty().WithMessage("O E-Mail é obrigatório");
            
            When(f => !string.IsNullOrEmpty(f.Email), () =>
            {
                RuleFor(f => EmailValidation.Validate(f.Email)).Equal(true)
                    .WithMessage("O E-Mail é inválido.");
            });
        }
    }
}