using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models 
{
    public class Admin : User
    { 
        public Admin(int id, String username, String email, String firstName, String lastName) 
            : base(id, username, email, firstName, lastName)
        {

        }
        public override string ToString()
        {
            return String.Format("Admin #{0}: {1} {2} \nUsername: {3} \nEmail: {4}", this.ID, this.FirstName, this.LastName, this.Username, this.Email);
        }
    }
}
