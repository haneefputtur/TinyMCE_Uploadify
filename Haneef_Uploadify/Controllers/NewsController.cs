using System;
using System.IO;
using System.Web.Mvc;
using Haneef_Uploadify.Models;

namespace Haneef_Uploadify.Controllers
{
    public class NewsController : Controller
    {
     
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(News model)
            {
            return View("NewsDisplay", model);
            }
        public ActionResult Upload()
            {
            var file = Request.Files["Filedata"];
            string extension = Path.GetExtension(file.FileName);
            string fileid = Guid.NewGuid().ToString();
            fileid = Path.ChangeExtension(fileid, extension);

            string savePath = Server.MapPath(@"~\Uploads\" + fileid);
            file.SaveAs(savePath);

            return Content(Url.Content(@"~\Uploads\" + fileid));
            }
	}
}