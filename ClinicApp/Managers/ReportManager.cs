using System.Linq;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class ReportManager
{
    private readonly AppointmentManager _appointments;
    private readonly DoctorManager _doctors;
    private readonly PatientManager _patients;
    private readonly AnalyticsManager _analytics;

    public ReportManager(AppointmentManager appointments,
                         DoctorManager doctors,
                         PatientManager patients)
    {
        _appointments = appointments;
        _doctors = doctors;
        _patients = patients;
        _analytics = new AnalyticsManager(appointments, doctors, patients);
    }

    // 1. Статистика по спеціальностях
    public IEnumerable<SpecialityReport> GetSpecialityStats()
    {
        Appointment[] appointments = _appointments.GetAll();

        return _doctors.GetAll()
            .GroupBy(d => d.Speciality)
            .Select(g =>
            {
                int[] doctorIds = g.Select(d => d.Id).ToArray();

                Appointment[] groupAppointments = appointments
                    .Where(a => doctorIds.Contains(a.DoctorId))
                    .ToArray();

                return new SpecialityReport(
                    g.Key,
                    g.Count(),
                    groupAppointments.Length,
                    groupAppointments.Sum(a => a.GetCost()));
            })
            .OrderByDescending(r => r.TotalRevenue);
    }

    // 2. Найзавантаженіший лікар
    public string? FindBusiestDoctorName()
    {
        return _analytics.ComputeDoctorStats()
            .OrderByDescending(s => s.AppointmentCount)
            .FirstOrDefault()
            ?.FullName;
    }

    // 3. Пацієнти з кількома візитами
    public IEnumerable<string> GetPatientsWithMultipleVisits(int minVisits)
    {
        return _appointments.GetAll()
            .GroupBy(a => a.PatientId)
            .Where(g => g.Count() >= minVisits)
            .Join(_patients.GetAll(),
                  g => g.Key,
                  p => p.Id,
                  (g, p) => p.FullName);
    }

    // 4. Топ-N лікарів за виручкою
    public IEnumerable<DoctorStats> GetTopEarners(int n)
    {
        return _analytics.ComputeDoctorStats()
            .OrderByDescending(s => s.TotalRevenue)
            .Take(n);
    }

    // 5. Чи є термінові прийоми
    public bool HasAnyUrgentAppointments()
    {
        return _appointments.GetAll().Any(a => a is UrgentAppointment);
    }

    // 6. Унікальні спеціальності
    public IEnumerable<Speciality> GetActiveSpecialities()
    {
        return _doctors.GetAll()
            .Select(d => d.Speciality)
            .Distinct()
            .OrderBy(s => s);
    }

    // 7. Виручка по місяцях
    public IEnumerable<(int Year, int Month, decimal Total)> GetMonthlyRevenue()
    {
        return _appointments.GetAll()
            .GroupBy(a => new { a.ScheduledAt.Year, a.ScheduledAt.Month })
            .Select(g => (Year: g.Key.Year,
                          Month: g.Key.Month,
                          Total: g.Sum(a => a.GetCost())))
            .OrderBy(r => r.Year)
            .ThenBy(r => r.Month);
    }
}