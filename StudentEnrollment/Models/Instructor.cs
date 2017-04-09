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
        public override string ToString()
        {
            return String.Format("Instructor #{0}: {1} {2} \nUsername: {3} \nEmail: {4}", this.ID, this.FirstName, this.LastName, this.Username, this.Email);
        }

    }
}
