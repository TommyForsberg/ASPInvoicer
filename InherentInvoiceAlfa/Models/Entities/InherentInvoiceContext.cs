using System;
using InherentInvoiceAlfa.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InherentInvoiceAlfa.Models.Entities
{
    public partial class InherentInvoiceContext : DbContext
    {
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "inhrnt");

                entity.Property(e => e.Adress).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserHashId).HasMaxLength(450);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice", "inhrnt");

                entity.Property(e => e.Total).HasColumnType("decimal");

                entity.Property(e => e.TotalWithoutVat)
                    .HasColumnName("TotalWithoutVAT")
                    .HasColumnType("decimal");

                entity.Property(e => e.Vatamount)
                    .HasColumnName("VATAmount")
                    .HasColumnType("decimal");

                entity.Property(e => e.Vatpercentage)
                    .HasColumnName("VATPercentage")
                    .HasColumnType("decimal");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Invoice_ToCustomer");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Invoice_ToUser");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service", "inhrnt");

                entity.Property(e => e.Amount)
                    .IsRequired()
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal");

                entity.Property(e => e.ServiceTotalWithoutVat)
                    .HasColumnName("ServiceTotalWithoutVAT")
                    .HasColumnType("decimal");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.Service)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK_Service_ToInvoice");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "inhrnt");

                entity.Property(e => e.BankAccount).HasMaxLength(50);

                entity.Property(e => e.BicSwift).HasMaxLength(50);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(30);

                entity.Property(e => e.Giro).HasMaxLength(50);

                entity.Property(e => e.HashId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Iban)
                    .HasColumnName("IBAN")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(30);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.PostalAddress).HasMaxLength(50);

                entity.Property(e => e.Street).HasMaxLength(50);
            });
        }

      
    }
}