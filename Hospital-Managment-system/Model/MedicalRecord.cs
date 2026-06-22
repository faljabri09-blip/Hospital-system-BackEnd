using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Managment_system.Model
{
    public class MedicalRecord
    {

        public int recordId { get; set; }
        public int patientId { get; set; }
        public int DoctorId { get; set; }
        public int appointmentId { get; set; }
        public string diagonsis { get; set; }
        public string prescription { get; set; }
        public string visitDate { get; set; }
        public decimal visitFee { get; set; }

        //public MedicalRecord(int rId, int patId, int docId, int appoId, 
        //                     string reDiagonsis, string rePrescription, string reVisitDate, decimal reVisitFee)
        //{
        //    recordId = rId;
        //    patientId = patId;
        //    DoctorId = docId;
        //    appointmentId = appoId;
        //    diagonsis = reDiagonsis;
        //    prescription = rePrescription;
        //    visitDate = reVisitDate;
        //    visitFee = reVisitFee;
        //}
    }
}
