using System;
using System.ComponentModel.DataAnnotations;
using ContractManager.Domain.Enums;

namespace ContractManager.Domain.Entities
{
    public class Contract : EntityBase
    {
        public Contract(string clientName, ETypeOfContract type, decimal quantityTraded, decimal negotiatedValue, DateTime startedAt, int duration, byte[] file, Guid createdBy)
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

        [Display(Name = "Cliente")]
        public string ClientName { get; private set; }

        [Display(Name = "Tipo")]
        public ETypeOfContract Type { get; private set; }

        [Display(Name = "Quantidade negociada")]
        public decimal QuantityTraded { get; private set; }

        [Display(Name = "Valor negociado")]
        public decimal NegotiatedValue { get; private set; }

        [Display(Name = "Mês/Ano do ínicio do contrato")]
        public DateTime StartedAt { get; private set; }

        [Display(Name = "Duração em meses do contrato")]
        public int Duration { get; private set; }

        [Display(Name = "Arquivo PDF com o contrato")]
        public byte[] File { get; private set; }

        public Guid CreatedBy { get; private set; }

        public DateTime FinishedAt => StartedAt.AddMonths(Duration);

        public void Update(string clientName, ETypeOfContract type, decimal quantityTraded, decimal negotiatedValue, DateTime startedAt, int duration, byte[] file)
        {
            ClientName = clientName;
            Type = type;
            QuantityTraded = quantityTraded;
            NegotiatedValue = negotiatedValue;
            StartedAt = startedAt;
            Duration = duration;
            File = file;
        }
    }
}