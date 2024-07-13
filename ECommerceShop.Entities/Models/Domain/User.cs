using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerceShop.Entities.Models.Domain
{
    public class User : IdentityUser
    {
        /*[Key]
        public int UserId { get; set; }
        
         public string? UserName { get; set; }
         public string? Email { get; set; }


        // public string? PasswordHash { get; set; }

        //public string? Role { get; set; }*/
        //public string? PhoneNumber { get; set; }
        //public string? TempProperty { get; set; }
        public string? FirstName { get; set; }
          public string? LastName { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public ICollection<Order>? Orders { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public Cart? Cart { get; set; }
    }
}
