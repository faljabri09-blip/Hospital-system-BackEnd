using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Managment_system.Model
{
    public class Doctor
    {
        public int doctorId { get; set; }
        public string doctorName { get; set; }
        public string doctorSpecialization { get; set; }
        public string doctorPhone { get; set; }

        public string doctorEmail { get; set; }

        public int departmentId { get; set; }

        public decimal consulationFee { get; set; }



        //public Doctor (int dId, string dName, string dSpecialization, string dPhone, string dEmail, decimal dconsulationFee)
        //{
        //    doctorId = dId;
        //    doctorName = dName;
        //    doctorSpecialization = dSpecialization;
        //    doctorPhone = dPhone;
        //    doctorEmail = dEmail;
        //    consulationFee = dconsulationFee;
        //}
    }
}
