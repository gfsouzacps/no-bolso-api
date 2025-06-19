using FluentValidation;
using NoBolso.Application.Commands.Carteiras;

namespace NoBolso.Application.Validators
{
    public class CriarCarteiraCommandValidator : AbstractValidator<CriarCarteiraCommand>
    {
        public CriarCarteiraCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome da carteira é obrigatório.")
                .MaximumLength(100)
                .WithMessage("Nome da carteira não pode ter mais de 100 caracteres.")
                .MinimumLength(2)
                .WithMessage("Nome da carteira deve ter pelo menos 2 caracteres.");
        }
    }
}