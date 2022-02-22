using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeAnCNPMNC.Models;

namespace DeAnCNPMNC.Areas.Admin.Controllers
{
    
    public class FoodsController : Controller
    {

        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        // GET: Admin/Foods
        public static List<Food> SelectAllArticle()
        {
            var rtn = new List<Food>();
            using (var context = new CNPMFastFoodEntities())
            {
                foreach (var item in context.Foods)
                {
                    rtn.Add(new Food
                    {
                        IDFood = item.IDFood,
                        NameFood = item.NameFood,
                        ImageFood = item.ImageFood,
                        PriceFood = item.PriceFood,
                        StatusFood = item.StatusFood,
                        CateFood = item.CategoryFood.NameCategoryFood,
                        QuantityFood=item.QuantityFood,
                        DesFood=item.DesFood,
                        Trending = item.Trending
                    });
                }
            }
            return rtn;
        }
        public ActionResult Index(string food)
        {
            if (food == null)
            {
                //var foodlist = database.Foods.OrderByDescending(x => x.NameFood);
                
                return View(SelectAllArticle().ToList());
            }
            else
            {
                var foodlist = database.Foods.OrderByDescending(x => x.IDFood).Where(s=>s.CategoryFood.NameCategoryFood==food);
                return View(foodlist);
            }
        }
        //public ActionResult SelectCate()
        //{
        //    CategoryFood se_Cate = new CategoryFood();
        //    se_Cate.ListCate = database.CategoryFoods.ToList<CategoryFood>();
        //    return PartialView(se_Cate);
        //}
        public ActionResult Create()
        {
            List<CategoryFood> list = database.CategoryFoods.ToList();
            ViewBag.ListCategoryFood = new SelectList(list, "IDCFood", "NameCategoryFood", "");
            Food food = new Food();
            return View(food);
        }
        [HttpPost]
        public ActionResult Create(Food food)
        {
            List<CategoryFood> list = database.CategoryFoods.ToList();
            try
            {
                if (food.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(food.UploadImage.FileName);
                    string extent = Path.GetExtension(food.UploadImage.FileName);
                    filename = filename + extent;
                    food.ImageFood = "~/Content/images/" + filename;
                    food.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                ViewBag.ListCategoryFood = new SelectList(list, "IDCFood", "NameCategoryFood", "");
                database.Foods.Add(food);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Lỗi! vui lòng kiểm tra lại");
            }
        }
        public ActionResult Details(string id)
        {
            return View(database.Foods.Where(s => s.IDFood == id).FirstOrDefault());
        }
        public ActionResult Edit(string id)
        {
            List<CategoryFood> list = database.CategoryFoods.ToList();
            ViewBag.ListCategoryFood = new SelectList(list, "IDCFood", "NameCategoryFood", "");
            return View(database.Foods.Where(s => s.IDFood == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(Food food, string id)
        {
            List<CategoryFood> list = database.CategoryFoods.ToList();
            database.Entry(food).State = System.Data.Entity.EntityState.Modified;
            if (food.UploadImage != null)
            {
                string filename = Path.GetFileNameWithoutExtension(food.UploadImage.FileName);
                string extent = Path.GetExtension(food.UploadImage.FileName);
                filename = filename + extent;
                food.ImageFood = "~/Content/images/" + filename;
                food.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
            }
            ViewBag.ListCategoryFood = new SelectList(list, "IDCFood", "NameCategoryFood", "");
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string id)
        {
            return View(database.Foods.Where(s => s.IDFood == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(string id,Food food)
        {
            food=database.Foods.Where(s=>s.IDFood==id).FirstOrDefault();
            database.Foods.Remove(food);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}