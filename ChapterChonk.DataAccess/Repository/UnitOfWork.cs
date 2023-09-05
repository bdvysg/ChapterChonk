using ChapterChonk.DataAccess.Data;
using ChapterChonk.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterChonk.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }

        ApplicationDBContext _dbContext;

        public UnitOfWork(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;

            Category = new CategoryRepository(dbContext);
            Product = new ProductRepository(dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
