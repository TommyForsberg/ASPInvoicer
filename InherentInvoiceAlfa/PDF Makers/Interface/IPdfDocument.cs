using InherentInvoiceAlfa.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InherentInvoiceAlfa
{
    interface IPdfDocument
    {
        string CultureType { get; set; }
        string invoiceGloss { get; set; }
        string invoiceNumberGloss { get; set; }
        string dateGloss { get; set; }
        string expirationDateGloss { get; set; }
        string specifictionGloss { get; set; }
        string amountGloss { get; set; }
        string priceGloss { get; set; }
        string sumGloss { get; set; }
        string giroGloss { get; set; }
        string bankAccountGloss { get; set; }
        string ibanGloss { get; set; }
        string bicSwiftGloss { get; set; }
        string netGloss { get; set; }
        string vatGloss { get; set; }
        string totalGloss { get; set; }
        Invoice Model { get; set; }
        string LogoPath { get; set; }
        void SetAllStrings();
    }
}
