using System;
using System.Collections.Generic;
using StudentEnrollment.Models;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;

namespace StudentEnrollment.Proxy
{
    public class LocalProxy : IProxy
    {
        List<Student> studentsList = new List<Student>();
        List<Admin> adminsList = new List<Admin>();
        List<Instructor> instructorsList = new List<Instructor>();

        List<Book> booksList = new List<Book>();
        List<Course> coursesList = new List<Course>();
        List<Section> sectionsList = new List<Section>();
        List<Term> termsList = new List<Term>();
        List<Location> locationsList = new List<Location>();

        //Student ID, List of Section IDs
        Dictionary<int, List<int>> sectionsInStudentDict = new Dictionary<int, List<int>>();

        //Section ID, List of Student IDs
        Dictionary<int, List<int>> studentsInSectionDict = new Dictionary<int, List<int>>();

        //Section ID, List of Book IDs
        Dictionary<int, List<int>> booksInSectionDict = new Dictionary<int, List<int>>();

        //Course ID, List of Course IDs
        Dictionary<int, List<int>> prereqsInCourseDict = new Dictionary<int, List<int>>();

        String filePath;

        public LocalProxy()
        {
            this.filePath = "~/Views/jsonData/";


            //Read in students.json
            
            dynamic studentsJSON = JsonConvert.DeserializeObject(File.ReadAllText(HttpContext.Current.Server.MapPath(filePath + "students.json")));
            foreach (var student in studentsJSON)
            {
                JObject modelJSON = student;
                this.studentsList.Add((Student)ModelFactory.createModelFromJson("student", modelJSON.ToString()));
            }

            //Read in instructor.json
            dynamic instructorsJSON = JsonConvert.DeserializeObject(File.ReadAllText(HttpContext.Current.Server.MapPath(filePath + "instructors.json")));
            foreach (var instructor in instructorsJSON)
            {
                JObject modelJSON = instructor;
                this.instructorsList.Add((Instructor)ModelFactory.createModelFromJson("instructor", modelJSON.ToString()));
            }

            //Read in admins.json
            dynamic adminJSON = JsonConvert.DeserializeObject(File.ReadAllText(HttpContext.Current.Server.MapPath(filePath + "admins.json")));
            foreach (var admin in adminJSON)
            {
                JObject modelJSON = admin;
                this.adminsList.Add((Admin)ModelFactory.createModelFromJson("admin", modelJSON.ToString()));
            }

            //Read in courses.json
            dynamic coursesJSON = JsonConvert.DeserializeObject(File.ReadAllText(HttpContext.Current.Server.MapPath(filePath + "courses.json")));
            foreach (var course in coursesJSON)
            {
                List<int> prereqs = new List<int>();
                foreach (var pre in course.prereqs)
                {
                    int id = pre;
                    prereqs.Add(id);
                }

                JObject modelJSON = course;
                Course newCourse = (Course)ModelFactory.createModelFromJson("course", modelJSON.ToString());

                this.coursesList.Add(newCourse);
                this.prereqsInCourseDict.Add(newCourse.ID, prereqs);
            }

            //Read in sections.json
            dynamic sectionsJSON = JsonConvert.DeserializeObject(File.ReadAllText(HttpContext.Current.Server.MapPath(filePath + "sections.json")));
            foreach (var section in sectionsJSON)
            {
                List<int> books = new List<int>();
                foreach (var book in section.books)
                {
                    int id = book;
                    books.Add(id);
                }

                JObject modelJSON = section;
                Section newSection = (Section)ModelFactory.createModelFromJson("section", modelJSON.ToString());

                this.sectionsList.Add(newSection);
                this.booksInSectionDict.Add(newSection.ID, books);

            }

            //Read in terms.json
            dynamic termsJSON = JsonConvert.DeserializeObject(File.ReadAllText(HttpContext.Current.Server.MapPath(filePath + "terms.json")));
            foreach (var term in termsJSON)
            {
                JObject modelJSON = term;
                this.termsList.Add((Term)ModelFactory.createModelFromJson("term", modelJSON.ToString()));
            }

            //Read in locations.json
            dynamic locationsJSON = JsonConvert.DeserializeObject(File.ReadAllText(HttpContext.Current.Server.MapPath(filePath + "locations.json")));
            foreach (var location in locationsJSON)
            {
                JObject modelJSON = location;
                this.locationsList.Add((Location)ModelFactory.createModelFromJson("location", modelJSON.ToString()));
            }

            //Read in books.json
            dynamic booksJSON = JsonConvert.DeserializeObject(File.ReadAllText(HttpContext.Current.Server.MapPath(filePath + "books.json")));
            foreach (var book in booksJSON)
            {
                JObject modelJSON = book;
                this.booksList.Add((Book)ModelFactory.createModelFromJson("book", modelJSON.ToString()));
            }

            //Create Reference Dictionaries
            dynamic students_sectionsJSON = JsonConvert.DeserializeObject(File.ReadAllText(HttpContext.Current.Server.MapPath(filePath + "students_sections.json")));
            foreach (var item in students_sectionsJSON)
            {
                int studentID = item.studentID;
                int sectionID = item.sectionID;

                if (sectionsInStudentDict.ContainsKey(studentID))
                {
                    sectionsInStudentDict[studentID].Add(sectionID);
                }
                else
                {
                    sectionsInStudentDict.Add(studentID, new List<int> { sectionID });
                }

                if (studentsInSectionDict.ContainsKey(sectionID))
                {
                    studentsInSectionDict[sectionID].Add(studentID);
                }
                else
                {
                    studentsInSectionDict.Add(sectionID, new List<int> { studentID });
                }
            }


        }
        #region Model Getters
        //Methods for Retreiving data from API
        public Admin getAdmin(int ID)
        {
            Admin admin = null;
            if (adminsList.Exists(x => x.ID == ID))
            {
                admin = adminsList.Find(x => x.ID.Equals(ID));
            }
            return admin;
        }
        public Course getCourse(int ID)
        {
            Course course = null;
            if (coursesList.Exists(x => x.ID == ID))
            {
                course = coursesList.Find(x => x.ID.Equals(ID));
            }
            return course;
        }

