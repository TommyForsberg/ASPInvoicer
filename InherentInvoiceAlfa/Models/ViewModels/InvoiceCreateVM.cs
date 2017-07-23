using InherentInvoiceAlfa.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InherentInvoiceAlfa.Models.ViewModels
{
    public class InvoiceCreateVM
    {
        public List<SelectListItem> VatValues { get; set; }
        [Required]
        public int SelectedVat { get; set; }
        public ServiceVM[] InvoiceServices { get; set; }
        [HiddenInput(DisplayValue = false)]
        [Required]
        public string JsonString { get; set; }
        public ServiceVM CurrentService { get; set; }
        [Required]
        public int SelectedCustomer { get; set; }

       public List<SelectListItem> Customers { get; set; }
        public InvoiceCreateVM()
        {
            VatValues = new List<SelectListItem>
            {
                new SelectListItem{Text="25%", Value="25"},
                new SelectListItem{Text="12%", Value="12"},
                new SelectListItem{Text="6%", Value="6"}
            };
        }
    }
}
