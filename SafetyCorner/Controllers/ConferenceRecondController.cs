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
        //http://www.abeautifulsite.net/blog/2013/08/whipping-file-inputs-into-shape-with-bootstrap-3/
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
                if (!Directory.Exists(Server.MapPath("~/Files/ConferenceReconds")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Files/ConferenceReconds"));
                }
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
                    TempData["message"] += e.Message;
                }
            }
            return RedirectToAction("Index");
        }

        public JsonResult Query(string fileDate, string fileName, int? page, int? rows,string sidx, string sord)
        {
            FileListRepository db = new FileListRepository();
            var data = db.GetAll();
            if (!string.IsNullOrEmpty(fileDate))
            {
                DateTime date = DateTime.Parse(fileDate);
                data = db.GetSome(data, item => item.Create_Date.Value.Year.Equals(date.Year)
                                                && item.Create_Date.Value.Month.Equals(date.Month)
                                                && item.Create_Date.Value.Day.Equals(date.Day));
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                data = db.GetSome(data, item => item.File_Name.IndexOf(fileName) > -1);
            }

            var field = typeof(FileList).GetProperty(sidx);

            var dataList = data.ToList();
            if (sord.Equals("asc"))
            {
                dataList = dataList.OrderBy(item => field.GetValue(item, null)).ToList();
            }
            else if(sord.Equals("desc"))
            {
                dataList = dataList.OrderByDescending(item => field.GetValue(item, null)).ToList();
            }
            //data = data.OrderByDescending(item => item.Create_Date);
            int pageSize = rows.HasValue ? rows.Value : 10;
            int pageNum = page.HasValue ? page.Value : 1;
            int totalRecords = dataList.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = pageNum,
                records = totalRecords,
                rows = dataList.Skip((pageNum - 1) * pageSize).Take(pageSize)
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
            //return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }

        [HttpPost]
        public ActionResult AsyncFileUpload(HttpPostedFileBase file, string fileId)
        {
            //var fileName = Path.GetFileName(file.FileName);
            //var path = Path.Combine(Server.MapPath("~/Files/ConferenceReconds"), fileName);
            //file.SaveAs(path);
            return Content(file.FileName + " " + file.ContentLength);
        }

    }
}
