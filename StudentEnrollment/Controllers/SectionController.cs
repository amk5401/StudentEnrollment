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
        private APIProxy proxy = new APIProxy();
        [HttpGet]
        public ActionResult SectionList(int courseID)
        {
            IProxy p = new APIProxy();
            ViewData["Title"] = "Sections of " + (p.getCourse(courseID)).Name;
            return PartialView(p.getCourseSections(p.getCourse(courseID)));
        }

        [HttpGet]
        public ActionResult SectionDetails(int sectionID)
        {
          


            ViewData["Title"] = (proxy.getSection(sectionID).CourseID);
            Console.Write(proxy.getSection(sectionID));

            
            Section section = proxy.getSection(sectionID);
            Course c = proxy.getCourse(section.CourseID);
            Instructor instructor = proxy.getInstructor(section.InstructorID);

            int numStudents = proxy.getSectionStudents(section).Length;

            ViewData["Enrolled"] = numStudents;
            
            if (numStudents >= section.MaxStudents)
            {
                ViewData["Enroll"] = "Waitlist";
            }
            else
            {
                ViewData["Enroll"] = "Enroll";
            }
            ViewData["Instructor"] = instructor;
            ViewData["Course"] = c;
            ViewData["CourseCode"] = c.CourseCode;
            ViewData["CourseName"] = c.Name;
            return View(section);
        }

        [HttpPost]
        public ActionResult Enroll(int sectionID)
        {
            if (ModelState.IsValid)
            {
                IProxy p = new APIProxy();
                Section section = this.proxy.getSection(sectionID);
                Student student = this.proxy.getStudent(4);
                //Student s = p.createStudent(student);
                //this.proxy.createStudent(student);
                this.proxy.enrollStudent(student, section);
                ViewData["student"] = student;
                ViewData["section"] = section;
                ViewData["course"] = this.proxy.getCourse(this.proxy.getSection(sectionID).CourseID);

                return View(section);
            }
            else
            {
                return RedirectToAction("SectionDetails", new { sectionID = sectionID });
            }
            //Student student = new Student(10, "user", "user@user", "bob", "smith", 4, 3.44f);
            //Section s = proxy.getSection(sectionID);

            //if (student != null && s != null)
            //{
            //    this.proxy.enrollStudent(student, s);
            //    return SectionDetails(sectionID);
            //}
            //Console.Write("User not logged in");
            //return View(s);
            
        
        }
        [HttpPost]
        public ActionResult Waitlist(Student student, Section section)
        {
            if (student != null && section !=null)
            {
                this.proxy.waitlistStudent(student, section);
                return SectionDetails(section.ID);
            }
            Console.Write("user not logged in");
            return SectionDetails(section.ID);
        }
    }
}