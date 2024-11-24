using ECommerce.Domain.Base;
using ECommerce.Domain.ComplexObjects;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Identity
{
    public class DeliveryMan : IdentityBase
    {
        public bool IsConfirmedAsDeliveryMan { get; set; }
        [Range(0,5)]
        public double Rate {  get; set; }

        public Address Address { get; set; }
        public List<Order> Orders { get; set; }
        public List<OrderLog> OrderLogs { get; set; }
        public List<Review> Reviews { get; set; }

        [Range(0, 100)]
        public int CurrentAcceptedOrders { get; set; } = 0;

        public DeliveryWay DeliveryWay { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        
    }
}
