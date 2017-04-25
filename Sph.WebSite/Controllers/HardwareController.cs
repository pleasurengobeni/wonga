using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sph.WebSite.DataAccess;

namespace Sph.WebSite.Controllers
{
    public class HardwareController : Controller
    {
        // GET: Hardware
        public ActionResult Index()
        {
            var context = new SphDataContext();

            return View(context.StockItems.ToList());
        }
    }
}