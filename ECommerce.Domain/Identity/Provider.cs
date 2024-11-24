using ECommerce.Domain.Base;
using ECommerce.Domain.ComplexObjects;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Identity
{
    public class Provider : IdentityBase
    {
        public Address Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? ContactPhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? ContactEmail { get; set; }

        public string? Description { get; set; }

        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Image>? Images { get; set; }
        public List<Ticket>? Tickets { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<Customer>? LoverCustomers { get; set; }

    }
}
