using ChapterChonk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterChonk.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShopingCart>
    {
        void Update(ShopingCart obj);
    }
}

