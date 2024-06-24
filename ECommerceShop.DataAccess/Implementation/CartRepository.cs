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
    public class CartRepository:GenericRepository<Cart>,ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

      

        public void Update(Cart cart)
        {
            var CartInDb = _context.Carts.FirstOrDefault(x => x.CartId == cart.CartId);
            if (CartInDb != null)
            {
                CartInDb.CartId = cart.CartId;
                CartInDb.UserId = cart.UserId;
                

            }
        }
    }
}
