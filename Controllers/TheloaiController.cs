using MangaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangaStore.Controllers
{
    public class TheloaiController : Controller
    {
        DataMangaDataContext db = new DataMangaDataContext();
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(db.THELOAIs.ToList());
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
        public ActionResult Create(THELOAI theloai)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                db.THELOAIs.InsertOnSubmit(theloai);
                db.SubmitChanges();

                return RedirectToAction("Index", "Theloai");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var theloai = from tl in db.THELOAIs where tl.MaTL == id select tl;
                return View(theloai.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                THELOAI theloai = db.THELOAIs.SingleOrDefault(n => n.MaTL == id);
                db.THELOAIs.DeleteOnSubmit(theloai);
                db.SubmitChanges();

                return RedirectToAction("Index", "Theloai");
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var theloai = from tl in db.THELOAIs where tl.MaTL == id select tl;
                return View(theloai.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                THELOAI theloai = db.THELOAIs.SingleOrDefault(n => n.MaTL == id);

                UpdateModel(theloai);
                db.SubmitChanges();
                return RedirectToAction("Index", "Theloai");
            }
        }
    }
}