using System.Data.Entity;
using DomainService.Adds.Entities;
using MySql.Data.EntityFramework;

namespace DomainService.DbContext
{
    // Атрибут DbConfigurationType указывает EF, что нужно использовать MySQL провайдер
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MyProjectDbContext : System.Data.Entity.DbContext
    {
        public MyProjectDbContext()
            : base("name=MyProjectDbContext")
        {
            // Отключаем прокси, если не нужны динамические прокси-типы EF
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Movement> Movements { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Указываем имена таблиц и схемы, если нужно (но мы уже атрибутами пометили)
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Warehouse>().ToTable("warehouses");
            modelBuilder.Entity<Product>().ToTable("products");
            modelBuilder.Entity<Movement>().ToTable("movements");
            modelBuilder.Entity<Supplier>().ToTable("suppliers");
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<OrderItem>().ToTable("order_items");

            // При необходимости настроить связи вручную:
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Warehouse)
                .WithMany()
                .HasForeignKey(p => p.WarehouseId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Movement>()
                .HasRequired(m => m.Product)
                .WithMany()
                .HasForeignKey(m => m.ProductId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Movement>()
                .HasOptional(m => m.FromWarehouse)
                .WithMany()
                .HasForeignKey(m => m.FromWarehouseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Movement>()
                .HasOptional(m => m.ToWarehouse)
                .WithMany()
                .HasForeignKey(m => m.ToWarehouseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasRequired(o => o.Supplier)
                .WithMany()
                .HasForeignKey(o => o.SupplierId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<OrderItem>()
                .HasRequired(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<OrderItem>()
                .HasRequired(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}
