using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShop.Entities.Models.DTO
{
    public class UserReadDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }


        public string? PasswordHash { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Role { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
