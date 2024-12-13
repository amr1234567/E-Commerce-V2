﻿
using ECommerce.DataAccess.EFContext.Configurations;
using ECommerce.Domain.Base;
using ECommerce.Domain.Identity;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.EFContext
{
    public class EFApplicationContext(DbContextOptions<EFApplicationContext> options) : DbContext(options)
    {

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<OrderLog> OrderLogs { get; set; }
        public DbSet<DiscountLog> DiscountLogs { get; set; }


        public DbSet<IdentityBase> BaseUsers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DeliveryMan> DeliveryMen { get; set; }
        public DbSet<Provider> Providers { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfigurations).Assembly);
         
            base.OnModelCreating(modelBuilder);
        }

    }
}


//متنساش الفيفوريت هي ريليشنشيب