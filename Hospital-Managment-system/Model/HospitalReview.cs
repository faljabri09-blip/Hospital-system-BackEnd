using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Managment_system.Model
{
    public class HospitalReview
    {
        public int reviewId { get; set; }
        public int patientId { get; set; }

        public int rating { get; set; } 

        public string comment { get; set; }

    }
}
