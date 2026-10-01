using System;

namespace ClinicApp
{
    static class ClinicFormatter
    {
        public static string FormatBloodType(BloodType bt)
        {
            return bt switch
            {
                BloodType.APositive => "A+",
                BloodType.ANegative => "A-",
                BloodType.BPositive => "B+",
                BloodType.BNegative => "B-",
                BloodType.ABPositive => "AB+",
                BloodType.ABNegative => "AB-",
                BloodType.OPositive => "O+",
                BloodType.ONegative => "O-",
                _ => "Невідомо"
            };
        }

        public static string FormatSpeciality(Speciality s)
        {
            return s switch
            {
                Speciality.General => "Загальна практика",
                Speciality.Cardiology => "Кардіологія",
                Speciality.Dermatology => "Дерматологія",
                Speciality.Neurology => "Неврологія",
                Speciality.Pediatrics => "Педіатрія",
                Speciality.Orthopedics => "Ортопедія",
                Speciality.Surgery => "Хірургія",
                Speciality.Emergency => "Невідкладна допомога",
                _ => "Невідомо"
            };
        }

        public static string FormatAge(int age)
        {
            int lastTwoDigits = age % 100;
            int lastDigit = age % 10;

            string word;

            if (lastTwoDigits >= 11 && lastTwoDigits <= 19)
            {
                word = "років";
            }
            else
            {
                word = lastDigit switch
                {
                    1 => "рік",
                    2 or 3 or 4 => "роки",
                    _ => "років"
                };
            }
            return $"{age} {word}";
        }

        public static string FormatPhone(string phone)
        {
            if (phone.Length != 10)
            {
                return phone;
            }

            foreach (char symbol in phone)
            {
                if (symbol< '0' || symbol> '9')
                {
                    return phone;
                }
            }

            return $"({phone.Substring(0, 3)}) " +
                   $"{phone.Substring(3, 3)}-" +
                   $"{phone.Substring(6, 4)}";
        }
    }
}
