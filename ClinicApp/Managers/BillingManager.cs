using ClinicApp.Interfaces;
using ClinicApp.Models;
using System;

namespace ClinicApp.Managers;

public class BillingManager
{
    private readonly AppointmentManager _appointments;

    public BillingManager(AppointmentManager appointments)
    {
        _appointments = appointments;
    }

    private IPayable[] FilterUnpaid(Appointment[] appointments)
    {
        int count = 0;

        foreach (Appointment appointment in appointments)
        {
            if (!appointment.IsPaid && !appointment.IsCancelled)
            {
                count++;
            }
        }

        IPayable[] result = new IPayable[count];
        int index = 0;

        foreach (Appointment appointment in appointments)
        {
            if (!appointment.IsPaid && !appointment.IsCancelled)
            {
                result[index] = appointment;
                index++;
            }
        }

        return result;
    }

    public IPayable[] GetAllUnpaid()
    {
        return FilterUnpaid(_appointments.GetAll());
    }

    public IPayable[] GetUnpaidByPatient(int patientId)
    {
        return FilterUnpaid(_appointments.GetByPatient(patientId));
    }

    public decimal GetTotalDebt()
    {
        decimal total = 0m;

        foreach (IPayable item in GetAllUnpaid())
        {
            total += item.GetCost();
        }

        return total;
    }

    public decimal GetPatientDebt(int patientId)
    {
        decimal total = 0m;

        foreach (IPayable item in GetUnpaidByPatient(patientId))
        {
            total += item.GetCost();
        }

        return total;
    }

    public bool PayAppointment(int appointmentId)
    {
        foreach (Appointment appointment in _appointments.GetAll())
        {
            if (appointment.Id == appointmentId)
            {
                if (appointment.IsPaid || appointment.IsCancelled)
                {
                    return false;
                }

                appointment.MarkPaid();
                return true;
            }
        }

        return false;
    }

    public void DisplayUnpaid(IPayable[] items)
    {
        if (items.Length == 0)
        {
            Console.WriteLine("Немає неоплачених записів.");
            return;
        }

        for (int i = 0; i < items.Length; i++)
        {
            IPayable item = items[i];
            string cost = item.GetCost().ToString("F2");

            if (item is Appointment appointment)
            {
                Console.WriteLine($"{appointment} | Сума: {cost} грн");
            }
            else
            {
                Console.WriteLine($"[{i + 1}] Сума: {cost} грн");
            }
        }
    }
}