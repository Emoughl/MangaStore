﻿using MangaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Index()
        {
            var truyenmoi = Laytruyenmoi(12);
            return View(truyenmoi);
        }
        public ActionResult Theloai()
        {
            var theloai = from tl in data.THELOAIs select tl;
            return PartialView(theloai);
        }
        public ActionResult Truyentheotheloai(int id)
        {
            var truyen = from t in data.TRUYENs
                         join tl in data.MACOMICs on t.MaTruyen equals tl.MaTruyen
                         where tl.MaTL == id
                         select t;
            return View(truyen);

        }
        public ActionResult Nhaxuatban()
        {
            var nhaxuatban = from nxb in data.NHAXUATBANs select nxb;
            return PartialView(nhaxuatban);
        }
        public ActionResult TruyentheoNXB(int id)
        {
            var truyen1 = from n in data.TRUYENs
                          join nxb in data.MAQUANHEs on n.MaTruyen equals nxb.MaTruyen
                          where nxb.MaNXB == id
                          select n;
            return View(truyen1);
        }
        public ActionResult Gia()
        {
            var gia = from g in data.GIás select g;
            return PartialView(gia);
        }
        [HttpPost]
        public ActionResult Search(String a)
        {
            var truyen = data.TRUYENs.Where(s => s.TenTruyen.Contains(a)).ToList();
            return View(truyen);
        }
    }   
}