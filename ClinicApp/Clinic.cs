using System;

namespace ClinicApp
{
    public class Clinic
    {
        public string Name { get; }
        public PatientManager Patients { get; }
        public DoctorManager Doctors { get; }
        public AppointmentManager Appointments { get; }

        public Clinic(string name)
        {
            Name = name;
            Patients = new PatientManager();
            Doctors = new DoctorManager();
            Appointments = new AppointmentManager(Patients, Doctors);
        }

        public void DisplaySchedule(DateTime date)
        {
            Console.WriteLine($"\n=== Розклад на {date:dd.MM.yyyy} ===");

            Appointment[] appointments = Appointments.GetByDate(date);
            Appointments.DisplayList(appointments);
        }

        public void GenerateReport()
        {
            Appointment[] upcoming = Appointments.GetUpcoming();
            Doctor[] doctors = Doctors.GetAll();

            Console.WriteLine($"\n=== Звіт - {Name} ===");
            Console.WriteLine($"Пацієнтів: {Patients.Count}");
            Console.WriteLine($"Лікарів: {Doctors.Count}");
            Console.WriteLine($"Майбутніх записів: {upcoming.Length}");
            Console.WriteLine("Навантаження лікарів (майбутні записи):");
            foreach (Doctor doctor in doctors)
            {
                int appointmentCount = 0;

                foreach (Appointment appointment in upcoming)
                {
                    if (appointment.DoctorId == doctor.Id)
                    {
                        appointmentCount++;
                    }
                }
                Console.WriteLine($"  {doctor.FullName} ({doctor.Speciality}): " +
                $"{appointmentCount} записів");
            }
        }

    }
}
