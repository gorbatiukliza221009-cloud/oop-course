namespace ClinicApp;

public static class ClinicFormatter
{
    public static string FormatBloodType(BloodType bt)
    {
        return bt switch
        {
            BloodType.Unknown => "Невідомо",
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
            Speciality.General => "Загальна",
            Speciality.Cardiology => "Кардіологія",
            Speciality.Neurology => "Неврологія",
            Speciality.Pediatrics => "Педіатрія",
            Speciality.Surgery => "Хірургія",
            Speciality.Orthopedics => "Ортопедія",
            Speciality.Dermatology => "Дерматологія",
            Speciality.Emergency => "Швидка допомога",
            _ => "Невідомо"
        };
    }

    public static string FormatAge(int age)
    {
        int lastTwoDigits = age % 100;
        int lastDigit = age % 10;

        if (lastTwoDigits >= 11 && lastTwoDigits <= 19)
        {
            return $"{age} років";
        }

        if (lastDigit == 1)
        {
            return $"{age} рік";
        }

        if (lastDigit >= 2 && lastDigit <= 4)
        {
            return $"{age} роки";
        }

        return $"{age} років";
    }

    public static string FormatPhone(string phone)
    {
        if (phone.Length != 10)
        {
            return phone;
        }

        foreach (char symbol in phone)
        {
            if (symbol < '0' || symbol > '9')
            {
                return phone;
            }
        }

        return $"({phone.Substring(0, 3)}) " +
               $"{phone.Substring(3, 3)}-" +
               $"{phone.Substring(6, 4)}";
    }
}