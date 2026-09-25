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

        PatientManager manager = new PatientManager();

        foreach (Patient patient in patients)
        {
            manager.Add(patient);
        }

        PatientMenu(manager);

        void PatientMenu(PatientManager patientManager)
        {
            while (true)
            {
                Console.WriteLine("\n=== Пацієнти ===");
                Console.WriteLine("1 — Показати всіх");
                Console.WriteLine("2 — Додати");
                Console.WriteLine("3 — Знайти за ім'ям");
                Console.WriteLine("4 — Видалити за ID");
                Console.WriteLine("5 — Статистика");
                Console.WriteLine("0 — Вихід із меню");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        patientManager.DisplayAll();
                        break;

                    case "2":
                        Console.Write("Ім'я: ");
                        string firstName = Console.ReadLine()!;
                        Console.Write("Прізвище: ");
                        string lastName = Console.ReadLine()!;

                        patientManager.Add(new Patient(firstName, lastName));
                        break;

                    case "3":
                        Console.Write("Ім'я або прізвище для пошуку: ");
                        string query = Console.ReadLine()!;
                        Patient[] found = patientManager.FindByName(query);

                        if (found.Length == 0)
                        {
                            Console.WriteLine("Пацієнтів не знайдено.");
                        }
                        else
                        {
                            foreach (Patient patient in found)
                            {
                                Console.WriteLine(patient);
                            }
                        }
                        break;

                    case "4":
                        Console.Write("ID пацієнта: ");
                        int id = int.Parse(Console.ReadLine()!);

                        if (patientManager.Remove(id))
                        {
                            Console.WriteLine("Пацієнта видалено.");
                        }
                        else
                        {
                            Console.WriteLine("Пацієнта з таким ID не знайдено.");
                        }
                        break;

                    case "5":
                        patientManager.DisplayStats();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Невідомий пункт меню.");
                        break;
                }
            }
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

        DoctorManager doctorManager = new DoctorManager();

        foreach (Doctor doctor in doctors)
        {
            doctorManager.Add(doctor);
        }

        DoctorManagerMenu(doctorManager);

        void DoctorManagerMenu(DoctorManager doctorManager)
        {
            while (true)
            {
                Console.WriteLine("\n=== Лікарі ===");
                Console.WriteLine("1 — Показати всіх");
                Console.WriteLine("2 — Додати");
                Console.WriteLine("3 — Знайти за спеціальністю");
                Console.WriteLine("4 — Видалити за ID");
                Console.WriteLine("5 — Статистика");
                Console.WriteLine("0 — Вихід із меню");
                Console.Write("Ваш вибір: ");
                string choice = Console.ReadLine()!;
                switch (choice)
                {
                    case "1":
                        doctorManager.DisplayAll();
                        break;
                    case "2":
                        Console.Write("Ім'я: ");
                        string firstName = Console.ReadLine()!;
                        Console.Write("Прізвище: ");
                        string lastName = Console.ReadLine()!;
                        Console.Write("Спеціальність: ");
                        string specialty = Console.ReadLine()!;
                        doctorManager.Add(new Doctor(firstName, lastName, specialty));
                        break;
                    case "3":
                        Console.Write("Спеціальність для пошуку: ");
                        string query = Console.ReadLine()!;
                        Doctor[] found = doctorManager.FindBySpeciality(query);
                        if (found.Length == 0)
                        {
                            Console.WriteLine("Лікарів не знайдено.");
                        }
                        else
                        {
                            foreach (Doctor doctor in found)
                            {
                                Console.WriteLine(doctor);
                            }
                        }
                        break;
                    case "4":
                        Console.Write("ID лікаря: ");
                        int id = int.Parse(Console.ReadLine()!);
                        if (doctorManager.Remove(id))
                        {
                            Console.WriteLine("Лікаря видалено.");
                        }
                        else
                        {
                            Console.WriteLine("Лікаря з таким ID не знайдено.");
                        }
                        break;
                    case "5":
                        doctorManager.DisplayStats();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невідомий пункт меню.");
                        break;
                }
            }
        }



    }
}