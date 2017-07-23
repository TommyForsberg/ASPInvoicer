using System;
using System.Collections.Generic;

namespace InherentInvoiceAlfa.Models.Entities
{
    public partial class Service
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Amount { get; set; }
        public decimal Price { get; set; }
        public decimal ServiceTotalWithoutVat { get; set; }
        public int? InvoiceId { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
