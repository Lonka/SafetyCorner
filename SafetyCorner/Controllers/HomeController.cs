using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafetyCorner.ViewModels;
namespace SafetyCorner.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Navigation()
        {
            var NavigationItems = new List<NavigationItem>()
            {
                new NavigationItem(){ Title="勞委會", Href="http://www.cla.gov.tw/" , Target="_blank", Text="勞委會" },
                new NavigationItem(){ Title="勞研所", Href="http://www.iosh.gov.tw/" , Target="_blank", Text="勞研所" },
                new NavigationItem(){ Title="北檢所", Href="http://www.nlio.gov.tw/" , Target="_blank", Text="北檢所" },
                new NavigationItem(){ Title="消防署", Href="http://www.nfa.gov.tw/" , Target="_blank", Text="消防署" },
                new NavigationItem(){ Title="全球化學品調和制度", Href="http://ghs.cla.gov.tw/" , Target="_blank", Text="全球化學品調和制度" },
                new NavigationItem(){ Title="全國資料庫", Href="http://law.moj.gov.tw/" , Target="_blank", Text="全國資料庫" }
            };
            return PartialView(NavigationItems);
        }

        public ActionResult Menu()
        {
            var menuItems = new List<MenuItem>();
            
            return PartialView();
        }

    }
}
