using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentEnrollment.Proxy;
using StudentEnrollment.Models;

namespace StudentEnrollment.Controllers
{
    public class LoginController : Controller
    {
        private APIProxy proxy = new APIProxy();

        [HttpGet]
        public ActionResult Index(string redirectAction = null, string redirectController = null, string error = null)
        {
            if (!(redirectAction != null && redirectAction != "") && !(redirectController != null && redirectController != ""))
            {
                redirectAction = "Index";
                redirectController = "Home";
            }

            if (Session["user"] != null && Session["role"] != null)
            {
                // TODO: Reidrect to whatever we consider the home page.
                return RedirectToAction(redirectAction, redirectController);
            }

            if (error != null && error != "")
            {
                ViewData["error"] = error;
            }

            ViewData["redirectAction"] = redirectAction;
            ViewData["redirectController"] = redirectController;
            return View("Index");
        }

        [HttpPost]
        public ActionResult Submit(string username, string password, string redirectAction, string redirectController)
        {
            User user = this.proxy.login(username, password);
            if (user != null)
            {
                Session["role"] = user.Role;
                Session["user"] = user;
                return RedirectToAction(redirectAction, redirectController);
            }
            else
            {
                return Index(redirectAction, redirectController, "Invalid username and/or password.");
            }

        }
    }
}