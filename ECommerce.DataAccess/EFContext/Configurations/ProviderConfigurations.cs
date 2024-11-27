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
    internal class ProviderConfigurations : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.ToTable(nameof(Provider) + "s");

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

            builder.HasMany(c => c.Products)
               .WithOne(o => o.Provider)
               .HasForeignKey(o => o.ProviderId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Images)
              .WithOne(o => o.Provider)
              .HasForeignKey(o => o.ProviderId)
              .IsRequired(false)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Tickets)
              .WithOne(o => o.Provider)
              .HasForeignKey(o => o.ProviderId)
              .IsRequired(false)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Reviews)
              .WithOne(o => o.Provider)
              .HasForeignKey(o => o.ProviderId)
              .IsRequired(false)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
