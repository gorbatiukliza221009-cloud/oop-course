using System;

namespace ClinicApp
{
    internal class Doctor
    {
        private static int _nextId = 1;
        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Speciality { get; set; }
        public string LicenseNumber { get; set; }
        public string Phone { get; set; }
        public int WorkStartHour { get; set; }
        public int WorkEndHour { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        public int WorkingHoursPerDay
        {
            get { return WorkEndHour - WorkStartHour; }
        }
        public string WorkSchedule
        {
            get { return $"{WorkStartHour:D2}:00 - {WorkEndHour:D2}:00"; }
        }
        public bool IsAvailableNow
        {
            get
            {
                int currentHour = DateTime.Now.Hour;
                return currentHour >= WorkStartHour && currentHour < WorkEndHour;
            }
        }
        public Doctor()
            : this("Невідомий", "Лікар", "Невідомо", "Невідомо", "0000000000")
        {

        }
        public Doctor(string firstName, string lastName, string speciality)
            : this(firstName, lastName, speciality, "Невідомо", "0000000000")
        {
        }
        public Doctor(string firstName, string lastName, string speciality, string licenseNumber, string phone)
        {
            Id = _nextId++;
            FirstName = firstName;
            LastName = lastName;
            Speciality = speciality;
            LicenseNumber = licenseNumber;
            Phone = phone;
        }
        
        public bool CanAcceptAt(int hour)
        {
            return hour >= WorkStartHour && hour < WorkEndHour;
        }
        public override string ToString()
        {
            string status;

            if(IsAvailableNow)
            {
                status = "доступний зараз";
            }
            else
            {
                status = "не в робочий час";
            }
            return $"[{Id}] {FullName} | {Speciality} | {LicenseNumber} | Тел: {Phone} | {WorkSchedule} | {status}";
        }


    }
}
