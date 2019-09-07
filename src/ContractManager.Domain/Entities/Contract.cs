using System;
using ContractManager.Domain.Enums;

namespace ContractManager.Domain.Entities
{
    public class Contract : EntityBase
    {
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
        public string ClientName { get; private set; }
        public ETypeOfContract Type { get; private set; }
        public decimal QuantityTraded { get; private set; }
        public decimal NegotiatedValue { get; private set; }
        public DateTime StartedAt { get; private set; }
        public int Duration { get; private set; }
        public string File { get; private set; }

        public Guid CreatedBy { get; private set; }

        public Contract()
        {

        }

        public void SetCreatedBy(Guid id){
            CreatedBy = CreatedBy;
        }
    }
}