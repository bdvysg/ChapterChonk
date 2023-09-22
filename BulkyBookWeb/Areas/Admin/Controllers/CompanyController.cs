using ChapterChonk.DataAccess.Repository;
using ChapterChonk.DataAccess.Repository.IRepository;
using ChapterChonk.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChapterChonkWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new Company());
            }
            else
            {
                Company company = _unitOfWork.Company.Get(u=> u.Id == id);
                return View(company);
            }
        }


        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    _unitOfWork.Company.Add(company);
                    TempData["message"] = "Company added succesfuly";
                }
                else
                {
                    _unitOfWork.Company.Update(company);
                    TempData["message"] = "Company updated succesfuly";
                }
                
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            else
            {
                return View(company);
            }
        }


        #region API CALLS

        [HttpGet] 
        public IActionResult GetAll()
        {
            List<Company> companies = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = companies});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Company company = _unitOfWork.Company.Get(u=> u.Id==id);

            if (company != null)
            {
                _unitOfWork.Company.Remove(company);
                _unitOfWork.Save();

                return Json(new { succes = false, message = "Company not found" });
            }
            else
            {
                return Json(new { succes = true, message = "Delete Succesful" });
            }
        }
        #endregion
    }
}
