using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace SafetyCorner.Controllers
{
    public class ConferenceRecondController : Controller
    {
        //
        // GET: /ConferenceRecond/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Files/ConferenceReconds"), fileName);
                file.SaveAs(path);
            }
            return RedirectToAction("Index");
        }

    }
}
