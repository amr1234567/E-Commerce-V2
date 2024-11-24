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
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //builder.ComplexProperty(p => p.ProductDiscount, options =>
            //{
            //    options.Property(d => d.StartFrom)
            //        .HasColumnType("datetime").IsRequired(false); // Represents the start datetime of the discount

            //    options.Property(d => d.MinPriceToApply)
            //        .HasColumnType("decimal(18,2)").IsRequired(false); // Represents the minimum price to apply the discount

            //    options.Property(d => d.EndAt)
            //        .HasColumnType("datetime").IsRequired(false); // Represents the end datetime of the discount

            //    options.Property(d => d.DiscountAmount)
            //        .HasColumnType("decimal(18,2)").IsRequired(false); // Represents the discount amount
            //});
        }
    }
}
