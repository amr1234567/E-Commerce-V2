using ECommerce.Domain.Base;
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
    internal class IdentityUserConfigurations : IEntityTypeConfiguration<IdentityBase>
    {
        public void Configure(EntityTypeBuilder<IdentityBase> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasIndex(p => p.Email).IsUnique();
            builder.ToTable("IdentityUsers");
        }
    }
}
