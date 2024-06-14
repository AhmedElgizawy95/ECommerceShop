using ECommerceShop.DataAccess.Data;
using ECommerceShop.Entities.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShop.DataAccess.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async void AddAsync(T entity)
        {
           await _dbSet.AddAsync(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null, string? IncludeWord = null)
        {
            IQueryable<T> query = _dbSet;
            if(predicate != null)
            {
                query = query.Where(predicate);
            }
            if(IncludeWord != null)
            {
                foreach(var item in IncludeWord.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.ToList();
        }
         async Task<IEnumerable<T>> IGenericRepository<T>.GetAllAsync(Expression<Func<T, bool>>? predicate, string? IncludeWord)
        {
            IQueryable<T> query = _dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (IncludeWord != null)
            {
                foreach (var item in IncludeWord.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return await query.ToListAsync();
        }


        public T GetFirstOrDefault(Expression<Func<T, bool>>? predicate = null, string? IncludeWord = null)
        {
            IQueryable<T> query = _dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (IncludeWord != null)
            {
                foreach (var item in IncludeWord.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.SingleOrDefault();
        }


        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        //public async void RemoveAsync(T entity)
        //{
        //    await _dbSet.Remove(entity);
        //}

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        //public async void RemoveRangeAsync(IEnumerable<T> entities)
        //{
        //   await _dbSet.RemoveRangeAsync(entities);
        //}

     

        async Task<T> IGenericRepository<T>.GetFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate, string? IncludeWord)
        {
            IQueryable<T> query = _dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (IncludeWord != null)
            {
                foreach (var item in IncludeWord.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return await query.SingleOrDefaultAsync();
        }

       
    }
}
