using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ECommerceShop.Entities.Models.Domain
{
    public class Product
    {
        /*   [Key]
           public int Id { get; set; }

           [Required]
           public string Name { get; set; }
           public string Description { get; set; }
           public string Img { get; set; }

           [Required]
           public decimal Price { get; set; }

           [Required]
           public int CategoryId { get; set; }

           public Category Category { get; set; }*/

        [Key]
        public int ProductId { get; set; }

        //[Required]
        [Column("theName", TypeName = "varchar(20)")]
        public string? Name { get; set; }
        
        public string? Description { get; set; }

        //[Required]
        public decimal Price { get; set; }

       // [Required]
        public int Stock { get; set; }

        //[Required]
        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }

        //[AllowNull]
        public Category? Category { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
        public ICollection<Review>? Reviews { get; set; }

    }
}
