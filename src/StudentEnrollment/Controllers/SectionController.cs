﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentEnrollment.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentEnrollment.Controllers
{
    public class SectionController : Controller
    {
        [HttpGet]
        public ActionResult SectionList(int courseID)
        {
            ViewData["Title"] = "Section List";
            Proxy p = new APIProxy();

            List<Student> none = new List<Student> { };
            int[] na = new int[] { };
            Term t1 = new Term(1, "Spring-17", DateTime.Now, DateTime.Now);
            Instructor i1 = new Instructor(1, "gj888", "gj888@rit.edu", "George", "Johnson");
            Course c1 = new Course(courseID, "CS-420", "Data Mining", 3, 3, na);
            Location l1 = new Location();

            Section s1 = new Section(1, 30, t1, i1, c1, l1, none);

            ViewData["Course"] = c1;
            ViewData["Sections"] = new List<Section> { s1 };// p.getCourseSections(p.getCourse(courseID));
            return View();
        }
    }
}