using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StudentEnrollment.Controllers
{
    public class CourseController : Controller
    {
        // GET: /<controller>/
        public IActionResult CourseList()
        {
            ViewData["Title"] = "Course List";

            int[] none = new int[] { };
            int[] se = new int[] {3, 4};
            Models.Course c1 = new StudentEnrollment.Models.Course(1, "SWEN-344", "Web Engineering", 3, 2, se);
            Models.Course c2 = new StudentEnrollment.Models.Course(2, "CS-420", "Data Mining", 3, 3, none);
            Models.Course c3 = new StudentEnrollment.Models.Course(3, "SWEN-261", "Intro to SE", 3, 1, none);
            Models.Course c4 = new StudentEnrollment.Models.Course(4, "CS-262", "Engineering of Software Subsystems", 3, 1, none);
            List<StudentEnrollment.Models.Course> courses = new List<Models.Course> { c1, c2, c3, c4 };


            Proxy p = new APIProxy();
            ViewData["Courses"] = courses;//p.getCourseList();
            return View();
        }
    }
}
