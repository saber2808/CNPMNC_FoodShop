using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeAnCNPMNC.Models;

namespace DeAnCNPMNC.Areas.Admin.Controllers
{
    public class CategoryCustomerController : Controller
    {
        // GET: Admin/CategoryCus
        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        public ActionResult Index(string _name)
        {
            if (_name == null)
            {
                return View(database.CategoryCustomers.ToList());
            }
            else
            {
                return View(database.CategoryCustomers.Where(s => s.IDCateCus.Contains(_name)).ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CategoryCustomer catecus)
        {
            database.CategoryCustomers.Add(catecus);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(string id)
        {
            return View(database.CategoryCustomers.Where(s => s.IDCateCus == id).FirstOrDefault());
        }
        public ActionResult Edit(string id)
        {
            return View(database.CategoryCustomers.Where(s => s.IDCateCus == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(CategoryCustomer catecus)
        {
            database.Entry(catecus).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string id)
        {
            return View(database.CategoryCustomers.Where(s => s.IDCateCus == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(string id, CategoryCustomer catecus)
        {
            try
            {
                catecus = database.CategoryCustomers.Where(s => s.IDCateCus == id).FirstOrDefault();
                database.CategoryCustomers.Remove(catecus);
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