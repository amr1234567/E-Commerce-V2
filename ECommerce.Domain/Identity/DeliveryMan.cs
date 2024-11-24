using ECommerce.Domain.ComplexObjects;
using ECommerce.Domain.Models;
using ECommerce.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Identity
{
    public class DeliveryMan : IdentityBase
    {
        public Address Address { get; set; }
        public List<Order> Orders { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        
    }
}
