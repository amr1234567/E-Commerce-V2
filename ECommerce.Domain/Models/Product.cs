using ECommerce.Domain.ComplexObjects;
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
    public class Product : BaseClass
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        [Range(0, 5)]
        public decimal Rate { get; set; } = 0;

        [Required]
        [Range(0, int.MaxValue)]
        public int AmountAvailable { get; set; }

        
        public ProductDiscount? ProductDiscount { get; set; }

        public List<Review>? Reviews { get; set; }
        public List<Image> Images { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        [Required]
        public int ProviderId { get; set; }
        [ForeignKey(nameof(ProviderId))]
        public Provider Provider { get; set; }
    }
}
