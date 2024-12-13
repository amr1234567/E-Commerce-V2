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
    internal class DiscountConfigurations : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasIndex(p=> p.Id).IsUnique();
            builder.HasKey(p => p.Id);
        }
    }
}
