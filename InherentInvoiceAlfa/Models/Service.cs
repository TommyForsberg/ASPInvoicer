using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace InherentInvoiceAlfa.Models.Entities
{
    public partial class Service
    {
        public Service(string Label, string Amount, decimal Price)
        {
            this.Label = Label;
            this.Amount = Amount;
            this.Price = Price;
            ServiceTotalWithoutVat = ServiceTotalCalculator(Amount, Price);
        }
        private decimal ServiceTotalCalculator(string amount, decimal price) //Since all chars are allowed in amount, it need to be parsed.
        {
            decimal value;
            if (decimal.TryParse(amount, out value))
                return price * value;
            else
                return 1 * price;
        }
    }
}
