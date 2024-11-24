using ECommerce.Domain.Base;
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
    public class OrderItem : BaseClass
    {
        [Required]
        [Range(1, int.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        [Range(0,int.MaxValue)]
        public int Quantity { get; set; }

        public int? OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }

        public int? BasketId { get; set; }
        [ForeignKey(nameof(BasketId))]
        public Basket? Basket { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
