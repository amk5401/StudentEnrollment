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
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            Models.Instructor i1 = new StudentEnrollment.Models.Instructor(5, "gj888", "gj888@rit.edu", "George", "Johnson");
            Models.Instructor i2 = new StudentEnrollment.Models.Instructor(4, "gj888", "gj888@rit.edu", "Bob", "Smith");

            List<StudentEnrollment.Models.Instructor> list = new List<Models.Instructor> { i1, i2};
            ViewData["Instructor"] = new StudentEnrollment.Models.Instructor(5, "gj888", "gj888@rit.edu", "George", "Johnson");
            ViewData["InstructorList"] = list;

            String path = HttpContext.Request.MapPath("~/Views/jsonData/proxyFunctions.json");
            Stream file = System.IO.File.OpenRead(path);
            Dictionary<string, string> functions = new Dictionary<string, string>();
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                functions = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            ViewData["Functions"] = functions;
            return View();
        }

        public ActionResult TestApi(string function, string parameters, string proxyType)
        {
            Models.Student s = new StudentEnrollment.Models.Student(1, "rs4296", "rs4296@rit.edu", "Rob", "Stone", 5, float.Parse("3.5"));
            Models.Instructor i = new StudentEnrollment.Models.Instructor(1, "gj888", "gj888@rit.edu", "Bob", "Smith");
            Models.Course c1 = new StudentEnrollment.Models.Course(1, "SWEN-344", "Web Engineering", 3, 2, false);
            Models.Course c2 = new StudentEnrollment.Models.Course(2, "CS-420", "Data Mining", 3, 3, false);
            Models.Course c3 = new StudentEnrollment.Models.Course(3, "SWEN-261", "Intro to SE", 3, 1, false);
            Models.Course c4 = new StudentEnrollment.Models.Course(4, "CS-262", "Engineering of Software Subsystems", 3, 1, false);
            List<StudentEnrollment.Models.Course> courses = new List<Models.Course> { c1, c2, c3, c4 };

            ViewData["Function"] = function;
            ViewData["Parameters"] = parameters;
            ViewData["ProxyType"] = proxyType;
            IProxy p;
            if (proxyType == "Local")
            {
                p = new LocalProxy(new ServerPathData());
            }
            else
            {
                p = new APIProxy();
            }
            dynamic result = "The function returned void.";
            if (function != null)
            {
                try
                {
                    switch (function)
                    {
                        case "getStudent": result = p.getStudent(Int32.Parse(parameters)); break;
                        case "getInstructor": result = p.getInstructor(Int32.Parse(parameters)); break;
                        case "getAdmin": result = p.getAdmin(Int32.Parse(parameters)); break;
                        case "getCourse": result = p.getCourse(Int32.Parse(parameters)); break;
                        case "getCourseList": result = p.getCourseList(); break;
                        case "getSection": result = p.getSection(Int32.Parse(parameters)); break;
                        case "getCourseSections":
                            Course course = (Course)ModelFactory.createModelFromJson("course", parameters);
                            result = p.getCourseSections(course);
                            break;
                        case "getStudentSections":
                            Student student = (Student)ModelFactory.createModelFromJson("student", parameters);
                            result = p.getStudentSections(student);
                            break;
                           
                        //result = p.getStudentSections((Student)Convert.ChangeType(parameters, typeof(Student))); break;
                        case "getSectionStudents":
                            Section section = (Section)ModelFactory.createModelFromJson("section", parameters);
                            result = p.getSectionStudents(section);
                            //result = p.getSectionStudents((Section)Convert.ChangeType(parameters, typeof(Section)));
                            break;
                        case "getInstructorSections":
                            Instructor instructor = (Instructor)ModelFactory.createModelFromJson("instructor", parameters);
                            result = p.getInstructorSections(instructor);
                            //result = p.getInstructorSections((Instructor)Convert.ChangeType(parameters, typeof(Instructor)));
                            break;
                        case "getBook": result = p.getBook(Int32.Parse(parameters)); break;
                        case "getSectionBooks":
                            section = (Section)ModelFactory.createModelFromJson("section", parameters);
                            result = p.getSectionBooks(section);
                            //result = p.getSectionBooks((Section)Convert.ChangeType(parameters, typeof(Section)));
                            break;
                        case "getLocation": result = p.getLocation(Int32.Parse(parameters)); break;
                        case "getCurrentTerm": result = p.getCurrentTerm(); break;
                        case "getTerm": result = p.getTerm(parameters); break;
                        case "getTerms": result = p.getTerms(); break;
                        case "createTerm":
                            Term term = (Term)ModelFactory.createModelFromJson("term", parameters);
                            p.createTerm(term);
                            result = "Call the getTerms method for the result of this operation.";
                            break;
                        case "createCourse":
                            course = (Course)ModelFactory.createModelFromJson("course", parameters);
                            p.createCourse(course);
                            result = "Check the course list for the result of that operation.";
                            break;
                    }
                }
                catch (FormatException fex)
                {
                    ViewData["Result"] = "The input needs to be an integer.";
                    System.Diagnostics.Debug.WriteLine("Function: " + function + " Parameter: " + parameters + " " + fex);
                    return View();
                }
                catch (InvalidCastException icex)
                {
                    ViewData["Result"] = "Your input was not properly formatted for that object type.";
                    System.Diagnostics.Debug.WriteLine("Function: " + function + " Parameter: " + parameters + " " + icex);
                    return View();
                }
                catch (NotImplementedException niex)
                {
                    ViewData["Result"] = "This function is currently not implemented by this proxy.";
                    System.Diagnostics.Debug.WriteLine("Function: " + function + " Parameter: " + parameters + " " + niex);
                    return View();
                }
                catch (Exception ex)
                {
                    ViewData["Result"] = "An unkown error occured.";
                    System.Diagnostics.Debug.WriteLine("Function: " + function + " Parameter: " + parameters + " " + ex);
                    return View();
                }
            }
            if (result == null)
            {
                ViewData["Result"] = result;
                return View();
            }

            // The function returned an object or a list of objects
            if (result.GetType().IsArray)
            {
                ViewData["Result"] = string.Join("\n", (object[])result);
            }
            else
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                ViewData["Result"] = json;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewData["Message"] = "Student Enrollment System for SWEN-344";

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

    }
}
