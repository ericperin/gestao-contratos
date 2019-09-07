using ContractManager.Domain.Commands.Contract;
using FluentValidation;

namespace ContractManager.Domain.Validations
{
    public class ContractValidation : AbstractValidator<ContractCommand>
    {
        public void ValidateQuantity()
        {
            RuleFor(p => p.QuantityTraded)
                .GreaterThan(0).WithMessage("The Quantity must be greater than zero");
        }
        public void ValidatePrice()
        {
            RuleFor(p => p.NegotiatedValue)
                .GreaterThan(0).WithMessage("The Price must be greater than zero");
        }
    }
}