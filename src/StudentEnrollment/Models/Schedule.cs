using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Schedule
    {
        public Schedule()
        {

        }
        public int Section_ID { get; set; }
        public String Day_of_week { get; set; }
        public TimeSpan Start_time { get; set; }
        public TimeSpan End_time { get; set; }
    }
}