        public Course[] getCourseList()
        {
            return coursesList.ToArray();
        }
        public Section getSection(int ID)
        {
            Section section = null;
            if (sectionsList.Exists(x => x.ID == ID))
            {
                section = sectionsList.Find(x => x.ID.Equals(ID));
            }
            return section;
        }
        public Location getLocation(int ID)
        {
            Location location = null;
            if (sectionsList.Exists(x => x.ID == ID))
            {
                location = locationsList.Find(x => x.ID.Equals(ID));
            }
            return location;
        }

        public Term getCurrentTerm()
        {
            Term term = null;
            if (termsList.Exists(x => (x.StartDate <= DateTime.Now) && (DateTime.Now <= x.EndDate)))
            {
                term = termsList.Find(x => (x.StartDate <= DateTime.Now) && (DateTime.Now <= x.EndDate));
            }
            return term;
        }

        public Term getTerm(String termCode)
        {
            throw new NotImplementedException();
        }
        public Student getStudent(int ID)
        {
            Student student = null;
            if (studentsList.Exists(x => x.ID == ID))
            {
                student = studentsList.Find(x => x.ID.Equals(ID));
            }
            return student;
        }

        public Instructor getInstructor(int ID)
        {
            Instructor instructor = null;
            if (instructorsList.Exists(x => x.ID == ID))
            {
                instructor = instructorsList.Find(x => x.ID.Equals(ID));
            }
            return instructor;
        }
        public Book getBook(int ID)
        {
            Book book = null;
            if (booksList.Exists(x => x.ID == ID))
            {
                book = booksList.Find(x => x.ID.Equals(ID));
            }
            return book;
        }
        #endregion


        #region Shared Data Tables
        //Returning arrays of Model objects from tables of IDs
        public Section[] getCourseSections(Course course)
        {
            return sectionsList.FindAll(x => x.Course.Equals(course)).ToArray();
        }

        public Section[] getStudentSections(Student student)
        {

            if (sectionsInStudentDict.ContainsKey(student.ID))
            {
                List<int> sectionIDs = sectionsInStudentDict[student.ID];
                List<Section> sectionsTemp = new List<Section>();
                foreach (int id in sectionIDs)
                {
                    sectionsTemp.Add(getSection(id));
                }
                return sectionsTemp.ToArray();
            }
            return null;
        }

