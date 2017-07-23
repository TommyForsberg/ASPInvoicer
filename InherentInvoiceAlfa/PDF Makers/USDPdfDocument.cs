using InherentInvoiceAlfa.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InherentInvoiceAlfa
{
    class USDPdfDocument : SEKPdfDocument, IPdfDocument
    {
        public USDPdfDocument(Invoice Model) : base(Model)
        {
        }
        public override void SetAllStrings()
        {
            CultureType = "en-US";
            invoiceGloss = "Invoice";
            invoiceNumberGloss = "Invoice number";
            dateGloss = "Date";
            expirationDateGloss = "Expiration date";
            specifictionGloss = "Specification";
            amountGloss = "Amount";
            priceGloss = "Price";
            sumGloss = "Sum";
            giroGloss = "Giro";
            bankAccountGloss = "Bank account";
            ibanGloss = "IBAN";
            bicSwiftGloss = "BIC/SWIFT";
            netGloss = "NET";
            vatGloss = "VAT";
            totalGloss = "TOTAL";
        }
    }
}
