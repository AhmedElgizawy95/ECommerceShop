using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ECommerceShop.Entities.Models.Domain
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        //[Column("theName", TypeName = "nvarchar(20)")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Product>? Products { get; set; }
    }
}
