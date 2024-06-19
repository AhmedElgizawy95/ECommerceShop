using ECommerceShop.DataAccess.Data;
using ECommerceShop.Entities.Models.Domain;
using ECommerceShop.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShop.DataAccess.Implementation
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void update(Product product)
        {
            var productInDb = _context.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
            if (productInDb != null)
            {
                productInDb.Name = product.Name;
                productInDb.Description = product.Description;
                productInDb.CreatedDate = DateTime.Now;
                productInDb.Price = product.Price;
                productInDb.Stock = product.Stock;
                productInDb.ImageUrl = product.ImageUrl;
                productInDb.CategoryId = product.CategoryId;


            }

        }
    }
}
