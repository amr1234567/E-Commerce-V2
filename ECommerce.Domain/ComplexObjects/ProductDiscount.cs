using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.ComplexObjects
{
    [Owned]
    public class ProductDiscount
    {
        public DateTime? StartFrom { get; set; }
        public DateTime? EndAt { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? MinPriceToApply { get; set; }
    }
}
