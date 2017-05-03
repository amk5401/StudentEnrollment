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

        private bool isAdmin()
        {
            if (Session["user"] != null && Session["role"] != null && String.Equals("admin", (string)Session["Role"], StringComparison.OrdinalIgnoreCase))
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
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "CourseDetail", redirectController = "Course", courseID = courseID });
            if (!isAdmin()) return RedirectToAction("AccessDenied", "Home");

            //proxy.deleteCourse(courseID);
            // Check?
            return CourseList();
        }
    }
}
