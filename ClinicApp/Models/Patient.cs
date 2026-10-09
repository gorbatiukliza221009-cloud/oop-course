using ClinicApp.Enums;
using ClinicApp.Utils;
using System;

namespace ClinicApp.Models;

public class Patient
{
    private static int _nextId = 1;
    private string _firstName = "";
    private string _lastName = "";
    private DateTime _dateOfBirth;
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

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            if (value.Date > DateTime.Today || value.Year < 1900)
                throw new ArgumentOutOfRangeException(
                    nameof(DateOfBirth),
                    "Дата народження має бути не раніше 1900 року та не пізніше сьогодні.");

            _dateOfBirth = value;
        }
    }

    public BloodType BloodType { get; set; }

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
    public string Email { get; set; }

    public string FullName
    {
        get { return FirstName + " " + LastName; }
    }

    public int Age
    {
        get
        {
            DateTime today = DateTime.Today;
            int age = today.Year - DateOfBirth.Year;

            if (DateOfBirth.Date > today.AddYears(-age))
                age--;
            return age;
        }
    }

    public bool IsAdult
    {
        get { return Age >= 18; }
    }

    public Patient()
        : this("Невідомий", "Пацієнт", DateTime.Today.AddYears(-26), BloodType.Unknown, "0000000000")
    {

    }

    public Patient(string firstName, string lastName)
        : this(firstName, lastName, DateTime.Today.AddYears(-26), BloodType.Unknown, "0000000000")
    {

    }

    public Patient(string firstName, string lastName, DateTime dateOfBirth, BloodType bloodType, string phone)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        BloodType = bloodType;
        Phone = phone;
        Email = "";
        Id = _nextId++;
    }

    public string GetAgeCategory()
    {
        if (Age < 18)
            return "дитина";
        else if (Age < 60)
            return "дорослий";
        else
            return "літній";
    }

    public override string ToString()
    {
        return $"[{Id}] {FullName} | Вік: {ClinicFormatter.FormatAge(Age)} ({GetAgeCategory()}) " +
       $"| Кров: {ClinicFormatter.FormatBloodType(BloodType)} | Тел: {ClinicFormatter.FormatPhone(Phone)}";
    }
}
