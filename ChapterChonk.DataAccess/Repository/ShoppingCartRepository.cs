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
    public class ShoppingCartRepository : Repository<ShopingCart>, IShoppingCartRepository
    {
        ApplicationDBContext _dbContext;

        public ShoppingCartRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(ShopingCart obj)
        {
            _dbContext.Update(obj);
        }
    }
}
