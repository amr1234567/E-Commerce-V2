﻿using ECommerce.Domain.Identity;
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

    public class Image : BaseClass
    {
        [Required]
        [DataType(DataType.ImageUrl)]
        public string Url { get; set; }

        public int? ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

        public int? ProviderId { get; set; }
        [ForeignKey(nameof(ProviderId))]
        public Provider? Provider { get; set; }
    }
}