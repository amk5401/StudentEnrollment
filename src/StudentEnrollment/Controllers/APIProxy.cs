using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using StudentEnrollment.Models;

namespace StudentEnrollment.Controllers
{
    public class APIProxy : Proxy
    {
        static HttpClient client = new HttpClient();
        private static String API_URL = "http://vm344f.se.rit.edu/API/API.php";
        private static List<String> teams = new List<String> { "general", "student_enrollment", "book_store", "human_resources", "facility_management", "cool_eval", "grading" };
        

        static async Task<String> CallAPI(string uri)
        {
            HttpResponseMessage response = await client.GetAsync(uri);
            String responseText = null;
            if (response.IsSuccessStatusCode)
            {
                responseText = await response.Content.ReadAsStringAsync();
            }
            return responseText;
        }


        public Student[] getSectionStudents(Section section)
        {


            throw new NotImplementedException();
            //HttpWebRequest http = (HttpWebRequest)WebRequest.Create("http://vm344f.se.rit.edu/API/API.php?team=general&function=test");
            //WebResponse response = http.res;

            //MemoryStream stream = response.GetResponseStream();
            //StreamReader sr = new StreamReader(stream);
            //string content = sr.ReadToEnd();
        }

        //public void createBook(Book book)
        //{
        //    throw new NotImplementedException();
        //}

        public void createCourse(Course course)
        {
            throw new NotImplementedException();
        }

        public void createSection(Section section)
        {
            throw new NotImplementedException();
        }

        public void createStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public void createTerm(Term term)
        {
            throw new NotImplementedException();
        }


        public void enrollStudent(Student student, Section section)
        {
            throw new NotImplementedException();
        }

        public Admin getAdmin(int ID)
        {
            throw new NotImplementedException();
        }

        public Book getBook(int ID)
        {
            throw new NotImplementedException();
        }

        public Course getCourse(int ID)
        {
            throw new NotImplementedException();
        }

        public Course[] getCourseList()
        {
            throw new NotImplementedException();
        }

        public Section[] getCourseSections(Course course)
        {
            throw new NotImplementedException();
        }

        public Term getCurrentTerm()
        {
            throw new NotImplementedException();
        }

        public Instructor getInstructor(int ID)
        {
            throw new NotImplementedException();
        }

        public Section[] getInstructorSections(Instructor student)
        {
            throw new NotImplementedException();
        }

        public Location getLocation(int ID)
        {
            throw new NotImplementedException();
        }

        public Section getSection(int ID)
        {
            Task<String> responseTask = CallAPI(API_URL + "?team=student_enrollment&function=getSection&sectionID=" + ID);
            String data = responseTask.Result;
            throw new NotImplementedException();
        }

        public Book[] getSectionBooks(Section section)
        {
            throw new NotImplementedException();
        }

        public Student getStudent(int ID)
        {
            throw new NotImplementedException();
        }

        public Section[] getStudentSections(Student student)
        {
            throw new NotImplementedException();
        }

        public Term getTerm(int ID)
        {
            throw new NotImplementedException();
        }

        public void toggleCourse(int ID)
        {
            throw new NotImplementedException();
        }

        public void waitlistStudent(Student student, Section section)
        {
            throw new NotImplementedException();
        }

        public void withdrawStudent(Student student, Section section)
        {
            throw new NotImplementedException();
        }
    }
}
