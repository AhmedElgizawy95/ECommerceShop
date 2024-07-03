using ECommerceShop.DataAccess.Data;
using ECommerceShop.Entities.Models.Domain;
using ECommerceShop.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShop.DataAccess.Implementation
{
    public class CartItemRepository: GenericRepository<CartItem>, ICartItemRepository
    {
        private readonly ApplicationDbContext _context;

        public CartItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(CartItem cartitem)
        {
            var CartItemInDb = _context.CartItems.FirstOrDefault(x => x.CartItemId == cartitem.CartItemId);
            if (CartItemInDb != null)
            {
                CartItemInDb.CartId = cartitem.CartId;
                CartItemInDb.ProductId = cartitem.ProductId;
                CartItemInDb.Quantity = cartitem.Quantity;

            }
        }
    }
}
