using MangaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangaStore.Controllers
{
    public class AuthController : Controller
    {
        DataMangaDataContext db = new DataMangaDataContext();
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection hz)
        {
            var email = hz["Email"];
            var matkhau = hz["Matkhau"];
            if (String.IsNullOrEmpty(email))
                ViewData["Loi1"] = "Vui lòng nhập tên đăng nhập";
            else if (String.IsNullOrEmpty(matkhau))
                ViewData["Loi2"] = "Vui lòng nhập mật khẩu";
            else
            {
                var us = db.Users.SingleOrDefault(n => n.Email == email && n.Matkhau == matkhau);
                if (us != null)
                {
                    Session["Taikhoan"] = us;
                    return RedirectToAction("Index", "Auth");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không hợp lệ";
            }

            return View();
        }
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(FormCollection collection, User us)
        {
            var email= collection["email"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";

            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                us.Email = email;
                us.Matkhau = matkhau;
                db.Users.InsertOnSubmit(us);
                db.SubmitChanges();
                return RedirectToAction("Dangnhap", "Auth");
            }
            return this.Dangky();
        }
        public ActionResult Quenmatkhau()
        {
            return View();
        }
    }
}
