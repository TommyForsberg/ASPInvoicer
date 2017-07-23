using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InherentInvoiceAlfa.Models.ViewModels
{
    public class CreateUserVM
    {
        // User name
        [Display(Name = "Name:", Prompt = "Name")]
        [Required(ErrorMessage = "*required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed")]
        public string UserName { get; set; }
        // Email
        [Display(Name = "Email Address:", Prompt = "you@yours.com")]
        [Required(ErrorMessage = "*required")]
        [EmailAddress]
        public string Email { get; set; }
        public string HashId { get; set; }
        // Password
        [Display(Name = "Password:")]
        [Required(ErrorMessage = "*required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
