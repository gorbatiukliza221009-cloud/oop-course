namespace ClinicApp;

internal class Program
{
    static void Main()
    {
        Clinic clinic = new Clinic("Медична клініка");
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
            clinic.Patients.Add(patient);
        }

        

        void PatientMenu(Clinic clinic)
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
                        clinic.Patients.DisplayAll();
                        break;

                    case "2":
                        Console.Write("Ім'я: ");
                        string firstName = Console.ReadLine()!;
                        Console.Write("Прізвище: ");
                        string lastName = Console.ReadLine()!;

                        clinic.Patients.Add(new Patient(firstName, lastName));
                        break;

                    case "3":
                        Console.Write("Ім'я або прізвище для пошуку: ");
                        string query = Console.ReadLine()!;
                        Patient[] found = clinic.Patients.FindByName(query);

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

                        if (clinic.Patients.Remove(id))
                        {
                            Console.WriteLine("Пацієнта видалено.");
                        }
                        else
                        {
                            Console.WriteLine("Пацієнта з таким ID не знайдено.");
                        }
                        break;

                    case "5":
                        clinic.Patients.DisplayStats();
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

        

        foreach (Doctor doctor in doctors)
        {
            clinic.Doctors.Add(doctor);
        }
        MainMenu(clinic);



        void DoctorManagerMenu(Clinic clinic)
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
                        clinic.Doctors.DisplayAll();
                        break;
                    case "2":
                        Console.Write("Ім'я: ");
                        string firstName = Console.ReadLine()!;
                        Console.Write("Прізвище: ");
                        string lastName = Console.ReadLine()!;
                        Console.Write("Спеціальність: ");
                        string specialty = Console.ReadLine()!;
                        clinic.Doctors.Add(new Doctor(firstName, lastName, specialty));
                        break;
                    case "3":
                        Console.Write("Спеціальність для пошуку: ");
                        string query = Console.ReadLine()!;
                        Doctor[] found = clinic.Doctors.FindBySpeciality(query);
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
                        if (clinic.Doctors.Remove(id))
                        {
                            Console.WriteLine("Лікаря видалено.");
                        }
                        else
                        {
                            Console.WriteLine("Лікаря з таким ID не знайдено.");
                        }
                        break;
                    case "5":
                        clinic.Doctors.DisplayStats();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невідомий пункт меню.");
                        break;
                }
            }
        }

        void AppointmentMenu(Clinic clinic)
        {
            while (true)
            {
                Console.WriteLine("\n=== Записи ===");
                Console.WriteLine("1 — Створити запис");
                Console.WriteLine("2 — Майбутні записи");
                Console.WriteLine("3 — Записи пацієнта");
                Console.WriteLine("4 — Записи лікаря");
                Console.WriteLine("5 — Записи на дату");
                Console.WriteLine("6 — Скасувати запис");
                Console.WriteLine("7 — Завершити запис");
                Console.WriteLine("0 — Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        clinic.Patients.DisplayAll();
                        clinic.Doctors.DisplayAll();

                        Console.Write("ID пацієнта: ");
                        if (!int.TryParse(Console.ReadLine(), out int patientId))
                        {
                            Console.WriteLine("Некоректний ID пацієнта.");
                            break;
                        }

                        Console.Write("ID лікаря: ");
                        if (!int.TryParse(Console.ReadLine(), out int doctorId))
                        {
                            Console.WriteLine("Некоректний ID лікаря.");
                            break;
                        }

                        Console.Write("Дата й час (дд.мм.рррр гг:хх): ");
                        string dateText = Console.ReadLine()!;

                        if (!DateTime.TryParseExact(
                                dateText,
                                "dd.MM.yyyy HH:mm",
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None,
                                out DateTime scheduledAt))
                        {
                            Console.WriteLine("Некоректна дата або час.");
                            break;
                        }

                        Console.Write("Тривалість у хвилинах: ");
                        if (!int.TryParse(Console.ReadLine(), out int duration)
                            || duration <= 0)
                        {
                            Console.WriteLine("Введіть додатну кількість хвилин.");
                            break;
                        }

                        clinic.Appointments.Book(patientId, doctorId, scheduledAt, duration);
                        break;

                    case "2":
                        Console.WriteLine("\nМайбутні записи:");
                        clinic.Appointments.DisplayList(clinic.Appointments.GetUpcoming());
                        break;

                    case "3":
                        clinic.Patients.DisplayAll();
                        Console.Write("ID пацієнта: ");

                        if (int.TryParse(Console.ReadLine(), out int searchPatientId))
                        {
                            clinic.Appointments.DisplayList(
                                clinic.Appointments.GetByPatient(searchPatientId));
                        }
                        else
                        {
                            Console.WriteLine("Некоректний ID.");
                        }
                        break;

                    case "4":
                        clinic.Doctors.DisplayAll();
                        Console.Write("ID лікаря: ");

                        if (int.TryParse(Console.ReadLine(), out int searchDoctorId))
                        {
                            clinic.Appointments.DisplayList(
                                clinic.Appointments.GetByDoctor(searchDoctorId));
                        }
                        else
                        {
                            Console.WriteLine("Некоректний ID.");
                        }
                        break;

                    case "5":
                        Console.Write("Дата (дд.мм.рррр): ");
                        string searchDateText = Console.ReadLine()!;

                        if (DateTime.TryParseExact(
                                searchDateText,
                                "dd.MM.yyyy",
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None,
                                out DateTime searchDate))
                        {
                            clinic.Appointments.DisplayList(
                                clinic.Appointments.GetByDate(searchDate));
                        }
                        else
                        {
                            Console.WriteLine("Некоректна дата.");
                        }
                        break;

                    case "6":
                        Console.Write("ID запису: ");
                        if (!int.TryParse(Console.ReadLine(), out int cancelId))
                        {
                            Console.WriteLine("Некоректний ID.");
                            break;
                        }

                        Console.Write("Причина скасування: ");
                        string reason = Console.ReadLine()!;

                        if (clinic.Appointments.Cancel(cancelId, reason))
                        {
                            Console.WriteLine($"Запис [{cancelId}] скасовано.");
                        }
                        else
                        {
                            Console.WriteLine("Запис не знайдено або його вже закрито.");
                        }
                        break;

                    case "7":
                        Console.Write("ID запису: ");
                        if (!int.TryParse(Console.ReadLine(), out int completeId))
                        {
                            Console.WriteLine("Некоректний ID.");
                            break;
                        }

                        if (clinic.Appointments.Complete(completeId))
                        {
                            Console.WriteLine($"Запис [{completeId}] завершено.");
                        }
                        else
                        {
                            Console.WriteLine("Запис не знайдено або його вже закрито.");
                        }
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Невідомий пункт меню.");
                        break;
                }
            }
        }
        void MainMenu(Clinic clinic)
        {
            while (true)
            {
                Console.WriteLine($"\n=== {clinic.Name} ===");
                Console.WriteLine("1 — Пацієнти");
                Console.WriteLine("2 — Лікарі");
                Console.WriteLine("3 — Записи");
                Console.WriteLine("4 — Розклад на дату");
                Console.WriteLine("5 — Звіт");
                Console.WriteLine("6 — Тест зростаючого масиву");
                Console.WriteLine("0 — Завершити програму");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        PatientMenu(clinic);
                        break;

                    case "2":
                        DoctorManagerMenu(clinic);
                        break;

                    case "3":
                        AppointmentMenu(clinic);
                        break;

                    case "4":
                        Console.Write("Дата (дд.мм.рррр): ");
                        string dateText = Console.ReadLine()!;

                        if (DateTime.TryParseExact(
                                dateText,
                                "dd.MM.yyyy",
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None,
                                out DateTime date))
                        {
                            clinic.DisplaySchedule(date);
                        }
                        else
                        {
                            Console.WriteLine("Некоректна дата.");
                        }
                        break;

                    case "5":
                        clinic.GenerateReport();
                        break;

                    case "6":
                        TestGrowablePatients();
                        break;  

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Невідомий пункт меню.");
                        break;
                }
            }
        }

        void TestGrowablePatients()
        {
            GrowablePatientManager growable = new GrowablePatientManager();
            int tenthPatientId = 0;

            Console.WriteLine("\n=== Тест GrowablePatientManager ===");
            Console.WriteLine("Додаємо пацієнтів одного за одним...");

            for (int i = 1; i <= 20; i++)
            {
                Patient patient = new Patient("Тест", $"Пацієнт{i}");
                growable.Add(patient);

                if (i == 10)
                {
                    tenthPatientId = patient.Id;
                }
            }

            Console.WriteLine("\nТест пошуку:");

            Patient? found = growable.FindById(tenthPatientId);
            if (found != null)
            {
                Console.WriteLine($"  FindById({tenthPatientId}) → {found.FullName}");
            }

            Patient? missing = growable.FindById(999999);
            if (missing == null)
            {
                Console.WriteLine("  FindById(999999) → не знайдено");
            }

            Console.WriteLine($"\nGrowablePatientManager: " +
                              $"{growable.Count} пацієнтів / {growable.Capacity} місць");
        }
    }
}