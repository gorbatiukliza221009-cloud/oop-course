using ClinicApp.Enums;
using ClinicApp.Models;
using ClinicApp.Events;
using ClinicApp.Utils;

namespace ClinicApp.Managers;

public class TreatmentPlanManager
{
    private readonly List<TreatmentPlan> _plans = new List<TreatmentPlan>();
    public event EventHandler<TreatmentPlanEventArgs>? PlanCompleted;
    public bool Add(TreatmentPlan plan)
    {
        ValidationResult result = ModelValidator.Validate(plan);

        if (!result.IsValid)
        {
            Console.WriteLine($"План #{plan.Id} не додано:");
            result.Print();
            return false;
        }

        _plans.Add(plan);
        return true;
    }

    public TreatmentPlan? GetById(int id)
    {
        foreach (TreatmentPlan plan in _plans)
        {
            if (plan.Id == id)
                return plan;
        }
        return null;
    }

    public TreatmentPlan[] GetByPatient(int patientId)
    {
        List<TreatmentPlan> found = new List<TreatmentPlan>();
        foreach (TreatmentPlan plan in _plans)
        {
            if (plan.PatientId == patientId)
                found.Add(plan);
        }
        return found.ToArray();
    }

    public TreatmentPlan[] GetByStatus(TreatmentStatus status)
    {
        List<TreatmentPlan> found = new List<TreatmentPlan>();
        foreach (TreatmentPlan plan in _plans)
        {
            if (plan.Status == status)
                found.Add(plan);
        }
        return found.ToArray();
    }

    public TreatmentPlan[] GetAll()
    {
        return _plans.ToArray();
    }
    public bool Complete(int planId)
    {
        TreatmentPlan? plan = GetById(planId);

        if (plan == null)
            return false;

        if (!plan.Complete())
            return false;

        PlanCompleted?.Invoke(this,
            new TreatmentPlanEventArgs(plan.Id, plan.PatientId, plan.Diagnosis));
        return true;
    }
}