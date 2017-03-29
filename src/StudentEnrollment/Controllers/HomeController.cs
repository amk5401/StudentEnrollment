using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using StudentEnrollment.Proxy;

namespace StudentEnrollment.Controllers
{
    public class HomeController : Controller
    {
        public LocalProxy localProxy;
        private IHostingEnvironment hostingEnv;

        public HomeController(IHostingEnvironment env)
        {
            hostingEnv = env;
        }

        public IActionResult Index()
        {
            localProxy = new LocalProxy(hostingEnv.WebRootPath);
            

            Models.Instructor i1 = new StudentEnrollment.Models.Instructor(5, "gj888", "gj888@rit.edu", "George", "Johnson");
            Models.Instructor i2 = new StudentEnrollment.Models.Instructor(4, "gj888", "gj888@rit.edu", "Bob", "Smith");

            List<StudentEnrollment.Models.Instructor> list = new List<Models.Instructor> { i1, i2};
            ViewData["Instructor"] = new StudentEnrollment.Models.Instructor(5, "gj888", "gj888@rit.edu", "George", "Johnson");
            ViewData["InstructorList"] = list;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
