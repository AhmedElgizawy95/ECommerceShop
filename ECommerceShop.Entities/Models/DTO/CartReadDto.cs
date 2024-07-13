using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShop.Entities.Models.DTO
{
    public class CartReadDto
    {
        public int CartId { get; set; }
        public string Id { get; set; }
        //public DateTime CreatedDate { get; set; }
        public List<CartItemReadDto> CartItems { get; set; }
    }
}
