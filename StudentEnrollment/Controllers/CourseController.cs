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

        private bool hasPermission(string role)
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
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login", new { redirectAction = "CourseDetail", redirectController = "Course", courseID = courseID });
            }
            ViewData["Title"] = "Course Detail";
            Course course = proxy.getCourse(courseID);
            ViewData["Role"] = Session["role"];
            //CourseAndSectionModel courseAndSectionModel = new CourseAndSectionModel(course, p.getCourseSections(course));
            return View(course);
        }

        [HttpPost]
        public ActionResult SubmitCourse(Course model, string operation)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("CourseList");
            }

            if (operation.Equals("delete"))
            {
                return RemoveCourse(model);
            }
            else if (operation.Equals("save"))
            {
                return EditCourse(model);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        private ActionResult EditCourse(Course model)
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "CourseDetail", redirectController = "Course", courseID = model.ID });
            if (!(hasPermission("admin") || hasPermission("professor"))) return RedirectToAction("AccessDenied", "Home");
            proxy.updateCourse(model);
            return RedirectToAction("CourseDetail", new { courseId = model.ID });
        }

        private ActionResult RemoveCourse(Course model)
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "CourseDetail", redirectController = "Course", courseID = model.ID });
            if (!hasPermission("admin")) return RedirectToAction("AccessDenied", "Home");
            bool success = proxy.deleteCourse(model.ID);
            return RedirectToAction("CourseList");
        }
    }
}
