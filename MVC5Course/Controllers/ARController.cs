using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewTest()
        {
            string model = "My Data";
            return View((object)model);
        }

        public ActionResult PartialViewTest()
        {
            string model = "My Data";
            return PartialView("ViewTest",(object)model);
        }

        public ActionResult FileTest(string dl)
        {
            if(string.IsNullOrEmpty(dl) && dl!="1")
            {
                return File(Server.MapPath("~/App_Data/FIFA123.jpg"), "image/jpeg");
            }
            else
            {
                return File(Server.MapPath("~/App_Data/FIFA123.jpg"), "image/jpeg", "FIFA.jpg");
            }

        }


    }
}