using ECommerceShop.Entities.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShop.Entities.Models.DTO
{
    public class ProductCreateDto
    {
        
        
        public string Name; 

        public string Description;
        public decimal Price { get; set; }  
        public int Stock { get; set; }      
        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }

       
    }
}
