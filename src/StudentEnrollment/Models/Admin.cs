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

        static Admin buildFromJson(String json)
        {
            return new Admin()
        }
    }
}
