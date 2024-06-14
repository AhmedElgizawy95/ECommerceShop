using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShop.Entities.Repositories
{
    public interface IUnitofWork:IDisposable
    {
        ICategoryRepository Category { get; }
        int Complete();
    }
}
