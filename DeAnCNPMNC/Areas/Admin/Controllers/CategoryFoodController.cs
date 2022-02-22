using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeAnCNPMNC.Models;

namespace DeAnCNPMNC.Areas.Admin.Controllers
{
    public class CategoryFoodController : Controller
    {
        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        // GET: Admin/CategoryFood
        public ActionResult Index(string _name)
        {
            if (_name == null)
            {
                return View(database.CategoryFoods.ToList());
            }
            else
            {
                return View(database.CategoryFoods.Where(s => s.NameCategoryFood.Contains(_name)).ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CategoryFood catefood)
        {
            database.CategoryFoods.Add(catefood);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(string id)
        {
            return View(database.CategoryFoods.Where(s => s.IDCFood == id).FirstOrDefault());
        }
        public ActionResult Edit(string id)
        {
            return View(database.CategoryFoods.Where(s=>s.IDCFood==id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(CategoryFood catefood)
        {
            database.Entry(catefood).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string id)
        {
            return View(database.CategoryFoods.Where(s => s.IDCFood == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(string id,CategoryFood catefood)
        {
            try
            {
                catefood = database.CategoryFoods.Where(s => s.IDCFood == id).FirstOrDefault();
                database.CategoryFoods.Remove(catefood);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("this data is using in other table, Error delete !");
            }
        }
        
    }
}