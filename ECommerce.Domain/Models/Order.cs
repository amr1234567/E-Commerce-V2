using ECommerce.Domain.ComplexObjects;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Identity;
using ECommerce.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    public class Order : BaseClass
    {
        [Required]
        public Address Address { get; set; }

        [Required]
        public decimal DeliveryPrice { get; set; }

        [Required]
        public decimal TotalPriceBeforeDiscount { get; set; }
        [Required]
        public decimal TotalPriceAfterDiscount { get; set; }

        [Required]
        public DateTime OrderedAt { get; init; } = DateTime.UtcNow;
        public DateTime? ReachedDeliveryAt { get; set; }
        public DateTime? DeliveredAt { get; init; }

        [Required]
        public int TimeExpectedToDelivered { get; set; } 

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        public int? DiscountId { get; set; }
        [ForeignKey(nameof(DiscountId))]
        public Discount? Discount { get; set; }

        [Required]
        public int CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        [Required]
        public int DeliveryManId { get; set; }
        [ForeignKey(nameof(DeliveryManId))]
        public DeliveryMan DeliveryMan { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
