/*using ECommerce.Entities.Models.Domain;
using Microsoft.EntityFrameworkCore;*/
using ECommerceShop.Entities.Models.Domain;
using Microsoft.EntityFrameworkCore;
namespace ECommerceShop.DataAccess.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithOne(u => u.Cart)
                .HasForeignKey<Cart>(c => c.UserId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId);


            // Seed data
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "Alice" },
                new User { UserId = 2, Username = "Bob" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Books" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Laptop", Price = 999.99M, CategoryId = 1 },
                new Product { ProductId = 2, Name = "Smartphone", Price = 599.99M, CategoryId = 1 },
                new Product { ProductId = 3, Name = "Novel", Price = 19.99M, CategoryId = 2 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, UserId = 1, OrderDate = DateTime.Now, TotalAmount = 1019.98M, OrderStatus = "Completed", ShippingAddress = "123 Main St" }
            );

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { OrderItemId = 1, OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 999.99M },
                new OrderItem { OrderItemId = 2, OrderId = 1, ProductId = 3, Quantity = 1, UnitPrice = 19.99M }
            );

            modelBuilder.Entity<Cart>().HasData(
                new Cart { CartId = 1, UserId = 1 }
            );

            modelBuilder.Entity<CartItem>().HasData(
                new CartItem { CartItemId = 1, CartId = 1, ProductId = 2, Quantity = 2 }
            );

            modelBuilder.Entity<Review>().HasData(
                new Review { ReviewId = 1, UserId = 1, ProductId = 1, Rating = 5, Comment = "Great product!" },
                new Review { ReviewId = 2, UserId = 2, ProductId = 3, Rating = 4, Comment = "Good read!" }
            );

            modelBuilder.Entity<Payment>().HasData(
                new Payment { PaymentId = 1, OrderId = 1, PaymentDate = DateTime.Now, Amount = 1019.98M, PaymentMethod = "Credit Card", PaymentStatus = "Paid" }
            );


            base.OnModelCreating(modelBuilder);



           
        }
    }
 }

 
   

