using ECommerce.Domain.Enums;
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
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasKey(p => p.Id);
            builder.ComplexProperty(c => c.Address, options =>
            {
                options.Property(a => a.Street).IsRequired();
                options.Property(a => a.PostalCode).IsRequired(false);
                options.Property(a => a.State).IsRequired();
                options.Property(a => a.City).IsRequired();
                options.Property(a => a.Country).IsRequired();
            });

            builder.HasMany(b => b.OrderItems)
               .WithOne(o => o.Order)
               .HasForeignKey(o => o.OrderId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

            builder.Property(b => b.PaymentMethod)
                .HasConversion(
                    p => p.ToString(),
                    p => (PaymentMethod)Enum.Parse(typeof(PaymentMethod), p)
                );

            builder.Property(b => b.OrderStatus)
               .HasConversion(
                   p => p.ToString(),
                   p => (OrderStatus)Enum.Parse(typeof(OrderStatus), p)
               );
            builder.HasMany(b => b.OrderLogs)
                .WithOne(o => o.Order)
                .HasForeignKey(o => o.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
