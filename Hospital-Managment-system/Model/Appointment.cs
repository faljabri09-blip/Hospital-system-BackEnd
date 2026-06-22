using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Managment_system.Model
{
    public class Appointment
    {
        public int appointmentID { get; set; }

        public int patientID { get; set; }

        public int doctorId {  get; set; }

        public string appointmentDate { get; set; }

        public string appointmentTime { get; set; }

        public string status { get; set; }

        //public Appointment(int appID, int paID, int doId, string appDate, string appTime, string appstatus)
        //{
        //    appointmentID = appID;
        //    patientID = paID;
        //    doctorId = doId;
        //    appointmentDate = appDate;
        //    appointmentTime = appTime;
        //    status = appstatus;
        //}
    }
}
