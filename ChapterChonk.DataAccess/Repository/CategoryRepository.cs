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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        ApplicationDBContext _dbContext;

        public CategoryRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Category obj)
        {
            _dbContext.Update(obj);
        }
    }
}
