using ContractManager.Domain.Enums;
using System;

namespace ContractManager.Domain.Commands.Contract
{
    public class FormContractCommand : ContractCommand
    {
        public FormContractCommand() { }
        public FormContractCommand(string clientName, ETypeOfContract type, decimal quantityTraded, decimal negotiatedValue, DateTime startedAt, int duration, string file, Guid createdBy)
        {
            ClientName = clientName;
            Type = type;
            QuantityTraded = quantityTraded;
            NegotiatedValue = negotiatedValue;
            StartedAt = startedAt;
            Duration = duration;
            File = file;
            CreatedBy = createdBy;
        }
    }
}
