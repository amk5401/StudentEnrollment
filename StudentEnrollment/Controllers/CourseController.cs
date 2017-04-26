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
        // GET: /<controller>/
        public ActionResult CourseList()
        {
            ViewData["Title"] = "Course List";
            APIProxy p = new APIProxy();
            return View(p.getCourseList());
        }

        public ActionResult CourseDetail(int courseID)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login", new { redirectAction = "CourseDetail", redirectController = "Course", courseID = courseID });
            }
            ViewData["Title"] = "Course Detail";
            APIProxy p = new APIProxy();
            Course course = p.getCourse(courseID);
            ViewData["Role"] = Session["role"];
            //CourseAndSectionModel courseAndSectionModel = new CourseAndSectionModel(course, p.getCourseSections(course));
            return View(course);
        }

        [HttpPost]
        public ActionResult EditCourse(Course model)
        {
            if (ModelState.IsValid)
            {
                APIProxy p = new APIProxy();
                p.updateCourse(model);//p.createCourse(model);
                return RedirectToAction("CourseDetail", new { courseId = model.ID });
            } else
            {
                return RedirectToAction("CourseList");
            }
        }
    }
}
