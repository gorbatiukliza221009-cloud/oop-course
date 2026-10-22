using ClinicApp.Enums;
using ClinicApp.Interfaces;
using ClinicApp.Utils;
using System;

namespace ClinicApp.Models
{
    public class Appointment : IPayable, ICancellable
    {
        private static int _nextId = 1;
        private int _durationMinutes;
        private bool _isPaid = false;
        private decimal _discountMultiplier = 1m;
        public int Id { get; }
        public int PatientId { get; }
        public int DoctorId { get; }

        public DateTime ScheduledAt { get; set; }
        public int DurationMinutes
        {
            get => _durationMinutes;
            set
            {
                ClinicValidator.ValidatePositive(value, nameof(DurationMinutes));
                _durationMinutes = value;
            }
        }
        public AppointmentStatus Status { get; private set; }
        public string Notes { get; private set; }
        public bool IsPaid
        {
            get { return _isPaid; }
        }
        public bool IsCancelled => Status == AppointmentStatus.Cancelled;

        public string CancellationReason => IsCancelled ? Notes : "";
        public DateTime EndTime
        {
            get { return ScheduledAt.AddMinutes(DurationMinutes); }
        }

        public bool IsUpcoming
        {
            get { return ScheduledAt > DateTime.Now && Status == AppointmentStatus.Scheduled; }
        }

        public Appointment(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes)
        {
            PatientId = patientId;
            DoctorId = doctorId;
            ScheduledAt = scheduledAt;
            DurationMinutes = durationMinutes;
            Status = AppointmentStatus.Scheduled;
            Notes = "";
            Id = _nextId++;
        }
        // Варіант А: множник знижки зберігається в Appointment.
        // Усі підкласи враховують його через base.GetCost(),
        // тому окремі VIP-підкласи не потрібні.
        public decimal DiscountMultiplier
        {
            get => _discountMultiplier;
            set
            {
                if (value < 0m || value > 1m)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(DiscountMultiplier),
                        "Множник знижки має бути від 0 до 1.");
                }

                _discountMultiplier = value;
            }
        }
        public virtual decimal GetCost()
        {
            return DurationMinutes * 10m * DiscountMultiplier;
        }

        public virtual string GetDescription()
        {
            return "Прийом";
        }

        public int GetPriority()
        {
            return 3;
        }

        public void MarkPaid()
        {
            if (IsCancelled)
            {
                return;
            }

            _isPaid = true;
        }

        public bool Cancel(string reason = "")
        {
            if (Status == AppointmentStatus.Scheduled)
            {
                Status = AppointmentStatus.Cancelled;
                Notes = reason;
                return true;
            }
            return false;
        }

        public bool Complete()
        {
            if (Status == AppointmentStatus.Scheduled)
            {
                Status = AppointmentStatus.Completed;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            string text = $"[{Id}] {GetDescription()} | " +
                          $"Пацієнт #{PatientId} → Лікар #{DoctorId} | " +
                          $"{ScheduledAt:dd.MM.yyyy HH:mm}–{EndTime:HH:mm} | {Status}";

            if (Notes.Length > 0)
            {
                text += $" | {Notes}";
            }

            text += $" | {GetCost():F2} грн";

            return text;
        }
    }
}
