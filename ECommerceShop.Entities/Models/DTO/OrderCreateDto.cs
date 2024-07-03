using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShop.Entities.Models.DTO
{
    public class OrderCreateDto
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? OrderStatus { get; set; }
        public string? ShippingAddress { get; set; }
        public ICollection<OrderItemCreateDto> OrderItems { get; set; }
    }
}
