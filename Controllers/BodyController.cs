using MangaStore.Models;
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
        public ActionResult Index()
        {
            var truyenmoi = Laytruyenmoi(21);
            return View(truyenmoi);
        }
    }
}