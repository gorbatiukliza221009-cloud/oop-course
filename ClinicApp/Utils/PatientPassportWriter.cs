using System.Text;
using ClinicApp.Events;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class PatientPassportWriter
{
    private readonly Clinic _clinic;
    private readonly string _folder;

    public PatientPassportWriter(Clinic clinic, string folder = "patients")
    {
        _clinic = clinic;
        _folder = folder;
        Directory.CreateDirectory(_folder);
    }

    // ── Обробники подій: лише передають PatientId ──

    public void OnPatientAdded(object? sender, PatientEventArgs e) =>
        WritePassport(e.PatientId);

    public void OnAppointmentCompleted(object? sender, AppointmentEventArgs e) =>
        WritePassport(e.PatientId);

    public void OnPlanCompleted(object? sender, TreatmentPlanEventArgs e) =>
        WritePassport(e.PatientId);

    // ── Спільна логіка генерації паспорта ──

    private void WritePassport(int patientId)
    {
        Patient? patient = _clinic.Patients.FindById(patientId);

        if (patient == null)
            return;

        string path = Path.Combine(_folder, $"passport_{patientId}.txt");

        using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            // Заголовок і дата генерації
            writer.WriteLine($"=== Паспорт пацієнта #{patient.Id} ===");
            writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
            writer.WriteLine();

            // Особисті дані
            writer.WriteLine($"Ім'я: {patient.FullName}");
            writer.WriteLine(
                $"Дата народження: {patient.DateOfBirth:dd.MM.yyyy} " +
                $"({ClinicFormatter.FormatAge(patient.Age)})");
            writer.WriteLine(
                $"Група крові: {ClinicFormatter.FormatBloodType(patient.BloodType)}   " +
                $"Телефон: {ClinicFormatter.FormatPhone(patient.Phone)}");
            writer.WriteLine();

            // Медичні записи — три окремі проходи
            MedicalRecord[] records = _clinic.MedicalRecords.GetByPatient(patientId);

            writer.WriteLine("-- Діагнози --");
            int count = 0;
            foreach (MedicalRecord record in records)
            {
                if (record is Diagnosis diagnosis)
                {
                    writer.WriteLine(diagnosis.GetSummary());
                    count++;
                }
            }
            if (count == 0)
                writer.WriteLine("(немає)");

            writer.WriteLine("-- Аналізи --");
            count = 0;
            foreach (MedicalRecord record in records)
            {
                if (record is LabResult labResult)
                {
                    writer.WriteLine(labResult.GetSummary());
                    count++;
                }
            }
            if (count == 0)
                writer.WriteLine("(немає)");

            writer.WriteLine("-- Рецепти --");
            count = 0;
            foreach (MedicalRecord record in records)
            {
                if (record is Prescription prescription)
                {
                    writer.WriteLine(prescription.GetSummary());
                    count++;
                }
            }
            if (count == 0)
                writer.WriteLine("(немає)");
            writer.WriteLine();

            // Записи на прийом
            writer.WriteLine("-- Записи --");
            Appointment[] appointments = _clinic.Appointments.GetByPatient(patientId);

            if (appointments.Length == 0)
                writer.WriteLine("(немає)");

            foreach (Appointment appointment in appointments)
            {
                writer.WriteLine(
                    $"[{appointment.Id}] {appointment.GetDescription()} | " +
                    $"{appointment.ScheduledAt:dd.MM.yyyy HH:mm} | " +
                    $"{appointment.Status} | {appointment.GetCost():F2} грн");
            }
            writer.WriteLine();

            // Плани лікування
            writer.WriteLine("-- Плани лікування --");
            TreatmentPlan[] plans = _clinic.TreatmentPlans.GetByPatient(patientId);

            if (plans.Length == 0)
                writer.WriteLine("(немає)");

            foreach (TreatmentPlan plan in plans)
            {
                writer.WriteLine(
                    $"[{plan.Id}] {plan.Diagnosis} | {plan.DurationDays} днів | {plan.Status}");
            }
            writer.WriteLine();

            // Заборгованість
            writer.WriteLine(
                $"Заборгованість: {_clinic.Billing.GetPatientDebt(patientId):F2} грн");
        }
    }
}