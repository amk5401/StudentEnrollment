using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using StudentEnrollment.Models;

namespace StudentEnrollment.Proxy
{
    public class APIProxy : Proxy
    {
        static HttpClient client = new HttpClient();
        private static String API_URL = "http://vm344f.se.rit.edu/API/API.php";

        static async Task<String> GetFromAPI(string uri)
        {
            HttpResponseMessage response = await client.GetAsync(uri);
            String responseText = null;
            if (response.IsSuccessStatusCode)
            {
                responseText = await response.Content.ReadAsStringAsync();
            }
            return responseText;
        }

        static async Task<String> PostToAPI(string uri, Dictionary<String, String> postParemeters)
        {
            var postData = new List<KeyValuePair<string, string>>();
            foreach (String key in postParemeters.Keys)
            {
                postData.Add(new KeyValuePair<string, string>(key, postParemeters[key]));
            }
            HttpContent content = new FormUrlEncodedContent(postData);
            var response = await client.PostAsync("http://localhost:44268/api/test", content);
            String responseText = null;
            if (response.IsSuccessStatusCode)
            {
                responseText = await response.Content.ReadAsStringAsync();
            }
            return responseText;
        }


        public Student[] getSectionStudents(Section section)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}/team=studentEnrollment&function=getSectionEnrolled&sectionID={1}",
                                                        API_URL, section.ID)).Result;
            String[] studentIDs = ModelFactory.createIDListFromJson("student", json);
            List<Student> students = new List<Student>();
            foreach (String studentID in studentIDs)
            {
                students.Add(getStudent(Convert.ToInt32(studentID)));
            }
            return students.ToArray();
        }

        //public void createBook(Book book)
        //{
        //    throw new NotImplementedException();
        //}

        public void createCourse(Course course)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}/team=studentEnrollment&function=postCourse&courseCode={1}&courseName={2}&credits={3}&minGPA={4}",
                                                        API_URL, course.CourseCode, course.Name, course.Credits, course.MinGPA)).Result;
            // TODO: See what comes back from JSON
        }

        public void createSection(Section section)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}/team=studentEnrollment&function=postSection&courseID={1}&professorID={2}&maxStudents={3}&termID={4}&classroomID={5}",
                                                        API_URL, section.Course.ID, section.Instructor.ID, section.MaxStudents, section.Term.ID, section.Location.ID)).Result;
            // TODO: See what comes back from JSON
        }

        /**
         * createStudent assumes that the User object attached to the Student object has already been registered within the database.
         * */
        public void createStudent(Student student)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}/team=studentEnrollment&function=postStudent&userID={1}&yearLevel={2}&gpa={3}",
                                                    API_URL, student.ID, student.YearLevel, student.GPA)).Result;
            // TODO: See what comes back from JSON
        }

        public void createTerm(Term term)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}/team=studentEnrollment&function=postTerm&termCode={1}&startDate={2}&endDate={3}",
                                                    API_URL, term.Code, term.StartDate, term.EndDate)).Result;
            // TODO: See what comes back from JSON
        }

        public void enrollStudent(Student student, Section section)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}/team=student_enrollment&function=enrollStudent&studentID={1}&sectionID={2}",
                                                    API_URL, student.ID, section.ID)).Result;
            // TODO: See what comes back from JSON
        }

        public Admin getAdmin(int ID)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}/team=general&function=getAdmin&adminID={1}",
                                                    API_URL, ID)).Result;
            Admin admin = null;
            try
            {
                admin = (Admin)ModelFactory.createModelFromJson("admin", json);
            }
            catch (InvalidCastException)
            {

            }
            return admin;
        }

        public Book getBook(int ID)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}/team=book_store&function=getBook&bookID={1}",
                                                    API_URL, ID)).Result;
            Book book = null;
            try
            {
                book = (Book)ModelFactory.createModelFromJson("book", json);
            }
            catch (InvalidCastException)
            {

            }
            return book;
        }

        public Course getCourse(int ID)
        {
            String json = APIProxy.GetFromAPI(String.Format("{0}/team=general&function=getCourse&courseID={1}",
                                        API_URL, ID)).Result;
            Course course = null;
            try
            {
                course = (Course)ModelFactory.createModelFromJson("course", json);
            }
            catch (InvalidCastException)
            {

            }
            return course;
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
            Task<String> responseTask = GetFromAPI(API_URL + "?team=student_enrollment&function=getSection&sectionID=" + ID);
            String data = responseTask.Result;
            Section section = null;
            try
            {
                section = (Section)ModelFactory.createModelFromJson("section", data);
            }
            catch (InvalidCastException)
            {

            }
            return section;
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

        public void createBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
