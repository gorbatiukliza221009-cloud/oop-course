using System;
using System.Text.RegularExpressions;

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
        if (string.IsNullOrWhiteSpace(phone) ||
            !Regex.IsMatch(phone, @"^[0-9]{10}\z"))
        {
            throw new ArgumentException(
                "Телефон має містити рівно 10 цифр від 0 до 9.",
                nameof(phone));
        }
    }

    public static void ValidateEmail(string email)
    {
        if (email == "")
            return;

        if (string.IsNullOrWhiteSpace(email) ||
            !Regex.IsMatch(email, @"^[^@\s]+@[^@\s.]+(\.[^@\s.]+)+\z"))
        {
            throw new ArgumentException(
                "Некоректний email. Приклад: ivan@mail.com.",
                nameof(email));
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