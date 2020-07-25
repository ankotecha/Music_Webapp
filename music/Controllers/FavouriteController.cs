using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace music.Controllers
{
    public class FavouriteController : Controller
    {
        // GET: Favourite
        public ActionResult Index()
        {
            return View();
        }
    }
}