using ClinicApp.Enums;
using ClinicApp.Managers;
using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp;

internal class Program
{
    static void Main()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture =
    System.Globalization.CultureInfo.InvariantCulture;
        System.Threading.Thread.CurrentThread.CurrentUICulture =
            System.Globalization.CultureInfo.InvariantCulture;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Clinic clinic = new Clinic("Медична клініка");
        Patient[] patients =
        {
            new Patient("Іван", "Петренко",
                        DateTime.Today.AddYears(-41), BloodType.APositive, "0501234567"),
            new Patient("Олена", "Коваль",
                        DateTime.Today.AddYears(-33), BloodType.BNegative, "0672345678"),
            new Patient("Максим", "Бойко",
                        DateTime.Today.AddYears(-16), BloodType.OPositive, "0933456789"),
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
                        {
                            try
                            {
                                Console.Write("Ім'я: ");
                                string firstName = Console.ReadLine()!;

                                Console.Write("Прізвище: ");
                                string lastName = Console.ReadLine()!;

                                Console.Write("Дата народження — дд.мм.рррр: ");
                                if (!DateTime.TryParseExact(
                                    Console.ReadLine(),
                                    "dd.MM.yyyy",
                                    System.Globalization.CultureInfo.InvariantCulture,
                                    System.Globalization.DateTimeStyles.None,
                                    out DateTime dateOfBirth))
                                {
                                    Console.WriteLine("Помилка: некоректний формат дати.");
                                    break;
                                }

                                Console.Write("Телефон — 10 цифр: ");
                                string phone = Console.ReadLine()!;

                                Console.WriteLine("Група крові:");
                                foreach (BloodType value in Enum.GetValues<BloodType>())
                                {
                                    Console.WriteLine(
                                        $"{(int)value} — {ClinicFormatter.FormatBloodType(value)}");
                                }

                                Console.Write("Введіть номер: ");
                                if (!int.TryParse(Console.ReadLine(), out int bloodTypeNumber))
                                {
                                    Console.WriteLine("Помилка: введіть ціле число.");
                                    break;
                                }

                                if (!Enum.IsDefined(typeof(BloodType), bloodTypeNumber))
                                {
                                    Console.WriteLine("Помилка: такої групи крові немає.");
                                    break;
                                }

                                BloodType bloodType = (BloodType)bloodTypeNumber;

                                Patient newPatient = new Patient(
                                    firstName, lastName, dateOfBirth, bloodType, phone);

                                clinic.Patients.Add(newPatient);
                            }
                            catch (ArgumentOutOfRangeException e)
                            {
                                Console.WriteLine("Помилка: " + e.Message);
                            }
                            catch (ArgumentException e)
                            {
                                Console.WriteLine("Помилка: " + e.Message);
                            }

                            break;
                        }


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
            new Doctor("Олег", "Сидоренко", Speciality.Cardiology, "LIC-001", "0441234567"),
            new Doctor("Наталія", "Мороз", Speciality.Neurology, "LIC-002", "0442345678"),
            new Doctor("Андрій", "Власенко", Speciality.Pediatrics, "LIC-003", "0443456789"),
        };

        doctors[0].Schedule = new WorkSchedule(8, 16);
        doctors[1].Schedule = new WorkSchedule(9, 18);



        foreach (Doctor doctor in doctors)
        {
            clinic.Doctors.Add(doctor);
        }
     



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
                        {
                            try
                            {
                                Console.Write("Ім'я: ");
                                string firstName = Console.ReadLine()!;

                                Console.Write("Прізвище: ");
                                string lastName = Console.ReadLine()!;

                                Console.WriteLine("Спеціальність:");
                                foreach (Speciality value in Enum.GetValues<Speciality>())
                                {
                                    Console.WriteLine(
                                        $"{(int)value} — {ClinicFormatter.FormatSpeciality(value)}");
                                }

                                Console.Write("Введіть номер: ");
                                if (!int.TryParse(Console.ReadLine(), out int specialityNumber))
                                {
                                    Console.WriteLine("Помилка: введіть ціле число.");
                                    break;
                                }

                                if (!Enum.IsDefined(typeof(Speciality), specialityNumber))
                                {
                                    Console.WriteLine("Помилка: такої спеціальності немає.");
                                    break;
                                }

                                Speciality speciality = (Speciality)specialityNumber;

                                Console.Write("Номер ліцензії: ");
                                string licenseNumber = Console.ReadLine()!;

                                Console.Write("Телефон — 10 цифр: ");
                                string phone = Console.ReadLine()!;

                                Console.Write("Година початку роботи — 0–23: ");
                                if (!int.TryParse(Console.ReadLine(), out int start))
                                {
                                    Console.WriteLine("Помилка: введіть цілу годину.");
                                    break;
                                }

                                Console.Write("Година завершення роботи — 1–24: ");
                                if (!int.TryParse(Console.ReadLine(), out int end))
                                {
                                    Console.WriteLine("Помилка: введіть цілу годину.");
                                    break;
                                }

                                WorkSchedule schedule = new WorkSchedule(start, end);

                                Doctor newDoctor = new Doctor(
                                    firstName, lastName, speciality, licenseNumber, phone);

                                newDoctor.Schedule = schedule;
                                clinic.Doctors.Add(newDoctor);
                            }
                            catch (ArgumentOutOfRangeException e)
                            {
                                Console.WriteLine("Помилка: " + e.Message);
                            }
                            catch (ArgumentException e)
                            {
                                Console.WriteLine("Помилка: " + e.Message);
                            }

                            break;
                        }

