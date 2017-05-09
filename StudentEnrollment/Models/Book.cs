using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Book : Model
    {
        public Book() { }
        public Book(int isbn, String title, int publisherID, String thumbnail, double price, bool available, int count)
        {
            ISBN = isbn;
            Title = title;
            PublisherID = publisherID;
            ThumbnailURL = thumbnail;
            Price = price;
            Availability = available;
            Count = count;
        }

        public int ID { get; }
        public int ISBN { get; }
        public String Title { get; }
        public int PublisherID { get; }
        public String ThumbnailURL { get; }
        public double Price { get; }
        public bool Availability { get; }
        public int Count { get; }
        public override string ToString()
        {
            return String.Format("Book #{0}: {1} \nISBN: {2}", this.ID, this.Title, this.ISBN);
        }
    }
}
