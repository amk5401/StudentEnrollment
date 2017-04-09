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
        public override string ToString()
        {
            return String.Format("Term #{0}: {1} \nStart Date: {2} \nEnd Date: {3}", this.ID, this.Code, this.StartDate.ToString(), this.EndDate.ToString());
        }
    }
}
