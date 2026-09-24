namespace ClinicApp;

internal class Program
{
    static void Main()
    {
        Patient[] patients =
        {
            new Patient("Іван", "Петренко",
                        DateTime.Today.AddYears(-41), "A+", "0501234567"),
            new Patient("Олена", "Коваль",
                        DateTime.Today.AddYears(-33), "B-", "0672345678"),
            new Patient("Максим", "Бойко",
                        DateTime.Today.AddYears(-16), "O+", "0933456789"),
            new Patient(),
            new Patient("Марія", "Ткач")
        };

        foreach (Patient patient in patients)
        {
            Console.WriteLine(patient);
        }

        Doctor[] doctors =
        {
            new Doctor("Олег", "Сидоренко", "Кардіологія", "LIC-001", "0441234567"),
            new Doctor("Наталія", "Мороз", "Неврологія", "LIC-002", "0442345678"),
            new Doctor("Андрій", "Власенко", "Педіатрія", "LIC-003", "0443456789"),
        };

        doctors[0].WorkEndHour = 16;
        doctors[1].WorkStartHour = 9;
        doctors[1].WorkEndHour = 18;

        foreach (Doctor doctor in doctors)
        {
            Console.WriteLine(doctor);
        }
    }
}