using ECommerce.Domain.Identity;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.EFContext.Configurations
{
    internal class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasIndex(c => c.Name).IsUnique();
            builder.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Discounts)
               .WithOne(p => p.Category)
               .HasForeignKey(p => p.CategoryId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Providers)
                .WithMany(p => p.Categories)
                .UsingEntity<Dictionary<string, object>>(
                    "CategoryProvider",
                    cp => cp.HasOne<Provider>()
                    .WithMany()
                    .HasForeignKey("ProviderId")
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict),
                    cp => cp.HasOne<Category>()
                    .WithMany()
                    .HasForeignKey("CategoryId")
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict));
        }
    }
}
