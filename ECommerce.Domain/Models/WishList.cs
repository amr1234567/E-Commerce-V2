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
    public class WishList : BaseClass
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public DateTime CreatedDate { get; init; } = DateTime.UtcNow;

        public int CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        public List<Product>? Products { get; set; }
    }
}
