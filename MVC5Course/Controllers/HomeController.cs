using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //this.View("About").ExecuteResult(this.ControllerContext);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Unknow()
        {
            return View();
        }

        public ActionResult PartialAbout()
        {
            ViewBag.Message = "Your application description page.";
            if (Request.IsAjaxRequest())
            {
                return PartialView("About");
            }
            else
            {
                return View("About");
            }
            
        }

        public ActionResult SomeAction()
        {
            //return View();
            //return "<script>alert('建立成功能'); location.href='/';</script>";

            //return Content("<script>alert('建立成功能'); location.href='/';</script>");
            return PartialView("SuccessRedirect", "/");
        }
    }
}