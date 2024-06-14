using System.ComponentModel.DataAnnotations;

namespace ECommerceShop.Entities.Models.Domain
{ 
    public class Payment
    {
        /* [Key]
         public int PaymentId { get; set; }
         public int OrderId { get; set; }
         public DateTime PaymentDate { get; set; }
         public decimal Amount { get; set; }
         public string PaymentMethod { get; set; }
         public string PaymentStatus { get; set; }

         public Order Order { get; set; }*/
        [Key]
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentStatus { get; set; }

        public Order? Order { get; set; }
    }
}
