using ECommerce.Domain.Enums;
using ECommerce.Domain.Identity;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.EFContext.Configurations
{
    internal class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(b => b.Role)
            .HasConversion(
                p => p.ToString(),
                p => (ApplicationRole)Enum.Parse(typeof(ApplicationRole), p)
            );
            builder.ComplexProperty(c => c.Address, options =>
            {
                options.Property(a => a.Street).IsRequired();
                options.Property(a => a.PostalCode).IsRequired(false);
                options.Property(a => a.State).IsRequired();
                options.Property(a => a.City).IsRequired();
                options.Property(a => a.Country).IsRequired();
            });

            builder.HasMany(c=>c.WishLists)
                .WithOne(c => c.Customer)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Basket)
                .WithOne(b => b.Customer)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.FavProviders)
                .WithMany(p => p.LoverCustomers)
                 .UsingEntity<Dictionary<string, object>>(
                    "CustomerProviders",
                    join => join
                        .HasOne<Provider>() // Target entity
                        .WithMany()         // No navigation property on `Provider`
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade), // Cascade delete behavior
                    join => join
                        .HasOne<Customer>() // Source entity
                        .WithMany()          // No navigation property on `Customer`
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade));

            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Reviews)
               .WithOne(o => o.Customer)
               .HasForeignKey(o => o.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Tickets)
               .WithOne(o => o.Customer)
               .HasForeignKey(o => o.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
