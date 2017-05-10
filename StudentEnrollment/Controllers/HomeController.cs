using System;
using System.Collections.Generic; 
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using StudentEnrollment.Proxy;
using Newtonsoft.Json;
using System.IO;
using StudentEnrollment.Models;

namespace StudentEnrollment.Controllers
{
    public struct SectionDisplay
    {
        public string courseName;
        public string instructorName;
        public int sectionID;
        public string locationName;
    }

    public class HomeController : Controller
    {
        User currentUser;
        Section[] currentSections;
        
        List<SectionDisplay> sectionsData = new List<SectionDisplay>();
        List<SectionDisplay> waitlistsData = new List<SectionDisplay>();


        private APIProxy proxy = new APIProxy();

        private bool loggedIn()
        {
            if (Session["user"] == null) return false; else return true;
        }

        private bool checkPermission(string role)
        {
            if (Session["user"] != null && Session["role"] != null && String.Equals(role, (string)Session["Role"], StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public ActionResult Index()
        {
       
            if(!loggedIn())
            {
                return RedirectToAction("Index", "Login");
            }

            ViewData["title"] = "Enrollment Dashboard";
            


            //If the user and role aren't null
            if (Session["role"] != null)
            {
                ViewData["Role"] = Session["role"];

                currentUser = (User)Session["user"];
                //If the user is a student
                if (checkPermission("student")) 
                {
                    ViewData["SectionsTitle"] = "Enrolled Sections";
                    Student student = proxy.getStudent(currentUser.ID);
                    currentSections = proxy.getStudentSections(student);

                    Section[] currentWaitlists = proxy.getStudentWaitlists(student);
                    List<SectionDisplay> waitlists = new List<SectionDisplay>(); 
                    for (int i = 0; i < currentWaitlists.Length; i++)
                    {
                        waitlists.Add(getSectionData(currentWaitlists[i]));
                    }
                    ViewData["StudentWaitlists"] = waitlists.ToArray();
                }
                //If the user is an instructor
                else if (checkPermission("professor"))
                {
                    ViewData["SectionsTitle"] = "Instructor Sections";
                    currentSections = proxy.getInstructorSectionsByID(currentUser.ID);
                    
                }
                //If the user is an admin
                else if (checkPermission("admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
            }

            for(int i = 0; i < currentSections.Length; i++)
            {
                sectionsData.Add(getSectionData(currentSections[i]));
            }


            return View(sectionsData.ToArray());


        }

        private SectionDisplay getSectionData(Section section)
        {
            string fName = proxy.getUser(section.InstructorID).FirstName;
            string lName = proxy.getUser(section.InstructorID).LastName;
            int buildingNum = proxy.getLocation(section.LocationID).BuildingID;
            int roomNum = proxy.getLocation(section.LocationID).RoomNumber;

            SectionDisplay sectionData = new SectionDisplay();
            sectionData.sectionID = section.ID;
            sectionData.instructorName = fName + " " + lName;
            sectionData.courseName = proxy.getCourse(section.CourseID).Name;
            sectionData.locationName = "Building " + buildingNum + ", Room " + roomNum;

            return sectionData;
        }

        [HttpPost]
        public ActionResult Withdraw(int sectionID)
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "Index", redirectController = "Home" });
            if (!checkPermission("student")) return RedirectToAction("AccessDenied", "Home");

            currentUser = (User)Session["user"];

            Section section = this.proxy.getSection(sectionID);
            Student student = this.proxy.getStudent(currentUser.ID);
            this.proxy.withdrawStudent(student, section);

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult WithdrawFromWaitlist(int sectionID)
        {
            if (!loggedIn()) return RedirectToAction("Index", "Login", new { redirectAction = "Index", redirectController = "Home" });
            if (!checkPermission("student")) return RedirectToAction("AccessDenied", "Home");

            currentUser = (User)Session["user"];

            Section section = this.proxy.getSection(sectionID);
            Student student = this.proxy.getStudent(currentUser.ID);
            this.proxy.withdrawWaitlistStudent(student, section);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            ViewData["Message"] = "Student Enrollment System for SWEN-344";

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult QOD()
        {
            APIProxy p = new APIProxy();
            string quote = p.getQuote();
            ViewData["Quote"] = System.Web.HttpUtility.HtmlDecode(quote.Replace("<p>","").Replace("</p>",""));
            return PartialView();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

    }
}
