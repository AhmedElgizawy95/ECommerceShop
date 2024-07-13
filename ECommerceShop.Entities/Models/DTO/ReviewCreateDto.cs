using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShop.Entities.Models.DTO
{
    public class ReviewCreateDto
    {
        public int ProductId { get; set; }
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
