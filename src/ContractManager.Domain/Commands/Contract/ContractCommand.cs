using ContractManager.Domain.Interfaces;
using MediatR;
using ContractManager.Domain.Enums;
using System;
using ContractManager.Domain.Validations;
using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ContractManager.Domain.Commands.Contract
{
    public class ContractCommand : IRequest<Result>, ICommand
    {
        public Guid Id { get; set; }

        [Display(Name = "Cliente")]
        public string ClientName { get; set; }

        [Display(Name = "Tipo")]
        public ETypeOfContract Type { get; set; }

        [Display(Name = "Quantidade negociada")]
        public decimal QuantityTraded { get; set; }

        [Display(Name = "Valor negociado")]
        public decimal NegotiatedValue { get; set; }

        [Display(Name = "Mês/Ano do ínicio do contrato")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/yyyy}")]
        public DateTime StartedAt { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/yyyy}")]
        public DateTime FinishedAt => StartedAt.AddMonths(Duration);

        [Display(Name = "Duração em meses do contrato")]
        public int Duration { get; set; }

        [Display(Name = "Arquivo PDF com o contrato")]
        public byte[] File { get; set; }

        public Guid CreatedBy { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public virtual bool IsValid()
        {
            var validation = new ContractValidation();
            validation.ValidateDomain();

            ValidationResult = validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}