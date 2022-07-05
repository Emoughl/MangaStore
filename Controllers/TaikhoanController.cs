using MangaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangaStore.Controllers
{
    public class TaikhoanController : Controller
    {
        DataMangaDataContext db = new DataMangaDataContext();
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(db.Users.ToList());
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
        public ActionResult Create(User us)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                db.Users.InsertOnSubmit(us);
                db.SubmitChanges();

                return RedirectToAction("Index", "Taikhoan");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var us = from tk in db.Users where tk.MaUser == id select tk;
                return View(us.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                User us = db.Users.SingleOrDefault(n => n.MaUser == id);
                db.Users.DeleteOnSubmit(us);
                db.SubmitChanges();

                return RedirectToAction("Index", "Taikhoan");
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var us = from tk in db.Users where tk.MaUser == id select tk;
                return View(us.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                User us = db.Users.SingleOrDefault(n => n.MaUser == id);

                UpdateModel(us);
                db.SubmitChanges();
                return RedirectToAction("Index", "Taikhoan");
            }
        }
    }
}