using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafetyCorner.Models.Repository;

namespace SafetyCorner.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/


        public ActionResult Index()
        {
            //PersonRepository db = new PersonRepository();
            //List<SafetyCorner.Models.Test> dt = db.GetAll().ToList();
            //System.Data.DataSet ds = db.Test();
            return View();
        }

        public ActionResult QuickLink()
        {
            QuickLinkRepository db = new QuickLinkRepository();
            var quickLinks = db.GetSome(item => item.Enabled.Value.Equals(1)).OrderBy(item => item.Sort_Num);
            return PartialView(quickLinks);
        }

        public ActionResult Menu()
        {
            MenuListRepository db = new MenuListRepository();
            var menuItems = db.GetSome(item => item.Enabled.Value.Equals(1)).OrderBy(item => item.Title_ID).OrderBy(item => item.Sort_Num);
            return PartialView(menuItems);
        }

    }
}
