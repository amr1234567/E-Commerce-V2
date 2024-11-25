using ECommerce.Domain.Base;
using ECommerce.Domain.ComplexObjects;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    [Table("Orders")]
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
        public DateTime? DeliveredAt { get; set; }

        [Required]
        public int TimeExpectedToDelivered { get; set; } 

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Created;

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

        public List<OrderLog> OrderLogs { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public static Order GenerateFromBasket(Basket basket)
        {
            ArgumentNullException.ThrowIfNull(basket, nameof(basket));
            ArgumentNullException.ThrowIfNull(basket.OrderItems, nameof(basket));
            ArgumentOutOfRangeException.ThrowIfLessThan(basket.OrderItems.Count, 1);

            return new()
            {
                OrderItems = basket.OrderItems,
                TotalPriceAfterDiscount = basket.TotalPriceAfterDiscount,
                TotalPriceBeforeDiscount = basket.TotalPriceBeforeDiscount,
                DiscountId = basket.DiscountId,
                CustomerId = basket.CustomerId,
            };
        }
    }
}
