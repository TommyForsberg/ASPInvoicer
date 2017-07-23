using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using InherentInvoiceAlfa.Models.ViewModels;
using InherentInvoiceAlfa.Models.Entities;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InherentInvoiceAlfa.Controllers
{
    public class UserController : Controller
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        IdentityDbContext identity;
        InherentInvoiceContext context;
        public UserController(
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
        [Authorize()]
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await userManager.GetUserAsync(User);
            var email = user.Email;
            var hashId = user.Id;
            EditUserVM model = context.GetEditUserVM(hashId);
            model.Email = email;
            return View(model);
        }
        [Authorize()]
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserVM model)
        {
            var user = await userManager.GetUserAsync(User);
            //user.Email = model.Email;
            await userManager.SetEmailAsync(user, model.Email);
            context.UpdateUser(model, GetCurrentUserID());
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(CreateUserVM viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            // Create the DB Schema
            await identity.Database.EnsureCreatedAsync();
           // context.Database.EnsureCreated();
            // 1. Create user
            var user = new IdentityUser(viewModel.UserName);
            user.Email = viewModel.Email;
            var result = await userManager.CreateAsync(user, viewModel.Password);
          
            
            if (!result.Succeeded)
            {
                ModelState.AddModelError(
                    nameof(CreateUserVM.UserName),
                    result.Errors.First().Description);
                return View(viewModel);
            }

            // 2. Log in user
            await signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, false, false);

            //3. Add user to context DB
            
            var currentUser = await userManager.FindByNameAsync(viewModel.UserName);
            viewModel.HashId  = currentUser.Id;
            context.AddUserToDB(viewModel);

            // 4. Redirect user           
            return RedirectToAction(nameof(UserController.Edit));
        }

        [Authorize()]
        string GetCurrentUserID()
        {
            return userManager.GetUserId(HttpContext.User);
        }

       

        public IActionResult GetServiceHtml(string jsonString)
        {
            var jsonSettings  = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var model = JsonConvert.DeserializeObject<ServiceVM[]>(jsonString,jsonSettings);
            return PartialView("ServiceTable", model);
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginVM viewModel)
        {
            await signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, false, false);
            var user = await userManager.FindByNameAsync(viewModel.UserName);
            if (user != null)
            {
               // var id = context.User.SingleOrDefault(o => o.HashId.Equals(user.Id)).Id;
                return RedirectToAction(nameof(UserController.Edit));
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }


    }
}
