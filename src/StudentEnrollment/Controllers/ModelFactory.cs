using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentEnrollment.Models;
using Newtonsoft.Json;

namespace StudentEnrollment.Controllers
{
    public static class ModelFactory
    {
        public static Model buildModelFromJson(String modelType, String json)
        {
            if (modelType == null || modelType.Equals(""))
            {
                return null;
            }
            else if(modelType == "student")
            {
                /*dynamic contents = JsonConvert.DeserializeObject(json);

                string firstName = contents.firstName;
                string lastName = contents.lastName;
                int id = contents.id;
                String username = contents.username;
                String email = contents.email;
                int yearLevel = contents.yearLevel;
                float gpa = contents.gpa;
                List< Section > enrolledSections = contents.enrolledSections;

                return new Student(id, username, email, firstName, lastName, yearLevel, gpa, enrolledSections);*/
                return JsonConvert.DeserializeObject<Student>(json);
            }
            else if(modelType == "course")
            {
                /* dynamic contents = JsonConvert.DeserializeObject(json);

                 int id = contents.id;
                 string courseCode = contents.courseCode;
                 string name = contents.name;
                 int credits = contents.credits;
                 int minGPA = contents.minGPA;
                 int[] prereqs = contents.prereqs;

                 return new Course(id, courseCode, name, credits, minGPA, prereqs);*/

                return JsonConvert.DeserializeObject<Course>(json);
            }
            return null;
        }
        public static String[] buildIDListFromJSON(String modelType, String json)
        {
            if (modelType == null || modelType.Equals(""))
            {
                return null;
            }
            else if(modelType == "course")
            {
                dynamic contents = JsonConvert.DeserializeObject(json);
                return contents.prereqs;
            }
            return null;
        }

    }
}

