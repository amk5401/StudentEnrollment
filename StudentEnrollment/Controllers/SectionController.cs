﻿using System;
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
            ViewData["Title"] = (p.getCourse(courseID)).Name;
            List<Student> none = new List<Student> { };
            int[] na = new int[] { };
            Term t1 = new Term(1, "Spring-17", DateTime.Now, DateTime.Now);
            Instructor i1 = new Instructor(1, "gj888", "gj888@rit.edu", "George", "Johnson");
            Course c1 = new Course(courseID, "CS-420", "Data Mining", 3, 3, false);
            Location l1 = new Location(1, 2, 3, 4);

            Section s1 = new Section(1, 30, 1, 1, courseID, 1, true);

            //ViewData["Course"] = p.getCourse(courseID);
            //ViewData["Sections"] = p.getCourseSections(p.getCourse(courseID));
            return View(p.getCourseSections(p.getCourse(courseID)));
        }
        public ActionResult SectionDetails(int sectionID)
        {
            IProxy proxy = new APIProxy();


            ViewData["Title"] = (proxy.getSection(sectionID).Course);
            Console.Write(proxy.getSection(sectionID));

            /* Section section = proxy.getSection(sectionID);
            Instructor instructor = proxy.getInstructor(section.InstructorID);
            //ViewData["Instructor"] = proxy.getInstructor(proxy.getSection(sectionID).InstructorID);
            ViewData["Instructor"] = instructor;
            //return View();
            */

            Instructor i = new Instructor(4, "prof4", "prof@example", "Dan", "Krutz");
            Section s = (proxy.getSection(sectionID));
            Course c = proxy.getCourse(s.CourseID);
            ViewData["InstructorName"] = i.LastName;
            ViewData["CourseCode"] = c.CourseCode;
            return View(proxy.getSection(sectionID));

        }
    }
}