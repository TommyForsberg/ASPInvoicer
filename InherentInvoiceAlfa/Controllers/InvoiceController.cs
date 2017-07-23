using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InherentInvoiceAlfa.Models.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using InherentInvoiceAlfa.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InherentInvoiceAlfa.Controllers
{
    [Authorize()]
    public class InvoiceController : Controller
    {
        #region Fields
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        IdentityDbContext identity;
        InherentInvoiceContext context;
        #endregion

        #region Constructors
        public InvoiceController(
             UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager,
             IdentityDbContext identity,
             InherentInvoiceContext context,
             IHostingEnvironment hostingEnvironment)

        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.identity = identity;
            this.context = context;
        }
        #endregion

       /// <summary>
       /// Fetches the hash ID present in Identity cookie.
       /// </summary>
        public string CurrentUser
        {
            get
            {
                return userManager.GetUserId(HttpContext.User);
            }        
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var model = new InvoiceCreateVM {
                Customers = context.GetAllCustomersAsSelectList(CurrentUser)};
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(InvoiceCreateVM model, string submit)
        {
            if (!ModelState.IsValid)
                return View(model);

            switch(submit)
            {
                case "Arkivera":
                    return Archive(model);
                    
                case "Pdf":
                   return  GetPdf(model);

                default:
                    return Archive(model);
            }
            //var newInvoice = context.CompileInvoice(model, CurrentUser);
            //context.PersistInvoice(newInvoice);
            //return View(model);
        }
        public IActionResult Archive(InvoiceCreateVM model)
        {
            return View(nameof(Create),model);
        }
       public FileStreamResult GetPdf(InvoiceCreateVM model)
        {           
            var file = context.GetInvoicePdf(model,CurrentUser);
           // var strFile = path + Path.DirectorySeparatorChar + "faktura.pdf";
           // string mimeType = "application/pdf";
            MemoryStream output = new MemoryStream();
            output.Write(file, 0, file.Length);
            output.Position = 0;

           // HttpContext.Response.AddHeader("content-disposition", "attachment; filename=form.pdf");


            // Return the output stream
            return File(output, "application/pdf", "Faktura.Pdf");

        }
    }
}
