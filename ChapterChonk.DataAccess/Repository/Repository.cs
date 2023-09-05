using ChapterChonk.DataAccess.Data;
using ChapterChonk.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterChonk.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _dbContext;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            this.dbSet = _dbContext.Set<T>();
        }


        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> queries = dbSet;
            queries = queries.Where(filter);
            return queries.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> queries = dbSet;
            return queries.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
