using MangaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangaStore.Controllers
{
    public class ThichController : Controller
    {
        // GET: Thich

        DataMangaDataContext db = new DataMangaDataContext();
        public List<Thich> Laytruyenthich()
        {
            List<Thich> lstThich = Session["Thich"] as List<Thich>;
            if (lstThich == null)
            {
                lstThich = new List<Thich>();
                Session["Thich"] = lstThich;
            }
            return lstThich;
        }
        public ActionResult ThemTruyenThich(int iMaTruyen, string strURL)
        {
            List<Thich> lstThich= Laytruyenthich();
            Thich sanpham = lstThich.Find(n => n.iMaTruyen == iMaTruyen);
            if (sanpham == null)
            {
                sanpham = new Thich(iMaTruyen);
                lstThich.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Thich> lstThich= Session["Thich"] as List<Thich>;
            if (lstThich != null)
            {
                iTongSoLuong = lstThich.Sum(n => n.iSoluong);
            }
            return iTongSoLuong;
        }
        public ActionResult Thich()
        {
            List<Thich> lstThich = Laytruyenthich();
            if (lstThich.Count == 0)
            {
                return RedirectToAction("Index", "Kind");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            return View(lstThich);
        }
        public ActionResult ThichPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            return PartialView();
        }
        public ActionResult XoaThich(int id)
        {
            List<Thich> lstThich = Laytruyenthich();
            Thich sanpham = lstThich.SingleOrDefault(n => n.iMaTruyen == id);
            if (sanpham != null)
            {
                lstThich.RemoveAll(n => n.iMaTruyen == id);
                return RedirectToAction("Thich");

            }
            if (lstThich.Count == 0)
            {
                return RedirectToAction("Index", "Kind");
            }
            return RedirectToAction("Thich");
        }
        public ActionResult Xoatatcatruyenthich()
        {
            List<Thich> lstThich = Laytruyenthich();
            lstThich.Clear();
            return RedirectToAction("Index", "Kind");
        }
    }
}