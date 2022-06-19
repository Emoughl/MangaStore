using MangaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
            ViewBag.Thongbao = Session["DangNhapThongBao"];
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
                Session["DangNhapThongBao"] = "Đã Đăng Ký Thành Công";
                return RedirectToAction("Dangnhap", "Auth");
            }
            return this.Dangky();
        }
        [HttpGet]
        public ActionResult Quenmatkhau()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Quenmatkhau(FormCollection qmk, User us)
        {
            DataMangaDataContext db = new DataMangaDataContext();
            var user = db.Users
             .Where(m => m.Email == qmk["Email"])
            .SingleOrDefault() ;
            if (user == null)
            {
                ViewBag.Thongbao = "Email này chưa được đăng ký , hãy thử lại";
            }
            else
            {
                ViewBag.Thongbao = "Đã gửi mật khẩu vào Email của bạn, vui lòng check Email";
                String body = "Mật khẩu của bạn là: " + user.Matkhau;
                MailMessage message = new MailMessage("hoangdanghuy222@gmail.com", qmk["Email"], "Quên mật khẩu", body);
                try
                {
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential("hoangdanghuy222@gmail.com", "hjwkvlouhsqozewd");
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    ViewBag.error = ex;
                }
            }
            return Quenmatkhau();
        }
    }
}
