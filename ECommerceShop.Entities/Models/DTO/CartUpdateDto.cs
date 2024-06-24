using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShop.Entities.Models.DTO
{
    public class CartUpdateDto
    {
        public int UserId { get; set; }
        public List<CartItemUpdateDto> CartItems { get; set; }
    }
}
