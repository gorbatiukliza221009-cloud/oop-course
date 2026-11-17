using System.Globalization;
using System.Text;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class SessionManager
{
    private const string PatientsSection = "[PATIENTS]";
    private const string EndSection = "[END]";

    private readonly string _sessionPath;

    public SessionManager(string sessionPath = "session.dat")
    {
        _sessionPath = sessionPath;
    }

    public bool Exists()
    {
        return File.Exists(_sessionPath);
    }

    public void Save(Clinic clinic)
    {
        // false — перезаписати файл
        using (StreamWriter writer = new StreamWriter(_sessionPath, false, Encoding.UTF8))
        {
            writer.WriteLine(PatientsSection);

            int i = 0;
            while (true)
            {
                Patient? p = clinic.Patients[i];
                if (p == null)
                    break;

                writer.WriteLine(
                    $"{p.FirstName},{p.LastName},{p.DateOfBirth:dd.MM.yyyy},{p.BloodType},{p.Phone}");
                i++;
            }

            writer.WriteLine(EndSection);
        }
    }

    public int Load(Clinic clinic)
    {
        if (!Exists())
            return 0;

        string[] lines = File.ReadAllLines(_sessionPath, Encoding.UTF8);

        string currentSection = "";
        int added = 0;

        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();

            if (line == "")
                continue;

            // заголовок секції: [PATIENTS], [END], у майбутньому [APPOINTMENTS]
            if (line.StartsWith("["))
            {
                currentSection = line;
                continue;
            }

            if (currentSection == PatientsSection)
            {
                if (TryLoadPatient(clinic, line))
                    added++;
            }
            // рядки до першої секції або після [END] ігноруються
        }

        return added;
    }

    // true — пацієнта додано; false — рядок пошкоджений або пацієнт уже є
    private static bool TryLoadPatient(Clinic clinic, string line)
    {
        try
        {
            string[] parts = line.Split(',');
            if (parts.Length != 5)
                return false;

            string firstName = parts[0].Trim();
            string lastName = parts[1].Trim();
            DateTime dateOfBirth = DateTime.ParseExact(
                parts[2].Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
            BloodType bloodType = (BloodType)Enum.Parse(typeof(BloodType), parts[3].Trim());
            string phone = parts[4].Trim();

            if (AlreadyExists(clinic, firstName, lastName, dateOfBirth))
                return false;

            clinic.Patients.Add(new Patient(firstName, lastName, dateOfBirth, bloodType, phone));
            return true;
        }
        catch (FormatException)
        {
            return false;   // некоректна дата
        }
        catch (ArgumentException)
        {
            return false;   // невідома група крові або валідація Patient
        }
    }

    // той самий пацієнт = те саме ім'я, прізвище і дата народження
    private static bool AlreadyExists(Clinic clinic, string firstName, string lastName, DateTime dateOfBirth)
    {
        int i = 0;
        while (true)
        {
            Patient? p = clinic.Patients[i];
            if (p == null)
                return false;

            if (p.FirstName == firstName
                && p.LastName == lastName
                && p.DateOfBirth.Date == dateOfBirth.Date)
            {
                return true;
            }

            i++;
        }
    }
}