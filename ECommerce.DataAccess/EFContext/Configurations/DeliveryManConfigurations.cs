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
    internal class DeliveryManConfigurations : IEntityTypeConfiguration<DeliveryMan>
    {
        public void Configure(EntityTypeBuilder<DeliveryMan> builder)
        {
            builder.Property(b => b.Role)
            .HasConversion(
                p => p.ToString(),
                p => (ApplicationRole)Enum.Parse(typeof(ApplicationRole), p)
            );

            builder.Property(b => b.DeliveryWay)
              .HasConversion(
                  p => p.ToString(),
                  p => (DeliveryWay)Enum.Parse(typeof(DeliveryWay), p)
              );

            builder.ComplexProperty(d => d.Address, options =>
            {
                options.Property(a => a.Street).IsRequired();
                options.Property(a => a.PostalCode).IsRequired(false);
                options.Property(a => a.State).IsRequired();
                options.Property(a => a.City).IsRequired();
                options.Property(a => a.Country).IsRequired();
            });

            builder.HasMany(d => d.Orders)
                .WithOne(o => o.DeliveryMan)
                .HasForeignKey(o => o.DeliveryManId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Reviews)
               .WithOne(o => o.DeliveryMan)
               .HasForeignKey(o => o.DeliveryManId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.OrderLogs)
                .WithOne(o => o.DeliveryMan)
                .HasForeignKey(o => o.DeliveryManId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
