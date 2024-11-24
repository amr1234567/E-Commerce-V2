using ECommerce.Domain.Base;
using ECommerce.Domain.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ECommerce.Domain.Models
{
    public class Category : BaseClass
    {
        [Required]
        public string Name { get; set; }
        [MinLength(10)]
        [AllowNull]
        public string? Description { get; set; }
        public List<Product> Products { get; set; }
        public List<Provider> Providers { get; set; }
        public List<Discount>? Discounts { get; set; }
    }
}
