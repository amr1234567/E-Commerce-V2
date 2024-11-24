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
    public class Discount : BaseClass
    {
        [Required]
        public DateTime StartFrom { get; set; }
        [Required]
        public DateTime EndAt { get; set; }
        [Required]
        public decimal DiscountAmount { get; set; }
        [Required]
        public decimal MinPriceToApply { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public List<Customer>? Customers { get; set; }
    }
}
