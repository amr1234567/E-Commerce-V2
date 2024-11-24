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
    public class DiscountLog
    {
        [Required]
        public int DiscountId { get; set; }
        [Required]
        [ForeignKey(nameof(DiscountId))]
        public Discount Discount { get; set; }

        [Required]
        public int CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        [Required]
        public Customer Customer { get; set; }

        [Required]
        public DateTime AppliedDate { get; set; } 
        
    }
}
