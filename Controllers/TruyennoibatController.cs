using MangaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangaStore.Controllers
{
    public class TruyennoibatController : Controller
    {
        DataMangaDataContext db = new DataMangaDataContext();
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(db.IDNoiBats.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View();
        }
        [HttpPost]
        public ActionResult Create(IDNoiBat noibat)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                db.IDNoiBats.InsertOnSubmit(noibat);
                db.SubmitChanges();

                return RedirectToAction("Index", "Truyennoibat");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var noibat = from nb in db.IDNoiBats where nb.idNoiBat1 == id select nb;
                return View(noibat.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                IDNoiBat noibat = db.IDNoiBats.SingleOrDefault(n => n.idNoiBat1 == id);
                db.IDNoiBats.DeleteOnSubmit(noibat);
                db.SubmitChanges();

                return RedirectToAction("Index", "Truyennoibat");
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var noibat = from nb in db.IDNoiBats where nb.idNoiBat1 == id select nb;
                return View(noibat.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                IDNoiBat noibat = db.IDNoiBats.SingleOrDefault(n => n.idNoiBat1 == id);

                UpdateModel(noibat);
                db.SubmitChanges();
                return RedirectToAction("Index", "Truyennoibat");
            }
        }
    }
}