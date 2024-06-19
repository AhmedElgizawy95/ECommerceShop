using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShop.Entities.Models.DTO
{
    public class CategoryUpdateDto
    {

        public string Name;
        public string Description;
        public DateTime CreatedDate { get; set; }
    }
}
