using ChapterChonk.DataAccess.Repository.IRepository;
using ChapterChonk.Models;
using ChapterChonk.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChapterChonkWeb.Areas.Costumer.Controllers
{
    [Area("Costumer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private ShopingCartVM _shopingCartVM;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            _shopingCartVM = new ShopingCartVM
            {
                ShopingCarts = _unitOfWork.ShoppingCart.GetAll(u=> u.UserId == userId, includeProperties:"Product")
            };

            foreach (var cart in _shopingCartVM.ShopingCarts)
            {
                cart.Price = CalculatePriceBasedOnQuantity(cart);
                _shopingCartVM.OrderTotal += (cart.Price * cart.Count);
            }
            return View(_shopingCartVM);
        }

        public IActionResult Summary()
        {
            return View();
        }

        public IActionResult IncrementCount(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.Get(u=> u.Id == cartId);
            cart.Count++;
            _unitOfWork.ShoppingCart.Update(cart);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DecrementCount(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);

            if (cart.Count <= 1) 
            {
                _unitOfWork.ShoppingCart.Remove(cart);
            }
            else
            {
                cart.Count--;
                _unitOfWork.ShoppingCart.Update(cart);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private double CalculatePriceBasedOnQuantity(ShopingCart cart)
        {
            if (cart.Count <= 50)
            {
                return cart.Product.Price;
            }
            else if(cart.Count <= 100)
            {
                return cart.Product.Price50;
            }
            else
            {
                return cart.Product.Price100;
            }
        }
    }
}
