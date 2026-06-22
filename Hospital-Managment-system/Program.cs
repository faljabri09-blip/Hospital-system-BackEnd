using Hospital_Managment_system.Model;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;


namespace Hospital_Managment_system
{
    internal class Program
    {
        public static string SystemEmail = "Hospital@om";
        private static readonly int slotId;

        //public static object appointmentID { get; private set; }

        public static void RegistrationPatients(List<Patient> patientsList)
        {
            Console.WriteLine("Enter your Name:");
            string patientName = Console.ReadLine();

            Console.WriteLine("Enter your Age:");
            int patientAge = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter your Gender");
            string patientGender = Console.ReadLine();

            Console.WriteLine("Enter your Phone number ");
            string patientPhone = Console.ReadLine();

            Console.WriteLine("Enter your Email");
            string patientEmail = Console.ReadLine();

            Console.WriteLine("Enter your Boold Type");
            string patientBooldType = Console.ReadLine();

            int patientId = patientsList.Count + 1;

            var newPatients = new Patient(
                    patientId,
                    patientName,
                    patientAge,
                    patientGender,
                    patientPhone,
                    patientEmail,
                    patientBooldType
                );

            patientsList.Add(newPatients);

            EmailServices.SendEmail(SystemEmail, patientEmail, "Registration Patients", "Thank you for registering whith our Hospital system");
            Console.WriteLine("Patient Id =" + patientId +  "Patient adding successfully" );
        } //done

        public static void AddNewDoctor(List<Doctor> doctors)
        {
            Console.WriteLine("Enter doctor Name:");
            string doctorName = Console.ReadLine();

            Console.WriteLine("Enter doctor Specialization");
            string doctorSpecialization = Console.ReadLine();

            Console.WriteLine("Enter doctor Phone");
            string doctorPhone = Console.ReadLine();

            Console.WriteLine("Enter doctor Email");
            string doctorEmail = Console.ReadLine();

            Console.WriteLine("Enter Department id");
            int departmentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter consulationFee");
            decimal consulationFee = decimal.Parse(Console.ReadLine());

            int doctorId = doctors.Count + 1;

            doctors.Add(
                new Doctor
                {
                    doctorId = doctorId,
                    doctorName = doctorName,
                    doctorSpecialization = doctorSpecialization,
                    doctorPhone = doctorPhone,
                    doctorEmail = doctorEmail,
                    departmentId = departmentId,
                    consulationFee = consulationFee
                });

            Console.WriteLine("Doctor ID=" + doctorId + "Doctor adding successfully");
     
        } //done

        public static void ViewAllPatient(List<Patient> patientsList)
        {

            if (patientsList.Count == 0)
            {
                Console.WriteLine("No patient register");
                return;
            }

            foreach (Patient patient in patientsList)
            {
                Console.WriteLine($"Patient ID = {patient.patientId}");
                Console.WriteLine($"Patient Name = {patient.patientName}");
                Console.WriteLine($"Patient Age = {patient.patientAge}");
                Console.WriteLine($"Patient Gender = {patient.patientGender}");
                Console.WriteLine($"Patient Phone = {patient.patientPhone}");
                Console.WriteLine($"Patient Email = {patient.patientEmail}");
                Console.WriteLine($"Patient Blood Type = {patient.patientBooldType}");

                Console.WriteLine("////////////////////////////////////////////////////");
            }
        
        }  //done

        public static void viewAlldoctor(List<Doctor> doctors)

        {

            Console.WriteLine("Enter Doctor Specialization");
            string input = Console.ReadLine().ToLower();


            var Specialization = doctors.Where(d => d.doctorSpecialization.ToLower() == input).ToList();


            if (Specialization.Count == 0)
            {
                Console.WriteLine($"No doctors found with specialization '{input}'.");
                return;
            }

            foreach (Doctor doctor in Specialization)
            {
                Console.WriteLine($"Doctor ID = {doctor.doctorId}");
                Console.WriteLine($"Doctor Name = {doctor.doctorName}");
                Console.WriteLine($"Doctor Specialization = {doctor.doctorSpecialization}");
                Console.WriteLine($"Doctor Phone = {doctor.doctorPhone}");
                Console.WriteLine($"Doctor Email = {doctor.doctorEmail}");
                Console.WriteLine($"Consultation Fee = {doctor.consulationFee}");
                
            }

        }

