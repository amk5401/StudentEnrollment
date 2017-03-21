using System;

namespace StudentEnrollment.Models
{
    public abstract class User
    {
        public int ID { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
    }
}
