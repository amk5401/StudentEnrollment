using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentEnrollment.Proxy;
using StudentEnrollment.Models;
using System.Web.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentEnrollment.Controllers
{
    public class SectionController : Controller
    {
        [HttpGet]
        public ActionResult SectionList(int courseID)
        {
            IProxy p = new APIProxy();
            ViewData["Title"] = "Sections of " + (p.getCourse(courseID)).Name;
            return PartialView(p.getCourseSections(p.getCourse(courseID)));
        }
        public ActionResult SectionDetails(int sectionID)
        {
            IProxy proxy = new APIProxy();


            ViewData["Title"] = proxy.getSection(sectionID);
            Console.Write(proxy.getSection(sectionID));

            // TODO: uncomment and make sure this works with the API proxy after the endpoint is made
            /* Section section = proxy.getSection(sectionID);
            Instructor instructor = proxy.getInstructor(section.InstructorID);
            //ViewData["Instructor"] = proxy.getInstructor(proxy.getSection(sectionID).InstructorID);
            ViewData["Instructor"] = instructor;
            //return View();
            */

            //only keeping this in to test how view shows instructor
            Instructor i = new Instructor(4, "prof4", "prof@example", "Dan", "Krutz");
            Section s = (proxy.getSection(sectionID));
            Course c = proxy.getCourse(s.CourseID);
            ViewData["InstructorName"] = i.LastName;
            ViewData["CourseCode"] = c.CourseCode;
            return View(proxy.getSection(sectionID));

        }
    }
}