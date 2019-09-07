using ContractManager.Domain.Validations;
using System;

namespace ContractManager.Domain.Commands.Contract
{
    public class DeleteContractCommand : ContractCommand
    {
        public DeleteContractCommand(Guid id) => Id = id;

        public override bool IsValid()
        {
            return true;
        }
    }
}
