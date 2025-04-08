using Microsoft.EntityFrameworkCore;
using StoreManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Models
{
    public class StoreManagementContext : DbContext
    {
        public StoreManagementContext(DbContextOptions<StoreManagementContext> options) : base(options) { }

        public DbSet<Manager> Manager { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<SupplierProduct> SupplierProducts { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderProducts>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProducts>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderProducts>()
                .HasOne(op => op.Product)
                .WithMany()
                .HasForeignKey(op => op.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SupplierProduct>()
                .HasKey(sp => new { sp.SupplierId, sp.ProductId });

            modelBuilder.Entity<SupplierProduct>()
                .HasOne(sp => sp.Supplier)
                .WithMany(s => s.SupplierProduct)
                .HasForeignKey(sp => sp.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SupplierProduct>()
                .HasOne(sp => sp.Product)
                .WithMany()
                .HasForeignKey(sp => sp.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
