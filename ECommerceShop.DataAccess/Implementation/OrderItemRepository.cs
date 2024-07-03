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
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }



        public void Update(OrderItem orderitem)
        {
            var OrderItemInDb = _context.OrderItems.FirstOrDefault(x => x.OrderItemId == orderitem.OrderId);
            if (OrderItemInDb != null)
            {
                OrderItemInDb.OrderItemId = orderitem.OrderItemId;

                OrderItemInDb.OrderId = orderitem.OrderId;
                OrderItemInDb.ProductId = orderitem.ProductId;
                OrderItemInDb.Quantity = orderitem.Quantity;
                OrderItemInDb.UnitPrice = orderitem.UnitPrice;

            }
        }
    }
}
