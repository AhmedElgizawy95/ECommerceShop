using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShop.Entities.Models.DTO
{
    public class OrderReadDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderItemReadDto> OrderItems { get; set; }
    }
}
