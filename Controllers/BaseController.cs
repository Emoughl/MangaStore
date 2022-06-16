using MangaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangaStore.Controllers
{
    public class BaseController : Controller
    {
        protected DataMangaDataContext dataContext = new DataMangaDataContext();

        public BaseController()
        {
            ViewBag.DataContext = dataContext;
            ViewBag.TheLoai = dataContext.THELOAIs;
        }
    }
}