                    case "3":
                        Console.Write("Спеціальність англійською (наприклад Cardiology): ");
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

                        if (!int.TryParse(Console.ReadLine(), out int duration))
                        {
                            Console.WriteLine("Помилка: введіть ціле число.");
                            break;
                        }

                        try
                        {
                            clinic.Appointments.Book(
                                patientId, doctorId, scheduledAt, duration);
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine("Помилка: " + e.Message);
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine("Помилка: " + e.Message);
                        }

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
        void MedicalRecordsMenu(Clinic clinic)
        {
            while (true)
            {
                Console.WriteLine("\n=== Медична картка ===");
                Console.WriteLine("1 — Картка пацієнта");
                Console.WriteLine("2 — Усі записи пацієнта");
                Console.WriteLine("3 — Додати діагноз");
                Console.WriteLine("4 — Додати аналіз");
                Console.WriteLine("5 — Додати рецепт");
                Console.WriteLine("6 — Записи лікаря");
                Console.WriteLine("0 — Назад");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                    case "2":
                        {
                            clinic.Patients.DisplayAll();
                            Console.Write("ID пацієнта: ");

                            if (!int.TryParse(Console.ReadLine(), out int patientId))
                            {
                                Console.WriteLine("Помилка: введіть ціле число.");
                                break;
                            }

                            if (clinic.Patients.FindById(patientId) == null)
                            {
                                Console.WriteLine("Пацієнта не знайдено.");
                                break;
                            }

                            if (choice == "1")
                            {
                                clinic.MedicalRecords.DisplayPatientSummary(patientId);
                            }
                            else
                            {
                                clinic.MedicalRecords.DisplayList(
                                    clinic.MedicalRecords.GetByPatient(patientId));
                            }

                            break;
                        }

                    case "3":
                    case "4":
                    case "5":
                        {
                            clinic.Patients.DisplayAll();
                            Console.Write("ID пацієнта: ");

                            if (!int.TryParse(Console.ReadLine(), out int patientId))
                            {
                                Console.WriteLine("Помилка: введіть ціле число.");
                                break;
                            }

                            if (clinic.Patients.FindById(patientId) == null)
                            {
                                Console.WriteLine("Пацієнта не знайдено.");
                                break;
                            }

                            clinic.Doctors.DisplayAll();
                            Console.Write("ID лікаря: ");

                            if (!int.TryParse(Console.ReadLine(), out int doctorId))
                            {
                                Console.WriteLine("Помилка: введіть ціле число.");
                                break;
                            }

                            if (clinic.Doctors.FindById(doctorId) == null)
                            {
                                Console.WriteLine("Лікаря не знайдено.");
                                break;
                            }

                            try
                            {
                                if (choice == "3")
                                {
                                    Console.Write("Код діагнозу (напр. J06.9): ");
                                    string code = Console.ReadLine()!;

                                    Console.Write("Опис: ");
                                    string description = Console.ReadLine()!;

                                    Console.Write("Хронічне? (1=так, 0=ні): ");
                                    string chronicInput = Console.ReadLine()!;

                                    if (chronicInput != "1" && chronicInput != "0")
                                    {
                                        Console.WriteLine("Помилка: введіть 1 або 0.");
                                        break;
                                    }

                                    clinic.MedicalRecords.Add(new Diagnosis(
                                        patientId, doctorId, DateTime.Today,
                                        code, description, chronicInput == "1"));
                                }
                                else if (choice == "4")
                                {
                                    Console.Write("Назва аналізу: ");
                                    string testName = Console.ReadLine()!;

                                    Console.Write("Значення (число з крапкою): ");
                                    if (!double.TryParse(
                                        Console.ReadLine(),
                                        System.Globalization.NumberStyles.Float,
                                        System.Globalization.CultureInfo.InvariantCulture,
                                        out double value))
                                    {
                                        Console.WriteLine("Помилка: введіть число з крапкою.");
                                        break;
                                    }

                                    Console.Write("Одиниці виміру: ");
                                    string unit = Console.ReadLine()!;

                                    Console.Write("Норма (напр. 120–160): ");
                                    string referenceRange = Console.ReadLine()!;

                                    Console.Write("В нормі? (1=так, 0=ні): ");
                                    string normalInput = Console.ReadLine()!;

                                    if (normalInput != "1" && normalInput != "0")
                                    {
                                        Console.WriteLine("Помилка: введіть 1 або 0.");
                                        break;
                                    }

                                    clinic.MedicalRecords.Add(new LabResult(
                                        patientId, doctorId, DateTime.Today,
                                        testName, value, unit, referenceRange,
                                        normalInput == "1"));
                                }
                                else
                                {
                                    Console.Write("Препарат: ");
                                    string medicationName = Console.ReadLine()!;

                                    Console.Write("Дозування (напр. 10 мг): ");
                                    string dosage = Console.ReadLine()!;

                                    Console.Write("Кількість днів: ");
                                    if (!int.TryParse(
                                        Console.ReadLine(), out int durationDays))
                                    {
                                        Console.WriteLine("Помилка: введіть ціле число.");
                                        break;
                                    }

                                    Console.Write("Інструкція (Enter — пропустити): ");
                                    string instructions = Console.ReadLine()!;

                                    clinic.MedicalRecords.Add(new Prescription(
                                        patientId, doctorId, DateTime.Today,
                                        medicationName, dosage, durationDays,
                                        instructions));
                                }
                            }
                            catch (ArgumentOutOfRangeException e)
                            {
                                Console.WriteLine("Помилка: " + e.Message);
                            }
                            catch (ArgumentException e)
                            {
                                Console.WriteLine("Помилка: " + e.Message);
                            }

                            break;
                        }

                    case "6":
                        {
                            clinic.Doctors.DisplayAll();
                            Console.Write("ID лікаря: ");

                            if (!int.TryParse(Console.ReadLine(), out int doctorId))
                            {
                                Console.WriteLine("Помилка: введіть ціле число.");
                                break;
                            }

                            if (clinic.Doctors.FindById(doctorId) == null)
                            {
                                Console.WriteLine("Лікаря не знайдено.");
                                break;
                            }

                            clinic.MedicalRecords.DisplayList(
                                clinic.MedicalRecords.GetByDoctor(doctorId));

                            break;
                        }

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
                Console.WriteLine("4 — Медична картка");
                Console.WriteLine("5 — Звіт");
                Console.WriteLine("6 — Тест зростаючого масиву");
                Console.WriteLine("7 — Розклад на дату");
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
                        MedicalRecordsMenu(clinic);
                        break;


                    case "7":
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

        WorkSchedule morning = new WorkSchedule(8, 16);
        WorkSchedule copy = morning;

        Console.WriteLine($"Початковий: {morning}");
        Console.WriteLine($"Копія: {copy}");

        copy = new WorkSchedule(14, 22);

        Console.WriteLine("Після зміни копії:");
        Console.WriteLine($"Початковий: {morning}");
        Console.WriteLine($"Копія: {copy}");

        Console.WriteLine($"Працює о 10:00: {morning.Contains(10)}");
        Console.WriteLine($"Працює о 16:00: {morning.Contains(16)}");
        Console.WriteLine($"Працює зараз: {morning.IsNow}");

        Console.WriteLine(
    ClinicFormatter.FormatBloodType(BloodType.APositive));

        int[] ages = { 1, 3, 11, 16, 21, 33, 41, 111 };

        foreach (int age in ages)
        {
            Console.WriteLine(ClinicFormatter.FormatAge(age));
        }

        Console.WriteLine(ClinicFormatter.FormatPhone("0501234567"));
        Console.WriteLine(ClinicFormatter.FormatPhone("050-123-4567"));

        Console.WriteLine("\n=== Перевірка індексаторів ===");

        Patient? first = clinic.Patients[0];
        Doctor? second = clinic.Doctors[1];

        Console.WriteLine($"Перший пацієнт: {first}");
        Console.WriteLine($"Другий лікар: {second}");

        Console.WriteLine(
            $"Індекс -1 повертає null: {clinic.Patients[-1] is null}");

        Console.WriteLine(
            $"Індекс 5 повертає null: {clinic.Patients[5] is null}");

        Console.WriteLine(
            $"Немає запису з індексом 0: {clinic.Appointments[0] is null}");

        Console.WriteLine("\n=== Задача 4: перевантаження та out ===");

        Doctor[] cardiologists =
            clinic.Doctors.FindBySpeciality(Speciality.Cardiology);

        Console.WriteLine($"Кардіологів за enum: {cardiologists.Length}");

        Doctor[] foundByText =
            clinic.Doctors.FindBySpeciality("кардіо");

        Console.WriteLine($"За рядком «кардіо»: {foundByText.Length}");

        DateTime demoDate = DateTime.Today;

        Appointment[] byDate =
            clinic.Appointments.GetByDate(demoDate);

        Appointment[] byNumbers =
            clinic.Appointments.GetByDate(
                demoDate.Year, demoDate.Month, demoDate.Day);

        Console.WriteLine(
            $"Записів на сьогодні: DateTime — {byDate.Length}, " +
            $"три числа — {byNumbers.Length}");

        if (clinic.Patients.TryFindById(patients[0].Id, out Patient? foundPatient))
        {
            Console.WriteLine($"Знайдено пацієнта: {foundPatient.FullName}");
        }
        else
        {
            Console.WriteLine("Пацієнта не знайдено.");
        }

        if (clinic.Doctors.TryFindById(doctors[0].Id, out Doctor? foundDoctor))
        {
            Console.WriteLine($"Знайдено лікаря: {foundDoctor.FullName}");
        }
        else
        {
            Console.WriteLine("Лікаря не знайдено.");
        }

        if (!clinic.Patients.TryFindById(-1, out Patient? missingPatient))
        {
            Console.WriteLine("Пацієнта з ID -1 не знайдено.");
        }

        if (!clinic.Doctors.TryFindById(-1, out Doctor? missingDoctor))
        {
            Console.WriteLine("Лікаря з ID -1 не знайдено.");
        }

        Patient[] aPositivePatients =
            clinic.Patients.FindByBloodType(BloodType.APositive);

        Console.WriteLine("\nПацієнти з групою крові A+:");

        foreach (Patient item in aPositivePatients)
        {
            Console.WriteLine(item);
        }

        string existingName =
            clinic.Patients.FindById(patients[0].Id)?.FullName
            ?? "не знайдено";

        string missingName =
            clinic.Patients.FindById(-1)?.FullName
            ?? "не знайдено";

        Console.WriteLine($"Наявний ID: {existingName}");
        Console.WriteLine($"Відсутній ID: {missingName}");
        // Медичні записи першого пацієнта.
        clinic.MedicalRecords.Add(new Diagnosis(
            patients[0].Id, doctors[0].Id, DateTime.Today.AddDays(-30),
            "I10", "Гіпертонічна хвороба", isChronic: true));

        clinic.MedicalRecords.Add(new Diagnosis(
            patients[0].Id, doctors[1].Id, DateTime.Today.AddDays(-5),
            "J06.9", "Гострий ринофарингіт"));

        clinic.MedicalRecords.Add(new LabResult(
            patients[0].Id, doctors[0].Id, DateTime.Today.AddDays(-7),
            "Гемоглобін", 145, "г/л", "120–160", isNormal: true));

        clinic.MedicalRecords.Add(new LabResult(
            patients[0].Id, doctors[0].Id, DateTime.Today.AddDays(-7),
            "Холестерин", 6.2, "ммоль/л", "< 5.2", isNormal: false));

        clinic.MedicalRecords.Add(new Prescription(
            patients[0].Id, doctors[0].Id, DateTime.Today.AddDays(-5),
            "Лізиноприл", "10 мг", 30, "1 раз на добу вранці"));

        // Медичні записи другого пацієнта.
        clinic.MedicalRecords.Add(new Prescription(
            patients[1].Id, doctors[1].Id, DateTime.Today.AddDays(-40),
            "Тестовий препарат", "1 таблетка", 10));

        clinic.MedicalRecords.Add(new Diagnosis(
            patients[1].Id, doctors[1].Id, DateTime.Today.AddDays(-40),
            "J06.9", "Ринофарингіт"));

        // Медичний запис третього пацієнта.
        clinic.MedicalRecords.Add(new LabResult(
            patients[2].Id, doctors[2].Id, DateTime.Today.AddDays(-2),
            "Лейкоцити", 6.0, "10^9/л", "4.0–9.0", isNormal: true));

        // Демонстрація поліморфізму.
        Console.WriteLine("\n=== Записи першого пацієнта ===");
        MedicalRecord[] patientRecords =
            clinic.MedicalRecords.GetByPatient(patients[0].Id);

        foreach (MedicalRecord medicalRecord in patientRecords)
        {
            Console.WriteLine(
                $"{medicalRecord.GetRecordType()}: {medicalRecord.GetSummary()}");
        }

        MainMenu(clinic);
    }
}