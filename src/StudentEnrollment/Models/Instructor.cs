using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Instructor : User
    {
        public Instructor(int id, String username, String email, String firstName, String lastName) 
            : base(id, username, email, firstName, lastName)
        {

        }

        public Section[] TeachingSections { get; set; }

    }
}
