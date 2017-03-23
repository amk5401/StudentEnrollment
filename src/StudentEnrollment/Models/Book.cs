using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Book
    {
        public Book(int id, int isbn, String title)
        {
            ID = id;
            ISBN = isbn;
            Title = title;
        }

        public int ID { get; }
        public int ISBN { get; }
        public String Title { get; }
    }
}
