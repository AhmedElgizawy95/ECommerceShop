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
    public class OrderRepository: GenericRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }



        public void Update(Order order)
        {
            var OrderInDb = _context.Orders.FirstOrDefault(x => x.OrderId == order.OrderId);
            if (OrderInDb != null)
            {
                OrderInDb.OrderId = order.OrderId;
                OrderInDb.UserId = order.UserId;
               OrderInDb.OrderDate = order.OrderDate;
                OrderInDb.TotalAmount = order.TotalAmount;
                OrderInDb.OrderStatus = order.OrderStatus;
                OrderInDb.ShippingAddress = order.ShippingAddress;

    }
        }
    }
}