        public static void AddAvailableTimeSoltDoctor(HospitalContext context)
        { 
           
            //search doctor

            var doctor = context.Doctors.FirstOrDefault(d => d.doctorId == d.doctorId);

            if (context.Doctors.Count == 0)
            {

                Console.WriteLine("Doctor not found");
                return;
            }

            Console.WriteLine("Available doctors:");
            // LINQ: ForEach to print all doctors
            context.Doctors.ForEach(d =>
                Console.WriteLine($"  ID: {d.doctorId}  |  {d.doctorName}  ({d.doctorSpecialization})")
            );


            Console.WriteLine("Enter Doctor ID:");
            int doctorId = int.Parse(Console.ReadLine());

            bool result = context.Doctors.Any(d => d.doctorId ==  doctorId);

            if (result == false)
            {
                Console.WriteLine("Not found");
            }

            Console.WriteLine("Enter Slot Date (YYYY-MM-DD):");
                string slotDate = Console.ReadLine();

                Console.WriteLine("Enter Slot Time (HH:MM):");
                string slotTime = Console.ReadLine();

                int SlotId = (context.availableSlots.Count) + 1;

            context.availableSlots.Add(new AvailableSlots
            {
                slotId = slotId,
                doctorId = doctorId,
                slotDate = slotDate,
                slotTime = slotTime,
                isBooked = true
            });

                Console.WriteLine($"Slot added successfully, Slot ID: {slotId}, Doctor: {doctor.doctorName}, Date: {slotDate}, Time: {slotTime}");
            }  //done


        public static void BookinganAppointment(HospitalContext context)
        {

            Console.WriteLine("Enter Patient ID:");
            int patientId = int.Parse(Console.ReadLine());

             Patient patient = context.Patients.FirstOrDefault(p => p.patientId == patientId);
            

            if (patient == null) 
            {
                Console.WriteLine("Patient not found");
            }

            // the patient select the doctor from the list , call from the function
            viewAlldoctor(context.Doctors);

            Console.WriteLine("Enter doctor Id");
            int doctorId = int.Parse(Console.ReadLine());

            Doctor doctor = context.Doctors.FirstOrDefault(d => d.doctorId == doctorId);

            if (doctor == null)

            {
                Console.WriteLine("Doctor not found");
                return;
            }

            
     
            List<AvailableSlots> slot = context.availableSlots.Where(slot => slot.doctorId == doctorId && slot.isBooked == false).ToList();

            if (slot.Count == 0)
            {
                Console.WriteLine("No available slot");
                return;
            }

            Console.WriteLine($"\nAvailable slots for Dr. {doctor.doctorName}:");
            slot.ForEach(s =>
                Console.WriteLine($"  Slot ID: {s.slotId}  |  Date: {s.slotDate}  |  Time: {s.slotTime}")
            );

            Console.Write("Enter slot ID to book: ");
            int slotId = int.Parse(Console.ReadLine());

            // LINQ: FirstOrDefault() to confirm chosen slot is valid and unbooked
            AvailableSlots selectedSlot = slot.FirstOrDefault(s => s.slotId == slotId);

            if (selectedSlot == null)
            {
                Console.WriteLine("Slot not found or already booked.");
                return;
            }


            int appointmentID = context.Appointments.Count + 1;

            context.Appointments.Add(new Appointment
            {
                appointmentID = appointmentID,
                patientID = patientId,
                doctorId = doctorId,
                appointmentDate = selectedSlot.slotDate,
                appointmentTime = selectedSlot.slotTime,
                status = "Scheduled"
            });

            selectedSlot.isBooked = true;

            Console.WriteLine("Appointment booked successfully , Appointment id:"+appointmentID);
        }  //done


        public static void CancleAppointment(HospitalContext context)
        {
            Console.WriteLine("Enter the Appointment id");
            int appointmentId = int.Parse(Console.ReadLine());

            Appointment appointment = context.Appointments.FirstOrDefault(a => a.appointmentID == appointmentId);

            if (appointment == null)
            {
                Console.WriteLine("Appointment id not found");
                return;
            }

            if(appointment.status == "cancelled")
            {
                Console.WriteLine("Appointment cancelled");
                return;

            }

            if(appointment.status == "Complete")
            {
                Console.WriteLine("Appointment completed can not be cancel");
            }

            // put appointment availabe again
            AvailableSlots availbleSlot = context.availableSlots.FirstOrDefault(s => s.doctorId == appointment.doctorId &&
                                                                          s.slotDate == appointment.appointmentDate &&
                                                                          s.slotTime == appointment.appointmentTime);


            if (availbleSlot != null)
            {

                availbleSlot.isBooked = false;
            }

            appointment.status = "cancelled";

            

            Console.WriteLine("Appointment cancelled successfully");
            Console.WriteLine($"ID: {appointment.appointmentID}, Status: {appointment.status}");
        }


