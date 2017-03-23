using System;

namespace StudentEnrollment.Models
{
    public abstract class User
    {
        public User(int id, String username, String email, String firstName, String lastName)
        {
            ID = id;
            Username = username;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }
        public int ID { get; }
        public String Username { get; }
        //public String Password { get; set; }
        public String Email { get; }
        public String FirstName { get; }
        public String LastName { get; } 
    }
}
