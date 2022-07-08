using MangaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangaStore.Controllers
{
    public class TruyentheotheloaiController : Controller
    {
        DataMangaDataContext db = new DataMangaDataContext();
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(db.MACOMICs.ToList());
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
        public ActionResult Create(MACOMIC truyentheotheloai)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                db.MACOMICs.InsertOnSubmit(truyentheotheloai);
                db.SubmitChanges();

                return RedirectToAction("Index", "Truyentheotheloai");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var truyentheotheloai = from tttl in db.MACOMICs where tttl.MC == id select tttl;
                return View(truyentheotheloai.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                MACOMIC truyentheotheloai = db.MACOMICs.SingleOrDefault(n => n.MC == id);
                db.MACOMICs.DeleteOnSubmit(truyentheotheloai);
                db.SubmitChanges();

                return RedirectToAction("Index", "Truyentheotheloai");
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var truyentheotheloai = from tttl in db.MACOMICs where tttl.MC == id select tttl;
                return View(truyentheotheloai.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                MACOMIC truyentheotheloai = db.MACOMICs.SingleOrDefault(n => n.MC == id);

                UpdateModel(truyentheotheloai);
                db.SubmitChanges();
                return RedirectToAction("Index", "Truyentheotheloai");
            }
        }
    }
}