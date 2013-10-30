using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using SafetyCorner.Models.Repository;
using SafetyCorner.Models;

namespace SafetyCorner.Controllers
{
    public class ConferenceRecondController : Controller
    {
        //
        // GET: /ConferenceRecond/
        //TODO Datepicker
        //http://mgcrea.github.io/angular-strap/#/tooltip
        //TODO Linq 比較日期
        //TODO 上傳大小限制
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
                try
                {
                    file.SaveAs(path);
                    FileList fileEntity = new FileList();
                    fileEntity.Create_Date = DateTime.Now;
                    fileEntity.Create_User = "Lonka";
                    fileEntity.Enabled = 1;
                    fileEntity.File_Name = fileName;
                    fileEntity.File_Root = @"\Files\ConferenceReconds\" + fileName;
                    fileEntity.File_Size = file.ContentLength;
                    FileListRepository db = new FileListRepository();
                    db.Create(fileEntity);
                }
                catch (Exception e)
                {
                    //TODO Exception
                }
            }
            return RedirectToAction("Index");
        }

        public string Query(string fileDate, string fileName)
        {
            FileListRepository db = new FileListRepository();
            var data = db.GetAll();
            if (!string.IsNullOrEmpty(fileDate))
            {
                string fileDateString = DateTime.Parse(fileDate).ToString("yyyyMMdd");
                data = db.GetSome(data, item => item.Create_Date.Value.Equals(fileDateString));
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                data = db.GetSome(data, item => item.File_Name.IndexOf(fileName) > -1);
            }

            data = data.OrderByDescending(item => item.Create_Date);
            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }

        [HttpPost]
        public ActionResult AsyncFileUpload(HttpPostedFileBase file,string fileId)
        {
            //var fileName = Path.GetFileName(file.FileName);
            //var path = Path.Combine(Server.MapPath("~/Files/ConferenceReconds"), fileName);
            //file.SaveAs(path);
            return Content(file.FileName + " " + file.ContentLength);
        }

    }
}
