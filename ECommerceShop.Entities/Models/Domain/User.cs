using System.ComponentModel.DataAnnotations;

namespace ECommerceShop.Entities.Models.Domain
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        
        public string? Username { get; set; }
        public string? Email { get; set; }

        
        public string? PasswordHash { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Role { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<Order>? Orders { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public Cart? Cart { get; set; }
    }
}
