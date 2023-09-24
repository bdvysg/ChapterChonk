using ChapterChonk.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChapterChonkWeb.Areas.Costumer.ViewComponents
{
    public class CartIconViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartIconViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            int? count = _unitOfWork.ShoppingCart.GetAll(u=> u.UserId == userId).Count();

            return View(count);
        }
    }

}
