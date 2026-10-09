using System;

namespace ClinicApp.Utils;

public static class ClinicValidator
{
    public static void ValidateName(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
            throw new ArgumentException(
                "Ім’я або прізвище має містити від 1 до 50 символів і не може складатися лише з пробілів.",
                fieldName);
    }

    public static void ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone) || phone.Length != 10)
            throw new ArgumentException(
                "Телефон має містити рівно 10 цифр.",
                nameof(phone));

        foreach (char symbol in phone)
        {
            if (symbol < '0' || symbol > '9')
                throw new ArgumentException(
                    "Телефон має містити лише цифри від 0 до 9.",
                    nameof(phone));
        }
    }

    public static void ValidateDate(DateTime value, string fieldName)
    {
        if (value.Date > DateTime.Today || value.Year < 1900)
            throw new ArgumentOutOfRangeException(
                fieldName,
                "Дата народження має бути не раніше 1900 року та не пізніше сьогодні.");
    }

    public static void ValidatePositive(int value, string fieldName)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(
                fieldName,
                "Значення має бути більшим за нуль.");
    }
}