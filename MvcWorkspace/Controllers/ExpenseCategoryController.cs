using Microsoft.AspNetCore.Mvc;
using MvcWorkspace.Data;
using MvcWorkspace.Models;

namespace MvcWorkspace.Controllers
{
    public class ExpenseCategoryController : Controller
    {
        private readonly AppDbContext _db;

        public ExpenseCategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<ExpenseCategory> ExpenseCatList = _db.ExpenseCategories;

            return View(ExpenseCatList);
        }

        //GET- Add or Edit
        public IActionResult AddOrEdit(int id)
        {
            if (id == 0)
                return View(new ExpenseCategory());
            else
                return View(_db.ExpenseCategories.Find(id));
        }

        //POST : Add or Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(ExpenseCategory expenseCat)
        {
            if (ModelState.IsValid)
            {
                if (expenseCat.Id == 0)
                {
                    _db.Add(expenseCat);
                }
                else
                {
                    _db.Update(expenseCat);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenseCat);
        }

        //Delete
        public IActionResult Delete(int id)
        {
            var obj = _db.ExpenseCategories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.ExpenseCategories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
