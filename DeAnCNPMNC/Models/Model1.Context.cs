﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeAnCNPMNC.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CNPMFastFoodEntities : DbContext
    {
        public CNPMFastFoodEntities()
            : base("name=CNPMFastFoodEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<CategoryCustomer> CategoryCustomers { get; set; }
        public virtual DbSet<CategoryFood> CategoryFoods { get; set; }
        public virtual DbSet<CategoryUser> CategoryUsers { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DetailBill> DetailBills { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Revenue> Revenues { get; set; }
    }
}
