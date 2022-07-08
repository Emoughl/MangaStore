using MangaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangaStore.Controllers
{
    public class TruyentheonhaxuatbanController : Controller
    {
        DataMangaDataContext db = new DataMangaDataContext();
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(db.MAQUANHEs.ToList());
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
        public ActionResult Create(MAQUANHE truyentheonhaxuatban)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                db.MAQUANHEs.InsertOnSubmit(truyentheonhaxuatban);
                db.SubmitChanges();

                return RedirectToAction("Index", "Truyentheonhaxuatban");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var truyentheonhaxuatban = from ttnxb in db.MAQUANHEs where ttnxb.MaQH == id select ttnxb;
                return View(truyentheonhaxuatban.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                MAQUANHE truyentheonhaxuatban = db.MAQUANHEs.SingleOrDefault(n => n.MaQH == id);
                db.MAQUANHEs.DeleteOnSubmit(truyentheonhaxuatban);
                db.SubmitChanges();

                return RedirectToAction("Index", "Truyentheonhaxuatban");
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var truyentheonhaxuatban = from ttnxb in db.MAQUANHEs where ttnxb.MaQH == id select ttnxb;
                return View(truyentheonhaxuatban.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                MAQUANHE truyentheonhaxuatban = db.MAQUANHEs.SingleOrDefault(n => n.MaQH == id);

                UpdateModel(truyentheonhaxuatban);
                db.SubmitChanges();
                return RedirectToAction("Index", "Truyentheotheloai");
            }
        }
    }
}