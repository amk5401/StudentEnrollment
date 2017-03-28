using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Location : Model
    {
        public int ID { get; }

        public Location(int id)
        {
            this.ID = id;
        }
    }
}
