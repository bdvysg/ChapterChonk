using ChapterChonk.DataAccess.Data;
using ChapterChonk.DataAccess.Repository.IRepository;
using ChapterChonk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterChonk.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        ApplicationDBContext _dbContext;

        public ProductRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Product obj)
        {
            _dbContext.Update(obj);
        }
    }
}
