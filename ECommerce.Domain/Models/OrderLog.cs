using ECommerce.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    [Table("OrderLogs")]
    public class OrderLog
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int DeliveryManId { get; set; }
        public DeliveryMan DeliveryMan { get; set; }
        public DeliveryResponseType ResponseType { get; set; }
        public DateTime CreationTime { get; init; } = DateTime.UtcNow;
    }
}
