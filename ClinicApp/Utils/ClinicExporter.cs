using System.Text;
using ClinicApp.Interfaces;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class ClinicExporter
{
    private const string Separator = "----------------------------------------";

    private readonly Clinic _clinic;
    private readonly string _baseDir;

    public ClinicExporter(Clinic clinic, string baseDir = "reports")
    {
        _clinic = clinic;
        _baseDir = baseDir;
    }

    public string ExportPatients()
    {
        string path = Path.Combine(GetReportDir(), "patients.txt");

        using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            WriteHeader(writer, "Пацієнти");

            int count = 0;
            while (true)
            {
                // індексатор PatientManager повертає null за межами списку
                Patient? patient = _clinic.Patients[count];
                if (patient == null)
                    break;

                writer.WriteLine($"{count + 1,3}. {patient}");
                count++;
            }

            WriteFooter(writer, count);
        }

        return path;
    }

    public string ExportAppointments()
    {
        string path = Path.Combine(GetReportDir(), "appointments.txt");
        Appointment[] appointments = _clinic.Appointments.GetAll();

        using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            WriteHeader(writer, "Записи на прийом");

            for (int i = 0; i < appointments.Length; i++)
            {
                writer.WriteLine($"{i + 1,3}. {appointments[i]}");
            }

            WriteFooter(writer, appointments.Length);
        }

        return path;
    }

    public string ExportBilling()
    {
        string path = Path.Combine(GetReportDir(), "billing.txt");
        IPayable[] unpaid = _clinic.Billing.GetAllUnpaid();

        using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            WriteHeader(writer, "Неоплачені записи");

            for (int i = 0; i < unpaid.Length; i++)
            {
                writer.WriteLine($"{i + 1,3}. {unpaid[i]}");
            }

            WriteFooter(writer, unpaid.Length);
            writer.WriteLine($"Загальний борг: {_clinic.Billing.GetTotalDebt():F2} грн");
        }

        return path;
    }

    public string ExportTreatmentPlans()
    {
        string path = Path.Combine(GetReportDir(), "treatment_plans.txt");
        TreatmentPlan[] plans = _clinic.TreatmentPlans.GetAll();

        using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            WriteHeader(writer, "Плани лікування");

            for (int i = 0; i < plans.Length; i++)
            {
                writer.WriteLine($"{i + 1,3}. {plans[i]}");
            }

            WriteFooter(writer, plans.Length);
        }

        return path;
    }

    public string[] ExportAll()
    {
        string[] paths = new string[4];

        paths[0] = ExportPatients();
        paths[1] = ExportAppointments();
        paths[2] = ExportBilling();
        paths[3] = ExportTreatmentPlans();

        return paths;
    }

    // reports/yyyy-MM-dd/ — створюється, якщо ще не існує
    private string GetReportDir()
    {
        string dir = Path.Combine(_baseDir, DateTime.Now.ToString("yyyy-MM-dd"));
        Directory.CreateDirectory(dir);
        return dir;
    }

    private static void WriteHeader(StreamWriter writer, string title)
    {
        writer.WriteLine($"=== {title} — звіт ===");
        writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
        writer.WriteLine(Separator);
    }

    private static void WriteFooter(StreamWriter writer, int count)
    {
        writer.WriteLine(Separator);
        writer.WriteLine($"Всього: {count}");
    }
}
