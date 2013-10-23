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

        [HttpPost]
        public JsonResult Query(string name,string age)
        {
            List<GridRowData> data = new List<GridRowData>();
            data.Add(new GridRowData() { name = "Lonka", age = "28", href = "/ConferenceRecond", hrefText = "GoL" });
            data.Add(new GridRowData() { name = "Sharon", age = "38", href = "#", hrefText = "GoS" });
            data.Add(new GridRowData() { name = "Donna", age = "48", href = "#", hrefText = "GoD" });
            data.Add(new GridRowData() { name = "Jelly", age = "58", href = "#", hrefText = "GoJ" });
            data.Add(new GridRowData() { name = "James", age = "68", href = "#", hrefText = "GoJ" });

            if (!string.IsNullOrEmpty(name))
            {
                data = data.Where(item => item.name.IndexOf(name) > -1).ToList();
            }
            if (!string.IsNullOrEmpty(age))
            {
                data = data.Where(item => int.Parse(item.age) > int.Parse(age)).ToList();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }

    public class GridRowData
    {
        public string name { get; set; }
        public string age { get; set; }
        public string href { get; set; }
        public string hrefText { get; set; }
    }
}
