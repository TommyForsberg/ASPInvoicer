using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InherentInvoiceAlfa.Models.ViewModels
{
    public class EditUserVM
    {
        [Display(Name = "Förnamn:", Prompt = "Förnamn")]
        [MaxLength(50, ErrorMessage = "Max 50 tecken!")]
        public string FirstName { get; set; }
        [Display(Name = "Efternamn:", Prompt = "Efternamn")]
        [MaxLength(50, ErrorMessage = "Max 50 tecken!")]
        public string LastName { get; set; }
        [Display(Name = "Gatuadress:", Prompt = "Gatuadress")]
        [MaxLength(50, ErrorMessage = "Max 50 tecken!")]
        public string Street { get; set; }
        [Display(Name = "Giro:", Prompt = "Giro")]
        [MaxLength(50, ErrorMessage = "Max 50 tecken!")]
        public string Giro { get; set; }
        [Display(Name = "Bankkonto:", Prompt = "Bankkonto")]
        [MaxLength(50, ErrorMessage = "Max 50 tecken!")]
        public string BankAccount { get; set; }
        [Display(Name = "Iban:", Prompt = "Iban")]
        [MaxLength(50, ErrorMessage = "Max 50 tecken!")]
        public string Iban { get; set; }
        [Display(Name = "Bic/Swift:", Prompt = "Bic/swift")]
        [MaxLength(50, ErrorMessage = "Max 50 tecken!")]
        public string BicSwift { get; set; }
        [Display(Name = "Postadress:", Prompt = "Postadress")]
        [MaxLength(50, ErrorMessage = "Max 50 tecken!")]
        public string PostalAddress { get; set; }
        [Display(Name = "Stad:", Prompt = "Stad")]
        [MaxLength(50, ErrorMessage = "Max 50 tecken!")]
        public string City { get; set; }
        [Display(Name = "E-mail:", Prompt = "E-mail")]
        [MaxLength(50, ErrorMessage = "Max 50 tecken!")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        [Display(Name = "Telefon:", Prompt = "Telefon")]
        [MaxLength(50, ErrorMessage = "Max 50 tecken!")]
        public string Phone { get; set; }
    }
}
