using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using StudentEnrollment.Proxy;
using StudentEnrollment.Models;

namespace StudentEnrollment.Controllers
{
    public class CourseController : Controller
    {
        private APIProxy proxy = new APIProxy();

        private bool loggedIn()
        {
            if (Session["user"] == null) return false; else return true;
        }

        private bool checkPermission(string role)
        {
            if (Session["user"] != null && Session["role"] != null && String.Equals(role, (string)Session["Role"], StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }


        // GET: /<controller>/
        public ActionResult CourseList()
        {
            ViewData["Title"] = "Course List";
            return View(proxy.getCourseList());
        }

        public ActionResult CourseDetail(int courseID)
        {
            if (!loggedIn())
            {
                return RedirectToAction("Index", "Login", new { redirectAction = "CourseList", redirectController = "Course"});
            }

            Course course = proxy.getCourse(courseID);
            ViewData["Title"] = "Course Detail"; 
            ViewData["Role"] = Session["role"];
            return View(course);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateSection(string courseID)
        {
            if(!loggedIn())
            {
                return RedirectToAction("Index", "Login", new { redirectAction = "CourseList", redirectController = "Course"});
            }
            if(!checkPermission("professor") && !checkPermission("admin"))
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            int id;
            try
            {
                id = Convert.ToInt32(courseID);
            }
            catch(Exception e)
            {
                id = -1;
            }

            if (id != -1 && proxy.getCourse(id) != null)
            {
                return RedirectToAction("Create", "Section", new { courseID = courseID });
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditCourse(Course model)
        {
            if (ModelState.IsValid)
            {
                proxy.updateCourse(model);//p.createCourse(model);
                return RedirectToAction("CourseDetail", new { courseId = model.ID });
            } else
            {
                return RedirectToAction("CourseList");
            }
        }

        [HttpPost]
        public ActionResult RemoveCourse(int courseID)
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "CourseList", redirectController = "Course"});
            if (!checkPermission("admin")) return RedirectToAction("AccessDenied", "Home");

            //proxy.deleteCourse(courseID);
            // Check?
            return CourseList();
        }
    }
}
