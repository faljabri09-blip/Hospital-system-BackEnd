using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Managment_system.Model
{
    public class Patient
    {
        public int patientId { get; set; }
        public string patientName { get; set; }
        public int patientAge { get; set; }
        public string patientGender { get; set; }

        public string patientPhone { get; set; }

        public string patientEmail { get; set; }

        public string patientBooldType { get; set; }



        public Patient(int pId, string pName, int pAge, string pGender,
                       string pPhone, string pEmail, string paBooldType)
        {
            patientId = pId;
            patientName = pName;
            patientAge = pAge;
            patientGender = pGender;
            patientPhone = pPhone;
            patientEmail = pEmail;
            patientBooldType = paBooldType;

        }
    }
}

