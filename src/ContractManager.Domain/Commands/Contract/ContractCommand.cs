using ContractManager.Domain.Interfaces;
using FluentValidation.Results;
using MediatR;
using ContractManager.Domain.Enums;
using System;
using ContractManager.Domain.Validations;

namespace ContractManager.Domain.Commands.Contract
{
    public class ContractCommand : IRequest<Result>, ICommand
    {
        public string ClientName { get; set; }
        public ETypeOfContract Type { get; set; }
        public decimal QuantityTraded { get; set; }
        public decimal NegotiatedValue { get; set; }
        public DateTime StartedAt { get; set; }
        public int Duration { get; set; }
        public string File { get; set; }

        public Guid CreatedBy { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public virtual bool IsValid()
        {
            var validation = new ContractValidation();
            validation.ValidateQuantity();
            validation.ValidatePrice();

            ValidationResult = validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
