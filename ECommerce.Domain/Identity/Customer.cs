using ECommerce.Domain.Base;
using ECommerce.Domain.ComplexObjects;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Identity
{
    public class Customer : IdentityBase
    {
        [DataType(DataType.ImageUrl)]
        public string? ImageUrl { get; set; }

        public Address Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public List<WishList>? WishLists { get; set; }

        public int BasketId { get; set; }
        [ForeignKey(nameof(BasketId))]
        public Basket? Basket { get; set; }

        public List<Order>? Orders { get; set; }

        public List<Review>? Reviews { get; set; }

        public List<Provider>? FavProviders { get; set; }

        public List<Ticket>? Tickets { get; set; }

        public List<Discount>? Discounts { get; set; }
    }
}
