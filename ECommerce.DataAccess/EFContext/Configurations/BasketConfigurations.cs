﻿using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.EFContext.Configurations
{
    internal class BasketConfigurations : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasMany(b => b.OrderItems)
                .WithOne(o => o.Basket)
                .HasForeignKey(o => o.BasketId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(b => b.CustomerId)
                .IsClustered();
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

        }
    }
}
