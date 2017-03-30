using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Term : Model
    {
        public Term(int id, String code, DateTime startDate, DateTime endDate)
        {
            ID = id;
            Code = code;
            StartDate = startDate;
            EndDate = endDate;
        }
        public int ID { get; }
        public String Code { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
    }
}