        public static void CreateMedicalRecordAfterVisit(HospitalContext context)
        {

            Console.WriteLine("\n=== Create Medical Record ===");

            Console.Write("Enter appointment ID for this visit: ");
            int appointmentId = int.Parse(Console.ReadLine());

            // LINQ: FirstOrDefault() to find the appointment
            Appointment appointment = context.Appointments.FirstOrDefault(a => a.appointmentID == appointmentId);

            if (appointment == null)
            {
                Console.WriteLine("Appointment not found.");
                return;
            }

            if (appointment.status == "Cancelled")
            {
                Console.WriteLine("Cannot create a medical record for a cancelled appointment.");
                return;
            }

            if (appointment.status == "Completed")
            {
                Console.WriteLine("A medical record already exists for this appointment.");
                return;
            }

            // LINQ: FirstOrDefault() + Select() to get the doctor's consultation fee
            decimal fee = context.Doctors
                .Where(d => d.doctorId == appointment.doctorId)
                .Select(d => d.consulationFee)
                .FirstOrDefault();

            Console.Write("Enter diagnosis: ");
            string diagnosis = Console.ReadLine();

            Console.Write("Enter prescription / medication: ");
            string prescription = Console.ReadLine();

            Console.Write("Enter visit date (e.g. 2026-07-10): ");
            string visitDate = Console.ReadLine();

            int recordId = context.MedicalRecords.Count + 1;

            context.MedicalRecords.Add(new MedicalRecord
            {
                recordId = recordId,
                patientId = appointment.patientID,
                DoctorId = appointment.doctorId,
                appointmentId = appointmentId,
                diagonsis = diagnosis,
                prescription = prescription,
                visitDate = visitDate,
                visitFee = fee
            });

            appointment.status = "Completed";

            Console.WriteLine($"Medical record created successfully. Record ID: {recordId}" +
                              $" | Fee charged: {fee:C}");

        }

        public static void patientMedicalHistoryReport(HospitalContext context)
        {

            Console.WriteLine("\n=== Patient Medical History Report ===");

            Console.Write("Enter patient ID: ");
            int patientId = int.Parse(Console.ReadLine());

            // LINQ: FirstOrDefault() to find the patient
            Patient patient = context.Patients.FirstOrDefault(p => p.patientId == patientId);

            if (patient == null)
            {
                Console.WriteLine("Patient not found.");
                return;
            }

            // LINQ: Where() to get all records for this patient
            List<MedicalRecord> records = context.MedicalRecords
                .Where(r => r.patientId == patientId)
                .ToList();

            if (records.Count == 0)
            {
                Console.WriteLine("No medical records found for this patient.");
                return;
            }

            Console.WriteLine($"\n--- Medical History for {patient.patientName} (ID: {patientId}) ---");

            records.ForEach(r =>
            {
                // LINQ: FirstOrDefault() + Select() to resolve doctor name
                string doctorName = context.Doctors
                    .Where(d => d.doctorId == r.DoctorId)
                    .Select(d => d.doctorName)
                    .FirstOrDefault() ?? "Unknown";

                Console.WriteLine($"\n  Record ID   : {r.recordId}");
                Console.WriteLine($"  Visit Date  : {r.visitDate}");
                Console.WriteLine($"  Doctor      : {doctorName}");
                Console.WriteLine($"  Diagnosis   : {r.diagonsis}");
                Console.WriteLine($"  Prescription: {r.prescription}");
                Console.WriteLine($"  Fee Charged : {r.visitFee:C}");
                Console.WriteLine("  " + new string('-', 50));
            });

            // LINQ: Sum() to total all fees
            decimal totalCharged = records.Sum(r => r.visitFee);
            Console.WriteLine($"\n  TOTAL AMOUNT CHARGED: {totalCharged:C}");
        }
        

        public static void DoctorWorkloadAndRevenueSammary(HospitalContext context)
        {
            Console.WriteLine("Enter doctor id:");
            int doctorId = int.Parse(Console.ReadLine());

            var doctor = context.Doctors.FirstOrDefault(d => d.doctorId == doctorId);

            if (doctor == null)
            {
                Console.WriteLine("Doctor not found");
            }


            foreach (var doctors in context.Doctors)
            {
                int completed = 0;
                int cancelled = 0;
                decimal revenue = 0;

                foreach (var appointment in context.Appointments)
                {
                    if (appointment.doctorId == doctors.doctorId)
                    {
                        if (appointment.status == "Completed")
                        {
                            completed++;

                            
                var record = context.MedicalRecords.FirstOrDefault(m => m.DoctorId == doctors.doctorId);

                            if (record != null)
                            {
                                revenue += record.visitFee; 
                            }
                        }

                        if (appointment.status == "Cancelled")
                        {
                            cancelled++;
                        }
                    }
                }

              
                Console.WriteLine($"Doctor Name: {doctors.doctorName}");
                Console.WriteLine($"Completed: {completed}");
                Console.WriteLine($"Revenue: {revenue} OMR");
                Console.WriteLine($"Cancelled: {cancelled}");
            }
        }

