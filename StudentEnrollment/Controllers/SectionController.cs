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


            ViewData["Title"] = (proxy.getSection(sectionID).CourseID);
            Console.Write(proxy.getSection(sectionID));

            
            Section section = proxy.getSection(sectionID);
            Course c = proxy.getCourse(section.CourseID);
            Instructor instructor = proxy.getInstructor(section.InstructorID);


            ViewData["Instructor"] = instructor;
            ViewData["Course"] = c;
            ViewData["CourseCode"] = c.CourseCode;
            ViewData["CourseName"] = c.Name;
            return View(proxy.getSection(sectionID));
        
        
            
            

        }
    }
}