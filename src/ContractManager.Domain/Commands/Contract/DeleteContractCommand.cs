using ContractManager.Domain.Validations;
using System;

namespace ContractManager.Domain.Commands.Contract
{
    public class DeleteContractCommand : ContractCommand
    {
        public DeleteContractCommand(Guid id) =>
            Id = id;

        public override bool IsValid()
        {
            var validation = new ContractValidation();
            validation.ValidatePrice();

            ValidationResult = validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
