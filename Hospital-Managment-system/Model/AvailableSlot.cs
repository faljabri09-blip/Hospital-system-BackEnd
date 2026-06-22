using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Managment_system.Model
{
    public class AvailableSlots
    {
        public int slotId {  get; set; }
        public int doctorId { get; set; }
        public string slotDate { get; set; }
        public string slotTime { get; set; }
        public bool isBooked { get; set; }

        //public AvailableSlots(int sId, int dId, string sDate, string sTime, bool Booked)
        //{
        //    slotId = sId;
        //    doctorId = dId;
        //    slotDate = sDate;
        //    slotTime = sTime;
        //    isBooked = Booked;
        //}
    }
}