        public static void AddDoctorDepartment(HospitalContext context)
        {

            Console.WriteLine("Enter Doctor ID:");
            int doctorId = int.Parse(Console.ReadLine());

            var doctor = context.Doctors.FirstOrDefault(d => d.doctorId == doctorId);

            if (doctor == null)
            {
                Console.WriteLine("Doctor not found");
                return;
            }

            Console.WriteLine("Available Departments:");

            foreach (Department department in context.Departments)
            {
                Console.WriteLine($"Department id :{department.dapartmentId} - {department.departmentname}"
                );
            }

            Console.WriteLine("Enter Department ID:");

            int departmentId =
            int.Parse(Console.ReadLine());

            var foundDepartment = context.Departments.FirstOrDefault(d => d.dapartmentId == departmentId);

            if (foundDepartment == null)
            {
                Console.WriteLine("Department not found");
                return;
            }

            doctor.departmentId = departmentId;

            Console.WriteLine
            ($"Doctor name : {doctor.doctorName} added to {foundDepartment.departmentname}");
        }

        public static void HospitalReview(HospitalContext context)
        {
            Console.WriteLine("Enter Patient ID:");

            int patientId = int.Parse(Console.ReadLine());

            var patient = context.Patients.FirstOrDefault(p => p.patientId == patientId);

            if (patient == null)
            {
                Console.WriteLine("Patient not found");
                return;
            }

            Console.WriteLine("Enter Rating (1-5):");

            int rating = int.Parse(Console.ReadLine());

            if (rating < 1 || rating > 5)
            {
                Console.WriteLine("Invalid Rating");
                return;
            }

            Console.WriteLine("Enter Comment:");

            string comment = Console.ReadLine();

            int reviewId = (context.reviews.Count) + 1;

            context.reviews.Add (new HospitalReview
            {
               reviewId = reviewId,
               patientId = patientId,
               rating = rating,
               comment = comment

            });

            Console.WriteLine("Review Added successfuly");
        
        }
        

        public static void Main(string[] args)
        {
            HospitalContext context = new HospitalContext();

            context.Patients = new List<Patient>()
            {
                new Patient(1,"Fatma" , 20 ,"Female", "A1233" , "Fatma@gmail.com" , "A+"),
                new Patient(2,"Mohammed" , 25 ,"Male", "A1245" , "Mohammed@gmail.com" , "O+"),
                new Patient(3,"Karim" , 27 ,"Male", "A1343" , "Karim@gmail.com" , "AB+"),
                new Patient(4,"Aysha" , 40 ,"Female", "A1293" , "Aysha@gmail.com" , "A+"),
            };


            context.Appointments = new List<Appointment>();
            context.availableSlots = new List<AvailableSlots>();
            context.Doctors = new List<Doctor>();
            context.MedicalRecords = new List<MedicalRecord>();
            context.Departments = new List<Department>();
            context.reviews = new List<HospitalReview>();

            bool exit = false;
            while (exit == false) {
                Console.WriteLine("Welcome to Hospital system");
                Console.WriteLine("1. Registration Patients");
                Console.WriteLine("2. Add a new Doctor");
                Console.WriteLine("3. View All Patient");
                Console.WriteLine("4. View All Doctor by specialization");
                Console.WriteLine("5. Add an Available Time solt for Doctor");
                Console.WriteLine("6. Booking an Appointment");
                Console.WriteLine("7. Cancle an Appointment");
                Console.WriteLine("8. Create a Medical record after a visit");
                Console.WriteLine("9. Generate a patient medical history report");
                Console.WriteLine("10. Doctor workload and revenue sammary");
                Console.WriteLine("11. Add Doctor to Department");
                Console.WriteLine("12. Hospital Review by patients");
                Console.WriteLine("13. Exit");

                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        RegistrationPatients(context.Patients);
                        break;

                    case 2:
                        AddNewDoctor(context.Doctors);
                        break;

                    case 3:
                        ViewAllPatient(context.Patients);
                        break;

                    case 4:
                        viewAlldoctor(context.Doctors);
                        break;

                    case 5:
                        AddAvailableTimeSoltDoctor(context);
                        break;

                    case 6:
                        BookinganAppointment(context);
                        break;

                    case 7:
                        CancleAppointment(context);
                        break;

                    case 8:
                        CreateMedicalRecordAfterVisit(context);
                        break;

                    case 9:
                        patientMedicalHistoryReport(context);
                        break;

                    case 10:
                        DoctorWorkloadAndRevenueSammary(context);
                        break;

                    case 11:
                        AddDoctorDepartment(context);
                        break;

                    case 12:
                        HospitalReview(context);
                        break;

                    case 13:
                        exit = true;
                        break;
                         
                    default:
                        Console.WriteLine("Invalid option");
                        break;

                }
            }
            }
    }
}
