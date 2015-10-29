using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeChatService.Demo.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult ScanQrCodeDemo()
        {
            return View();
        }

        public ActionResult Photo()
        {
            return View();
        }
        public ActionResult NetworkType()
        {
            return View();
        }
        public ActionResult Location()
        {
            return View();
        }
    }
}