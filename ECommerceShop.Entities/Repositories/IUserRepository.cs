using ECommerceShop.Entities.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShop.Entities.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        //Identity
        Task<IdentityUser> FindByEmailAsync(string email);
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<bool> CheckPasswordAsync(User user, string password);

    }
}
