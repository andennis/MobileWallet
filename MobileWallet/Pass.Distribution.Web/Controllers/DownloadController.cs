using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace Pass.Distribution.Web.Controllers
{
    public class DownloadController : Controller
    {
        //
        // GET: /PassDistribution/
        public FileResult Download(string id)
        {
            //TODO temp code just for test
            string path = HttpContext.Server.MapPath("~/App_Data/TextFile1.txt");
            return File(path, "text/plain");
        }

        public FileResult DownloadPass(string passToken)
        {
            //TODO temp code just for test
            string path = HttpContext.Server.MapPath("~/App_Data/Test1.pkpass");
            return File(path, "application/vnd.apple.pkpass");
        }
	}
}