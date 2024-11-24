using ECommerce.Domain;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.DataAccess.EFContext.Configurations
{
    internal class OrderLogConfigurations : IEntityTypeConfiguration<OrderLog>
    {
        public void Configure(EntityTypeBuilder<OrderLog> builder)
        {
            builder.HasKey(o => new { o.OrderId, o.DeliveryManId });

            builder.Property(b => b.ResponseType)
             .HasConversion(
                 p => p.ToString(),
                 p => (DeliveryResponseType)Enum.Parse(typeof(DeliveryResponseType), p)
             );
        }
    }
}
