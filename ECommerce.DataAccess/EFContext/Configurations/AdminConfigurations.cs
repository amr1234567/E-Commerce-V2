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
    internal class AdminConfigurations : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.Property(b => b.Role)
              .HasConversion(
                  p => p.ToString(),
                  p => (ApplicationRole)Enum.Parse(typeof(ApplicationRole), p)
              );

            builder.ToTable(nameof(Admin) + "s");
        }
    }
}
