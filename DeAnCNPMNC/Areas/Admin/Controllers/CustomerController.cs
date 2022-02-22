using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeAnCNPMNC.Models;

namespace DeAnCNPMNC.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Admin/Customer
        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        public ActionResult Index(string _name)
        {
            if (_name == null)
            {
                return View(database.Customers.ToList());
            }
            else
            {
                return View(database.Customers.Where(s => s.AccountCustomer.Contains(_name)).ToList());
            }
        }
        //public ActionResult SelectCateCus()
        //{
        //    CategoryCustomer se_Cate = new CategoryCustomer();
        //    se_Cate.ListCateCus = database.CategoryCustomers.ToList<CategoryCustomer>();
        //    return PartialView(se_Cate);
        //}
        public ActionResult Create()
        {
            List<CategoryCustomer> list = database.CategoryCustomers.ToList();
            ViewBag.ListCategoryCus = new SelectList(list, "IDCateCus", "NameCateCus", "");
            Customer cus = new Customer();
            return View(cus);
        }
        [HttpPost]
        public ActionResult Create(Customer cus)
        {
            List<CategoryCustomer> list = database.CategoryCustomers.ToList();
            try
            {
                if (cus.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(cus.UploadImage.FileName);
                    string extent = Path.GetExtension(cus.UploadImage.FileName);
                    filename = filename + extent;
                    cus.ImageCustomer = "~/Content/images/" + filename;
                    cus.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                ViewBag.ListCategoryFood = new SelectList(list, "IDCFood", "NameCategoryFood", "");
                database.Customers.Add(cus);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Lỗi! vui lòng kiểm tra lại");
            }
        }
        public ActionResult Details(string acccus)
        {
            return View(database.Customers.Where(s => s.AccountCustomer == acccus).FirstOrDefault());
        }
        public ActionResult Edit(string acccus)
        {
            List<CategoryCustomer> list = database.CategoryCustomers.ToList();
            ViewBag.ListCategoryCus = new SelectList(list, "IDCateCus", "NameCateCus", "");
            return View(database.Customers.Where(s => s.AccountCustomer == acccus).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(Customer cus)
        {
            List<CategoryCustomer> list = database.CategoryCustomers.ToList();
            database.Entry(cus).State = System.Data.Entity.EntityState.Modified;
            if (cus.UploadImage != null)
            {
                string filename = Path.GetFileNameWithoutExtension(cus.UploadImage.FileName);
                string extent = Path.GetExtension(cus.UploadImage.FileName);
                filename = filename + extent;
                cus.ImageCustomer = "~/Content/images/" + filename;
                cus.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
            }
            ViewBag.ListCategoryCus = new SelectList(list, "IDCateCus", "NameCateCus", "");
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string acccus)
        {
            return View(database.Customers.Where(s => s.AccountCustomer == acccus).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(string acccus, Customer cus)
        {
            cus = database.Customers.Where(s => s.AccountCustomer == acccus).FirstOrDefault();
            database.Customers.Remove(cus);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}