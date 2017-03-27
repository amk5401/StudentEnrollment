using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Prerequisite : Model
    {
        public Prerequisite()
        {

        }
        public int Course_ID { get; set; }
        public int Day_of_week { get; set; }
    }
}
