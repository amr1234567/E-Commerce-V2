﻿using ECommerce.Domain.Base;
using ECommerce.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    public class Basket : BaseClass
    {
        
        public decimal TotalPriceBeforeDiscount { get; set; } = 0;
        public decimal TotalPriceAfterDiscount { get; set; } = 0;
        public int NumberOfItems { get; set; } = 0;

        public int? DiscountId { get; set; }
        [ForeignKey(nameof(DiscountId))]
        public Discount? Discount { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer? Customer { get; set; }

        public List<OrderItem>? OrderItems { get; set; }
    }
}
