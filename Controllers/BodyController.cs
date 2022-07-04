using MangaStore.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangaStore.Controllers
{
    public class BodyController : Controller
    {
        DataMangaDataContext data = new DataMangaDataContext();
        private List<TRUYEN> Laytruyenmoi(int count)
        {
            return data.TRUYENs.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        // GET: Body
        public ActionResult Index(int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            var truyenmoi = Laytruyenmoi(22);   
            return View(truyenmoi.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Banchay()
        {
            var banchay = from bc in data.BanChays select bc;
            return PartialView(banchay);
        }
        public ActionResult Banchaytheo(int id, int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            var truyen = from t in data.TRUYENs
                         join bc in data.IDBanChays on t.MaTruyen equals bc.MaTruyen
                         where bc.MaBanChay == id   
                         select t;
            return View(truyen.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Noibat()
        {
            var noibat = from nb in data.NoiBats select nb;
            return PartialView(noibat);
        }
        public ActionResult Noibattheo(int id, int? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            var truyen = from b  in data.TRUYENs
                         join nb in data.IDNoiBats on b.MaTruyen equals nb.MaTruyen
                         where nb.MaNoiBat == id
                         select b;
            return View(truyen.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Chitiettruyen(int id)
        {
            var truyen= from s in data.TRUYENs where s.MaTruyen == id select s;
            return View(truyen.Single());
        }
    }
}