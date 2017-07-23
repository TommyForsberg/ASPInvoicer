using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InherentInvoiceAlfa.Models.ViewModels
{
    public class CreateCustomerVM
    {
        [Display(Name = "Namn:", Prompt = "Namn")]
        [Required(ErrorMessage = "*Får inte vara tomt")]
        [MaxLength(50, ErrorMessage = "Använd högst 50 tecken!")]
        public string Name { get; set; }
        [Display(Name = "Adress:", Prompt = "Adress")]
        [Required(ErrorMessage = "*Får inte vara tomt")]
        [MaxLength(50, ErrorMessage = "Använd högst 50 tecken!")]
        public string Adress { get; set; }
    }
}
