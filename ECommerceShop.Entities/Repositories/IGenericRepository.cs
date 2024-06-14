using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShop.Entities.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T,bool>>? predicate = null,string? IncludeWord = null);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, string? IncludeWord = null);

        T GetFirstOrDefault(Expression<Func<T, bool>>? predicate = null, string? IncludeWord = null);

        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null, string? IncludeWord = null);

        void Add(T entity);

        //void AddListAsync(IEnumerable<T> entities);

        void AddAsync(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

    }
}