        public Section[] getInstructorSections(Instructor instructor)
        {
            return sectionsList.FindAll(x => x.InstructorID.Equals(instructor.ID)).ToArray();
        }
        //TODO:
        public Student[] getSectionStudents(Section section)
        {
            if (studentsInSectionDict.ContainsKey(section.ID))
            {
                List<int> studentIDs = studentsInSectionDict[section.ID];
                List<Student> studentsTemp = new List<Student>();
                foreach (int id in studentIDs)
                {
                    studentsTemp.Add(getStudent(id));
                }
                return studentsTemp.ToArray();
            }
            return null;
        }

        public Course[] getCoursePrereqs(Course course)
        {
            if (prereqsInCourseDict.ContainsKey(course.ID))
            {
                List<int> prereqIDs = prereqsInCourseDict[course.ID];
                List<Course> prereqsTemp = new List<Course>();
                foreach (int id in prereqIDs)
                {
                    prereqsTemp.Add(getCourse(id));
                }
                return prereqsTemp.ToArray();
            }
            return null;
        }

        public Book[] getSectionBooks(Section section)
        {
            if (booksInSectionDict.ContainsKey(section.ID))
            {
                List<int> bookIDs = prereqsInCourseDict[section.ID];
                List<Book> booksTemp = new List<Book>();
                foreach (int id in bookIDs)
                {
                    booksTemp.Add(getBook(id));
                }
                return booksTemp.ToArray();
            }
            return null;
        }

        #endregion


        #region Creation Methods
        //Methods for adding data to the database
        public void createCourse(Course course)
        {
            this.coursesList.Add(course);
        }

        public void createSection(Section section)
        {
            /*JObject newStudent = new JObject(
                new JProperty("ID", section.ID),
                new JProperty("COURSE_ID", section.Course.ID),
                new JProperty("TERM_ID", section.TermID),
                new JProperty("PROFESSOR_ID", section.InstructorID),
                new JProperty("CLASSROOM_ID", section.LocationID)
             //new JProperty("location", student.LastName)
             );

            dynamic studentsJSON = JsonConvert.DeserializeObject(File.ReadAllText(this.filePath + "/jsonData/students.json"));
            JArray newJSON = studentsJSON;
            newJSON.Add(newStudent);

            File.WriteAllText(this.filePath + "/jsonData/students.json", newJSON.ToString());*/
            this.sectionsList.Add(section);
        }

        public void createStudent(Student student)
        {
            this.studentsList.Add(student);
        }

        public void createTerm(Term term)
        {
            this.termsList.Add(term);
        }
        public void createBook(Book book)
        {
            this.booksList.Add(book);
        }
        #endregion


        #region Interaction Methods
        //Methods for interactions between models
        public void enrollStudent(Student student, Section section)
        {
            if (sectionsInStudentDict.ContainsKey(student.ID))
            {
                sectionsInStudentDict[student.ID].Add(section.ID);
            }
            else
            {
                sectionsInStudentDict.Add(student.ID, new List<int> { section.ID });
            }

            if (studentsInSectionDict.ContainsKey(section.ID))
            {
                studentsInSectionDict[section.ID].Add(student.ID);
            }
            else
            {
                studentsInSectionDict.Add(section.ID, new List<int> { student.ID });
            }
        }
        public void toggleCourse(int ID)
        {
            Course course = getCourse(ID);
            Course changedCourse = new Course(course.ID, course.CourseCode, course.Name, course.Credits, course.MinGPA, !course.Availability);

            coursesList.Remove(course);
            coursesList.Add(changedCourse);

            Section[] sections = getCourseSections(course);
            
            foreach(Section section in sections)
            {
                toggleSection(section.ID);
            }

            //TODO: toggling off sections
        }
        public void toggleSection(int ID)
        {
            Section section = getSection(ID);

            Section changedSection = new Section(section.ID, section.MaxStudents, section.TermID, section.InstructorID, section.CourseID, section.LocationID, !section.Availability);

            sectionsList.Remove(section);
            sectionsList.Add(changedSection);
        }
        public void waitlistStudent(Student student, Section section)
        {
            throw new NotImplementedException();
        }
        public void withdrawStudent(Student student, Section section)
        {
            if (sectionsInStudentDict.ContainsKey(student.ID))
            {
                sectionsInStudentDict[student.ID].Remove(section.ID);
            }

            if (studentsInSectionDict.ContainsKey(section.ID))
            {
                studentsInSectionDict[section.ID].Remove(student.ID);
            }
        }
        #endregion
    }
}
