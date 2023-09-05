using ChapterChonk.DataAccess.Repository.IRepository;
using ChapterChonk.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChapterChonkWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll().ToList();
            return View(productList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();

                TempData["message"] = "Product created succesfully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var product = _unitOfWork.Product.Get(c => c.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();

                TempData["message"] = "Category updated succesfully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var product = _unitOfWork.Product.Get(c => c.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var product = _unitOfWork.Product.Get(c => c.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();

            TempData["message"] = "Product deleted succesfully";

            return RedirectToAction("Index");

        }
    }
}
