using System;
using System.ComponentModel.DataAnnotations;
using ContractManager.Domain.Enums;

namespace ContractManager.Domain.Entities
{
    public class Contract : EntityBase
    {
        public Contract() { } //TODO: Remove...
        public Contract(string clientName, ETypeOfContract type, decimal quantityTraded, decimal negotiatedValue, DateTime startedAt, int duration, string file, Guid createdBy)
        {
            this.ClientName = clientName;
            this.Type = type;
            this.QuantityTraded = quantityTraded;
            this.NegotiatedValue = negotiatedValue;
            this.StartedAt = startedAt;
            this.Duration = duration;
            this.File = file;
            this.CreatedBy = createdBy;

        }

        [Display(Name = "Cliente")]
        public string ClientName { get; private set; }

        [Display(Name = "Tipo")]
        public ETypeOfContract Type { get; private set; }

        [Display(Name = "Quantidade negociada")]
        public decimal QuantityTraded { get; private set; }

        [Display(Name = "Valor negociado")]
        public decimal NegotiatedValue { get; private set; }

        [Display(Name = "M�s/Ano do �nicio do contrato")]
        public DateTime StartedAt { get; private set; }

        [Display(Name = "Dura��o em meses do contrato")]
        public int Duration { get; private set; }

        [Display(Name = "Arquivo PDF com o contrato")]
        public string File { get; private set; }

        public Guid CreatedBy { get; private set; }
    }
}