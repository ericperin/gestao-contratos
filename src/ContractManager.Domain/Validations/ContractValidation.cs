using ContractManager.Domain.Commands.Contract;
using FluentValidation;

namespace ContractManager.Domain.Validations
{
    public class ContractValidation : AbstractValidator<ContractCommand>
    {
        public void ValidateDomain()
        {
            RuleFor(p => p.ClientName).NotEmpty().NotNull().WithMessage("O nome do cliente é obrigatório");
            RuleFor(p => p.QuantityTraded).GreaterThan(0).WithMessage("A quantidade deve ser maior que 0");
            RuleFor(p => p.NegotiatedValue).GreaterThan(0).WithMessage("O preço deve ser maior que 0");
            RuleFor(p => p.File).NotEmpty().WithMessage("O arquivo é obrigatório");
        }
    }
}