using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeAnCNPMNC.Models;

namespace DeAnCNPMNC.Areas.Admin.Controllers
{
    [Authorize(Roles = "AD, NV")]
    public class EmployeeController : Controller
    {
        // GET: Admin/Employee
        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        [Authorize(Roles = "AD,NV")]
        public ActionResult Index(string _name)
        {
            if (_name == null)
            {
                return View(database.Employees.ToList());
            }
            else
            {
                return View(database.Employees.Where(s => s.AccountEmp.Contains(_name)).ToList());
            }
        }
        //public ActionResult SelectCateUser()
        //{
        //    CategoryUser se_Cate = new CategoryUser();
        //    se_Cate.ListCateUser = database.CategoryUsers.ToList<CategoryUser>();
        //    return PartialView(se_Cate);
        //}
        [Authorize(Roles = "AD")]
        public ActionResult Create()
        {
            List<CategoryUser> list = database.CategoryUsers.ToList();
            ViewBag.ListCategoryUser = new SelectList(list, "IDCUser", "NameCateUser", "");
            Employee emp = new Employee();
            return View(emp);
        }
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            List<CategoryUser> list = database.CategoryUsers.ToList();
            try
            {
                if (emp.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(emp.UploadImage.FileName);
                    string extent = Path.GetExtension(emp.UploadImage.FileName);
                    filename = filename + extent;
                    emp.ImageEmp = "~/Content/Image/" + filename;
                    emp.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                ViewBag.ListCategoryUser = new SelectList(list, "IDCUser", "NameCateUser", "");
                database.Employees.Add(emp);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "AD,NV")]
        public ActionResult Details(string accemp)
        {
            return View(database.Employees.Where(s => s.AccountEmp == accemp).FirstOrDefault());
        }
        [Authorize(Roles = "AD,NV")]
        public ActionResult Edit(string accemp)
        {
            List<CategoryUser> list = database.CategoryUsers.ToList();
            ViewBag.ListCategoryUser = new SelectList(list, "IDCUser", "NameCateUser", "");
            return View(database.Employees.Where(s => s.AccountEmp == accemp).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            List<CategoryUser> list = database.CategoryUsers.ToList();
            database.Entry(emp).State = System.Data.Entity.EntityState.Modified;
            try
            {
                if (emp.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(emp.UploadImage.FileName);
                    string extent = Path.GetExtension(emp.UploadImage.FileName);
                    filename = filename + extent;
                    emp.ImageEmp = "~/Content/Image/" + filename;
                    emp.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                ViewBag.ListCategoryUser = new SelectList(list, "IDCUser", "NameCateUser", "");
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "AD")]
        public ActionResult Delete(string accemp)
        {
            return View(database.Employees.Where(s => s.AccountEmp == accemp).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(string accemp, Employee emp)
        {
            try
            {
                emp = database.Employees.Where(s => s.AccountEmp == accemp).FirstOrDefault();
                database.Employees.Remove(emp);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete Employee");
            }   
        }
    }
}