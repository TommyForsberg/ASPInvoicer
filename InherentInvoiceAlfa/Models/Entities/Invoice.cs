using System;
using System.Collections.Generic;

namespace InherentInvoiceAlfa.Models.Entities
{
    public partial class Invoice
    {
        public Invoice()
        {
            Service = new HashSet<Service>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Vatpercentage { get; set; }
        public decimal Vatamount { get; set; }
        public int InvoiceNumber { get; set; }
        public int PaymentPeriod { get; set; }
        public decimal TotalWithoutVat { get; set; }
        public decimal Total { get; set; }
        public int? CustomerId { get; set; }
        public int? UserId { get; set; }

        public virtual ICollection<Service> Service { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual User User { get; set; }
    }
}
