using ClinicApp.Enums;
using ClinicApp.Utils;
using System;

namespace ClinicApp.Models
{
    public class Doctor
    {
        private static int _nextId = 1;
        private string _firstName = "";
        private string _lastName = "";
        private string _licenseNumber = "";
        private string _phone = "";
        public int Id { get; }
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
                    throw new ArgumentException(
                        "Ім’я має містити від 1 до 50 символів і не може складатися лише з пробілів.",
                        nameof(FirstName));

                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
                    throw new ArgumentException(
                        "Прізвище має містити від 1 до 50 символів і не може складатися лише з пробілів.",
                        nameof(LastName));

                _lastName = value;
            }
        }

        public Speciality Speciality { get; set; }

        public string LicenseNumber
        {
            get => _licenseNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(
                        "Номер ліцензії не може бути порожнім або складатися лише з пробілів.",
                        nameof(LicenseNumber));

                _licenseNumber = value;
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length != 10)
                    throw new ArgumentException(
                        "Телефон має містити рівно 10 цифр.",
                        nameof(Phone));

                foreach (char symbol in value)
                {
                    if (symbol < '0' || symbol > '9')
                        throw new ArgumentException(
                            "Телефон має містити лише цифри від 0 до 9.",
                            nameof(Phone));
                }

                _phone = value;
            }
        }
        public WorkSchedule Schedule { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        public int WorkingHoursPerDay
        {
            get { return Schedule.HoursPerDay; }
        }
        public string WorkSchedule
        {
            get { return Schedule.ToString(); }
        }
        public bool IsAvailableNow
        {
            get { return Schedule.IsNow; }
        }
        public Doctor()
    : this("Невідомий", "Лікар", Speciality.General,
           "Невідомо", "0000000000")
        {
        }

        public Doctor(string firstName, string lastName, Speciality speciality)
            : this(firstName, lastName, speciality,
                   "Невідомо", "0000000000")
        {
        }

        public Doctor(
    string firstName,
    string lastName,
    Speciality speciality,
    string licenseNumber,
    string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Speciality = speciality;
            LicenseNumber = licenseNumber;
            Phone = phone;
            Schedule = new WorkSchedule(8, 17);
            Id = _nextId++;
        }

        public bool CanAcceptAt(int hour)
        {
            return Schedule.Contains(hour);
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
            return $"[{Id}] {FullName} | {ClinicFormatter.FormatSpeciality(Speciality)} | {LicenseNumber} | Тел: {ClinicFormatter.FormatPhone(Phone)} | {Schedule} | {status}";
        }


    }
}
