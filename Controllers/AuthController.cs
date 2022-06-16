using MangaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangaStore.Controllers
{
    public class AuthController : BaseController
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Theloai()
        {
            var theloai = from tl in dataContext.THELOAIs select tl;
            return PartialView(theloai);
        }
    }
}