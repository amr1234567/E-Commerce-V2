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
    public class Ticket : BaseClass
    {
        [Required]
        [StringLength(255)]
        public string Text { get; set; }
        public int? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer? Customer { get; set; }

        public int? ProviderId { get; set; }
        [ForeignKey(nameof(ProviderId))]
        public Provider? Provider { get; set; }
    }
}
