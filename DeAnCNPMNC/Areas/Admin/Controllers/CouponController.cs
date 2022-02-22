using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeAnCNPMNC.Models;

namespace DeAnCNPMNC.Areas.Admin.Controllers
{
    public class CouponController : Controller
    {
        // GET: Admin/Coupon
        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        public ActionResult Index(string _idcoupon)
        {
            if (_idcoupon == null)
            {
                return View(database.Coupons.ToList());
            }
            else
            {
                return View(database.Coupons.Where(s => s.IDCoupon.Contains(_idcoupon)).ToList());
            }
        }
        public ActionResult Create()
        {
            Coupon cou = new Coupon();
            return View(cou);
        }
        [HttpPost]
        public ActionResult Create(Coupon cou)
        {
            try
            {
                if (cou.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(cou.UploadImage.FileName);
                    string extent = Path.GetExtension(cou.UploadImage.FileName);
                    filename = filename + extent;
                    cou.ImageCoupon = "~/Content/Image/" + filename;
                    cou.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                database.Coupons.Add(cou);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Details(string idcou)
        {
            return View(database.Coupons.Where(s => s.IDCoupon == idcou).FirstOrDefault());
        }
        public ActionResult Edit(string idcou)
        {
            return View(database.Coupons.Where(s => s.IDCoupon == idcou).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(Coupon cou)
        {
            if (cou.UploadImage != null)
            {
                string filename = Path.GetFileNameWithoutExtension(cou.UploadImage.FileName);
                string extent = Path.GetExtension(cou.UploadImage.FileName);
                filename = filename + extent;
                cou.ImageCoupon = "~/Content/Image/" + filename;
                cou.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
            }
            database.Entry(cou).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string idcou)
        {
            return View(database.Coupons.Where(s => s.IDCoupon == idcou).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(string idcou, Coupon cou)
        {
            cou = database.Coupons.Where(s => s.IDCoupon == idcou).FirstOrDefault();
            database.Coupons.Remove(cou);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}