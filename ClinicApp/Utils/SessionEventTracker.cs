using System.Text;
using ClinicApp.Events;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class SessionEventTracker
{
    private readonly Clinic _clinic;

    public int PatientsAdded { get; private set; }
    public int AppointmentsBooked { get; private set; }
    public int UrgentBooked { get; private set; }
    public int AppointmentsCancelled { get; private set; }
    public int AppointmentsCompleted { get; private set; }
    public int PaymentsReceived { get; private set; }
    public int PlansCompleted { get; private set; }

    public SessionEventTracker(Clinic clinic)
    {
        _clinic = clinic;
    }

    // ── Обробники подій ──

    public void OnPatientAdded(object? sender, PatientEventArgs e) => PatientsAdded++;

    public void OnAppointmentBooked(object? sender, AppointmentEventArgs e) => AppointmentsBooked++;

    public void OnUrgentAppointmentBooked(object? sender, AppointmentEventArgs e) => UrgentBooked++;

    public void OnAppointmentCompleted(object? sender, AppointmentEventArgs e) => AppointmentsCompleted++;

    public void OnPaymentReceived(object? sender, PaymentEventArgs e) => PaymentsReceived++;

    public void OnPlanCompleted(object? sender, TreatmentPlanEventArgs e) => PlansCompleted++;

    public void OnAppointmentCancelled(object? sender, AppointmentEventArgs e)
    {
        AppointmentsCancelled++;

        if (!_clinic.WaitingRoom.IsEmpty)
        {
            Patient next = _clinic.WaitingRoom.Peek();
            Console.WriteLine($"  [ЧЕРГА] Слот звільнився. Наступний: {next.FullName}");
        }
    }

    // ── Підсумок сесії ──

    public void PrintSummary()
    {
        WriteSummary(Console.Out);
    }

    public void SaveSummary(string path = "session_summary.txt")
    {
        using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            writer.WriteLine($"Сформовано: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
            WriteSummary(writer);
        }
    }

    private void WriteSummary(TextWriter writer)
    {
        writer.WriteLine("=== Підсумок сесії ===");
        writer.WriteLine($"Пацієнтів додано:    {PatientsAdded}");
        writer.WriteLine($"Записів створено:    {AppointmentsBooked} (термінових: {UrgentBooked})");
        writer.WriteLine($"Скасовано:           {AppointmentsCancelled}");
        writer.WriteLine($"Завершено:           {AppointmentsCompleted}");
        writer.WriteLine($"Оплат:               {PaymentsReceived}");
        writer.WriteLine($"Планів завершено:    {PlansCompleted}");
    }
}