using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InherentInvoiceAlfa.Models.Entities
{
    public partial class Invoice
    {
        public Invoice(Customer Customer, decimal VAT, List<Service> Services, User User, int InvoiceNumber, int PaymentPeriod)
        {
            this.User = User;
            this.Vatpercentage = VAT;
            this.InvoiceNumber = InvoiceNumber;
            this.Service = Services;
            this.Total = (ServicesTotal() * Vatpercentage) + ServicesTotal();
            this.Date = DateTime.Today.Date;
            this.Customer = Customer;
            this.Vatamount = VATCalculator();
            this.PaymentPeriod = PaymentPeriod;
            this.TotalWithoutVat = Total - Vatamount;
        }

        internal decimal ServicesTotal() //Calculates all services in List of services.
        {
            decimal ServicesTotal = 0;
            foreach (var service in Service)
            {
                ServicesTotal += service.ServiceTotalWithoutVat;
            }
            return ServicesTotal;
        }

        private decimal VATCalculator() //Returns total with VAT
        {
            return ServicesTotal() * Vatpercentage;
        }
    }
}
