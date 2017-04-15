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
            IProxy p = new APIProxy();
            return View(p.getCourseList());
        }

        public ActionResult CourseDetail(int courseID)
        {
            ViewData["Title"] = "Course Detail";
            IProxy p = new APIProxy();
            Course course = p.getCourse(courseID);
            //CourseAndSectionModel courseAndSectionModel = new CourseAndSectionModel(course, p.getCourseSections(course));
            return View(course);
        }

        [HttpPost]
        public ActionResult EditCourse(Course model)
        {
            if (ModelState.IsValid)
            {
                IProxy p = new APIProxy();
                //p.createCourse(model);//p.editCourse(model);
                return RedirectToAction("CourseDetail", new { courseId = model.ID });
            } else
            {
                return RedirectToAction("CourseList");
            }
        }
    }
}
