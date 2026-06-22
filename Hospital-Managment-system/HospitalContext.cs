using Hospital_Managment_system.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Managment_system
{
    public class HospitalContext
    {
        public List<Patient> Patients {  get; set; }

        public List<Doctor> Doctors { get; set; }

        public List<Appointment> Appointments { get; set; }

        public List<MedicalRecord> MedicalRecords { get; set; }

        public List<AvailableSlots> availableSlots { get; set; }

        public  List<Department> Departments { get; set; }

        public List<HospitalReview> reviews { get; set; }


    }
}
