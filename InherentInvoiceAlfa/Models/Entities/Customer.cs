using System;
using System.Collections.Generic;

namespace InherentInvoiceAlfa.Models.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Invoice = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string UserHashId { get; set; }

        public virtual ICollection<Invoice> Invoice { get; set; }
    }
}
