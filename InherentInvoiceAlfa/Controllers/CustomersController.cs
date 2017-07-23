using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using InherentInvoiceAlfa.Models.Entities;
using InherentInvoiceAlfa.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InherentInvoiceAlfa.Controllers
{
    public class CustomersController : Controller
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        IdentityDbContext identity;
        InherentInvoiceContext context;
        public CustomersController(
             UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager,
             IdentityDbContext identity,
             InherentInvoiceContext context)

        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.identity = identity;
            this.context = context;


        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateCustomerVM model)
        {
            context.AddCustomerToDB(model, GetCurrentUserID());
            TempData[nameof(CreateCustomerVM.Name)] = model.Name;
            return RedirectToAction(nameof(CustomersController.Create));
        }
        string GetCurrentUserID()
        {
            return userManager.GetUserId(HttpContext.User);
        }
        [HttpGet]
        [Authorize()]
        public IActionResult List()
        {
            var model = context.GetAllCustomersForUser(GetCurrentUserID());
            return View(model);
        }

    }
}
