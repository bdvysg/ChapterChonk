using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterChonk.Models.ViewModels
{
    public class ShopingCartVM
    {
        public IEnumerable<ShopingCart> ShopingCarts { get; set; }
        public double OrderTotal { get; set; }
    }
}
