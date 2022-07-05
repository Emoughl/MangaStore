using MangaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangaStore.Controllers
{
    public class NhaxuatbanController : Controller
    {
        DataMangaDataContext db = new DataMangaDataContext();
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(db.NHAXUATBANs.ToList());
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
        public ActionResult Create(NHAXUATBAN nhaxuatban)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                db.NHAXUATBANs.InsertOnSubmit(nhaxuatban);
                db.SubmitChanges();

                return RedirectToAction("Index", "Nhaxuatban");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var nhaxuatban = from nxb in db.NHAXUATBANs where nxb.MaNXB == id select nxb;
                return View(nhaxuatban.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                NHAXUATBAN nhaxuatban = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
                db.NHAXUATBANs.DeleteOnSubmit(nhaxuatban);
                db.SubmitChanges();

                return RedirectToAction("Index", "Nhaxuatban");
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var nhaxuatban = from nxb in db.NHAXUATBANs where nxb.MaNXB == id select nxb;
                return View(nhaxuatban.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                NHAXUATBAN nhaxuatban = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);

                UpdateModel(nhaxuatban);
                db.SubmitChanges();
                return RedirectToAction("Index", "Nhaxuatban");
            }
        }
    }
}