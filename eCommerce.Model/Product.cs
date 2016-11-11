using System.ComponentModel.DataAnnotations;
using eCommerce.Contracts.Modules;

namespace eCommerce.Model
{
    public class Product : IProduct
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        [MaxLength(255)]
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
        public int Quantity { get; set; }
    }
}
