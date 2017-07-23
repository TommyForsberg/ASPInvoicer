using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using InherentInvoiceAlfa.Models.ViewModels;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace InherentInvoiceAlfa.Models.Entities
{
    public partial class InherentInvoiceContext : DbContext
    {
    



        public InherentInvoiceContext(DbContextOptions<InherentInvoiceContext> options) : base(options)
        {

        }

        internal EditUserVM GetEditUserVM(string hashId)
        {
            var model = User
                .SingleOrDefault(o => o.HashId.Equals(hashId));
            var viewModel = Mapper.Map<EditUserVM>(model);
            return viewModel;
        }
        internal void UpdateUser(EditUserVM model, string hashId)
        {
            var userToBeUpdated = User.Single(o => o.HashId.Equals(hashId));
            userToBeUpdated.BankAccount = model.BankAccount;
            userToBeUpdated.BicSwift = model.BicSwift;
            userToBeUpdated.City = model.City;
            userToBeUpdated.FirstName = model.FirstName;
            userToBeUpdated.Giro = model.Giro;
            userToBeUpdated.Iban = model.Iban;
            userToBeUpdated.LastName = model.LastName;
            userToBeUpdated.Phone = model.Phone;
            userToBeUpdated.PostalAddress = model.PostalAddress;
            userToBeUpdated.Street = model.Street;
            SaveChanges();
        }

        internal void AddUserToDB(CreateUserVM model)
        {
            var user = new User
            {
                HashId = model.HashId
            };
            User.Add(user);
            SaveChanges();
        }

        internal void AddCustomerToDB(CreateCustomerVM model, string hashId)
        {
            var newCust = Mapper.Map<Customer>(model);
            newCust.UserHashId = hashId;
            Customer.Add(newCust);
            SaveChanges();
        }

        internal ListAllCustomersVM[] GetAllCustomersForUser(string hashId)
        {
            return Customer
                .Where(cust => cust.UserHashId.Equals(hashId))
                .Select(o => Mapper.Map<ListAllCustomersVM>(o)).ToArray();
        }
        internal List<SelectListItem> GetAllCustomersAsSelectList(string hashId)
        {
            return Customer
                .Where(cust => cust.UserHashId.Equals(hashId))
                .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() })
                .ToList();
        }

        internal List<SelectListItem> GetVatValues()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text="25%", Value="25"},
                new SelectListItem{Text="12%", Value="12"},
                new SelectListItem{Text="6%", Value="6"}
            };
        }

        internal Invoice CompileInvoice(InvoiceCreateVM model, string UserId)
        {
            model.InvoiceServices = JsonConvert.DeserializeObject<ServiceVM[]>(model.JsonString);
            var services = new List<Service>();
            foreach(var service in model.InvoiceServices)
            {
                services.Add(new Service(service.Label, service.Amount,service.Price));
            }
            var customer = Customer.FirstOrDefault(o => o.Id == model.SelectedCustomer);
            var currentUser = User.FirstOrDefault(o => o.HashId == UserId);
            var invoice = new Invoice(customer,0.25M,services,currentUser,000023,30);
            return invoice;
        }

        internal void PersistInvoice(Invoice invoice)
        {
            Invoice.Add(invoice);
            SaveChanges();
        }

        internal byte[] GetInvoicePdf(InvoiceCreateVM model, string currentUser)
        {
            var invoice = CompileInvoice(model, currentUser);
            var newPdf = new SEKPdfDocument(invoice);
            return newPdf.GeneratePDF();
        }

    }
}