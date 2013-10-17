using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafetyCorner.ViewModels;
using SafetyCorner.Models.Repository;

namespace SafetyCorner.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        QuickLinkRepository db = new QuickLinkRepository();

        public ActionResult Index()
        {
            //PersonRepository db = new PersonRepository();
            //List<SafetyCorner.Models.Test> dt = db.GetAll().ToList();
            //System.Data.DataSet ds = db.Test();
            return View();
        }

        public ActionResult Navigation()
        {
            var quickLinks = db.GetSome(item => item.Enabled.Value.Equals(1));
            return PartialView(quickLinks);
        }

        public ActionResult Menu()
        {
            var menuItems = new List<MenuItem>();
            
            return PartialView();
        }

    }
}
