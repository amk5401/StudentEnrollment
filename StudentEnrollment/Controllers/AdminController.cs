using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentEnrollment.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateTerm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTermSubmit(string code, string startDate, string endDate)
        {

            return RedirectToAction("FormResponse", "Admin", new { message = "success" });
        }

        public ActionResult FormResponse(string message)
        {
            ViewData["message"] = message;
            return View();
        }
    }
}