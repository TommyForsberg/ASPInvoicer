using System;
using System.Collections.Generic;

namespace InherentInvoiceAlfa.Models.Entities
{
    public partial class User
    {
        public User()
        {
            Invoice = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HashId { get; set; }
        public string Street { get; set; }
        public string Giro { get; set; }
        public string BankAccount { get; set; }
        public string Iban { get; set; }
        public string BicSwift { get; set; }
        public string PostalAddress { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Invoice> Invoice { get; set; }
    }
}
