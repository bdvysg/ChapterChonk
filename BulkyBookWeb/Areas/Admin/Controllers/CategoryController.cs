using ChapterChonk.DataAccess.Data;
using ChapterChonk.DataAccess.Repository;
using ChapterChonk.DataAccess.Repository.IRepository;
using ChapterChonk.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChapterChonkWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _unitOfWork.Category.GetAll().ToList();
            return View(categoryList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("NameError", "The Name cannot exactly match the Display Order");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();

                TempData["message"] = "Category created succesfully";

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

            var category = _unitOfWork.Category.Get(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("NameError", "The Name cannot exactly match the Display Order");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
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

            var category = _unitOfWork.Category.Get(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var category = _unitOfWork.Category.Get(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();

            TempData["message"] = "Category deleted succesfully";

            return RedirectToAction("Index");

        }
    }
}
