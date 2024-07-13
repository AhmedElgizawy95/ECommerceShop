using ECommerceShop.DataAccess.Data;
using ECommerceShop.Entities.Models.Domain;
using ECommerceShop.Entities.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShop.DataAccess.Implementation
{
    public class UnitOfWork : IUnitofWork
    {
        private readonly ApplicationDbContext _context;

        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }

        public ICartRepository Cart { get; private set; }

        public ICartItemRepository CartItem { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IOrderItemRepository OrderItem { get; private set; }

        public IUserRepository User { get; private set; }

        public UnitOfWork(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            Category = new CategoryRepository(context);
            Product = new ProductRepository(context);
            Cart = new CartRepository(context);
            CartItem = new CartItemRepository(context);
            Order = new OrderRepository(context);
            OrderItem = new OrderItemRepository(context);
            User = new UserRepository(_context, userManager);
        }

        public int Complete()
        {
           return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
