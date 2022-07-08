using MangaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace MangaStore.Controllers
{
    public class KindController : Controller
    {
        DataMangaDataContext data = new DataMangaDataContext();
        // GET: Kind
        private List<TRUYEN> Laytruyenmoi(int count)
        {
            return data.TRUYENs.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        public ActionResult Index(int ? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var truyenmoi = Laytruyenmoi(20);
            return View(truyenmoi.ToPagedList(pageNum,pageSize));
        }
        public ActionResult Theloai()
        {
            var theloai = from tl in data.THELOAIs select tl;
            return PartialView(theloai);
        }
        public ActionResult Truyentheotheloai(int id,int ? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var truyen = from t in data.TRUYENs
                         join tl in data.MACOMICs on t.MaTruyen equals tl.MaTruyen
                         where tl.MaTL == id
                         select t;
            return View(truyen.ToPagedList(pageNum, pageSize));

        }
        public ActionResult Nhaxuatban()
        {
            var nhaxuatban = from nxb in data.NHAXUATBANs select nxb;
            return PartialView(nhaxuatban);
        }
        public ActionResult TruyentheoNXB(int id, int? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var truyen = from n in data.TRUYENs
                          join nxb in data.MAQUANHEs on n.MaTruyen equals nxb.MaTruyen
                          where nxb.MaNXB == id
                          select n;
            return View(truyen.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Gia()
        {
            var gia = from g in data.GIás select g;
            return PartialView(gia);
        }
        public ActionResult TruyentheoGiaTri(int id, int? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var truyen = from gt in data.TRUYENs
                          join g in data.GiaTruyens on gt.MaTruyen equals g.MaTruyen
                          where g.MaGia == id
                          select gt;
            return View(truyen.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Search(String a)
        {
            var truyen = data.TRUYENs.Where(s => s.TenTruyen.Contains(a)).ToList();
            return View(truyen);
        }
        public ActionResult Chitiettruyen(int id)
        {
            var truyen = from s in data.TRUYENs where s.MaTruyen == id select s;
            return View(truyen.Single());
        }
    }   
}