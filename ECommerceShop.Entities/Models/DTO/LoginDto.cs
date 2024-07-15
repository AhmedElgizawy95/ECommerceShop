using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShop.Entities.Models.DTO
{
    public class LoginDto
    {
        [Required]
        public string userEmail { get; set; }

        [Required]
        public string password { get; set; }


    }
}
