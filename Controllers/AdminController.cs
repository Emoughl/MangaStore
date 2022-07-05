using MangaStore.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangaStore.Controllers
{
    public class AdminController : Controller
    {
        DataMangaDataContext db = new DataMangaDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            var tendn = f["txtuser"];
            var matkhau = f["txtpass"];
            if (String.IsNullOrEmpty(tendn))
                ViewData["Loi1"] = "Vui lòng nhập tên đăng nhập";
            else if (String.IsNullOrEmpty(matkhau))
                ViewData["Loi2"] = "Vui lòng nhập mật khẩu";
            else
            {
                var ad = db.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không hợp lệ";
            }

            return View();
        }
        public ActionResult Truyen(int? page)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                int pagesize = 7;
                int pagenum = (page ?? 1);
                return View(db.TRUYENs.ToList().OrderByDescending(n => n.MaTruyen).ToPagedList(pagenum, pagesize));
            }
        }
        public ActionResult Chitiettruyen(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var truyen = from s in db.TRUYENs where s.MaTruyen == id select s;
                return View(truyen.SingleOrDefault());
            }
        }
        [HttpGet]
        public ActionResult Xoatruyen(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var truyen = from s in db.TRUYENs where s.MaTruyen == id select s;
                return View(truyen.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Xoatruyen")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                TRUYEN truyen = db.TRUYENs.SingleOrDefault(n => n.MaTruyen == id);
                db.TRUYENs.DeleteOnSubmit(truyen);
                db.SubmitChanges();
                return RedirectToAction("Truyen", "Admin");
            }
        }
        [HttpGet]
        public ActionResult Themmoitruyen()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                ViewBag.MaTL = new SelectList(db.THELOAIs.ToList().OrderBy(n => n.TenTL), "MaTL", "TenTL");
                ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoitruyen(TRUYEN truyen, HttpPostedFileBase fileUpload)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                if (fileUpload == null)
                {
                    ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                    return View();
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        var path = Path.Combine(Server.MapPath("~/assets/img/ImagesBody"), fileName);
                        if (System.IO.File.Exists(path))
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        else
                        {
                            fileUpload.SaveAs(path);
                        }
                        truyen.Anhbia = fileName;
                        db.TRUYENs.InsertOnSubmit(truyen);
                        db.SubmitChanges();
                    }
                    return RedirectToAction("Truyen", "Admin");
                }
            }
        }

        public ActionResult Suatruyen(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                TRUYEN truyen = db.TRUYENs.SingleOrDefault(n => n.MaTruyen == id);
                return View(truyen);
            }
        }
        [HttpPost, ActionName("Suatruyen")]
        public ActionResult Xacnhansua(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                TRUYEN truyen = db.TRUYENs.SingleOrDefault(n => n.MaTruyen == id);
                UpdateModel(truyen);
                db.SubmitChanges();
                return RedirectToAction("Truyen", "Admin");
            }
        }

    }
}