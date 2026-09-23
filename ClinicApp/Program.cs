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
    }
}