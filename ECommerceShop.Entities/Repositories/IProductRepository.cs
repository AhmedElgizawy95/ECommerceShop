using ECommerceShop.Entities.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShop.Entities.Repositories
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        void Update(Product product); 
    }
}
