using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using StudentEnrollment.Proxy;

namespace StudentEnrollment.Controllers
{
    public class CourseController : Controller
    {
        // GET: /<controller>/
        public ActionResult CourseList()
        {
            ViewData["Title"] = "Course List";

            int[] none = new int[] { };
            int[] se = new int[] {3, 4};
            Models.Course c1 = new StudentEnrollment.Models.Course(1, "SWEN-344", "Web Engineering", 3, 2, true);
            Models.Course c2 = new StudentEnrollment.Models.Course(2, "CS-420", "Data Mining", 3, 3, true);
            Models.Course c3 = new StudentEnrollment.Models.Course(3, "SWEN-261", "Intro to SE", 3, 1, true);
            Models.Course c4 = new StudentEnrollment.Models.Course(4, "CS-262", "Engineering of Software Subsystems", 3, 1, true);
            List<StudentEnrollment.Models.Course> courses = new List<Models.Course> { c1, c2, c3, c4 };


            IProxy p = new APIProxy();
            ViewData["Courses"] = courses;//p.getCourseList();
            return View();
        }
    }
}
