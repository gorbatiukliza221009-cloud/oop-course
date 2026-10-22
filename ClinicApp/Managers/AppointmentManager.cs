using ClinicApp.Models;
using ClinicApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Managers
{
    public class AppointmentManager
    {
        private const int MaxAppointments = 500;
        private Appointment[] _appointments = new Appointment[MaxAppointments];
        private int _count = 0;
        private PatientManager _patients;
        private DoctorManager _doctors;

        public int Count
        {
            get { return _count; }
        }

        public AppointmentManager(PatientManager patients, DoctorManager doctors)
        {
            _patients = patients;
            _doctors = doctors;
        }
        public Appointment[] GetAll()
        {
            Appointment[] result = new Appointment[_count];

            for (int i = 0; i < _count; i++)
            {
                result[i] = _appointments[i];
            }

            return result;
        }
        public static int CancelAll(ICancellable[] items, string reason = "")
        {
            int cancelledCount = 0;

            foreach (ICancellable item in items)
            {
                if (item.Cancel(reason))
                {
                    cancelledCount++;
                }
            }

            return cancelledCount;
        }
        public bool Book(
    int patientId,
    int doctorId,
    DateTime scheduledAt,
    int durationMinutes = 30)
        {
            return AddAppointment(new RegularAppointment(
                patientId, doctorId, scheduledAt, durationMinutes));
        }

        public bool BookUrgent(
            int patientId,
            int doctorId,
            DateTime scheduledAt,
            string urgencyNote = "",
            int durationMinutes = 30)
        {
            return AddAppointment(new UrgentAppointment(
                patientId, doctorId, scheduledAt,
                urgencyNote, durationMinutes));
        }

        public bool BookSpecialist(
            int patientId,
            int doctorId,
            DateTime scheduledAt,
            string topic = "",
            int durationMinutes = 45)
        {
            return AddAppointment(new SpecialistAppointment(
                patientId, doctorId, scheduledAt,
                topic, durationMinutes));
        }

        private bool AddAppointment(Appointment appointment)
        {
            if (_count >= MaxAppointments)
            {
                Console.WriteLine("Досягнуто ліміту записів на прийом.");
                return false;
            }

            Patient? patient = _patients.FindById(appointment.PatientId);
            Doctor? doctor = _doctors.FindById(appointment.DoctorId);

            if (patient == null)
            {
                Console.WriteLine(
                    $"Пацієнт з ID {appointment.PatientId} не знайдений.");
                return false;
            }

            if (doctor == null)
            {
                Console.WriteLine(
                    $"Лікар з ID {appointment.DoctorId} не знайдений.");
                return false;
            }

            _appointments[_count] = appointment;
            _count++;

            Console.WriteLine(
                $"Запис на прийом [{appointment.Id}] " +
                $"для пацієнта [{patient.FullName}] " +
                $"до лікаря [{doctor.FullName}] " +
                $"заплановано на {appointment.ScheduledAt:dd.MM.yyyy HH:mm}.");

            return true;
        }
        public Appointment[] GetByDate(int year, int month, int day)
        {
            return GetByDate(new DateTime(year, month, day));
        }

        private Appointment? FindById(int id)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].Id == id)
                {
                    return _appointments[i];
                }
            }
            return null;
        }

        public bool Cancel(int id, string reason = "")
        {
            Appointment? appointment = FindById(id);

            if (appointment == null)
                return false;

            return appointment.Cancel(reason);
        }

        public bool Complete(int id)
        {
            Appointment? appointment = FindById(id);
            if (appointment == null)
                return false;
            return appointment.Complete();
        }

        public Appointment[] GetByPatient(int patientId)
        {
            int matches = 0;

            for(int i = 0; i < _count; i++)
            {
                if (_appointments[i].PatientId == patientId)
                    matches++;
            }
            Appointment[] result = new Appointment[matches];
            int index = 0;
            for(int i = 0; i < _count; i++)
            {
                if (_appointments[i].PatientId == patientId)
                {
                    result[index] = _appointments[i];
                    index++;
                }
            }
            return result;  

        }

        public Appointment[] GetByDoctor(int doctorId)
        {
            int matches = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].DoctorId == doctorId)
                    matches++;
            }
            Appointment[] result = new Appointment[matches];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].DoctorId == doctorId)
                {
                    result[index] = _appointments[i];
                    index++;
                }
            }
            return result;
        }

        public Appointment[] GetByDate(DateTime date)
        {
            int matches = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].ScheduledAt.Date == date.Date)
                    matches++;
            }
            Appointment[] result = new Appointment[matches];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].ScheduledAt.Date == date.Date)
                {
                    result[index] = _appointments[i];
                    index++;
                }
            }
            return result;
        }

        public Appointment[] GetUpcoming()
        {
            int matches = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].IsUpcoming)
                    matches++;
            }
            Appointment[] result = new Appointment[matches];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].IsUpcoming)
                {
                    result[index] = _appointments[i];
                    index++;
                }
            }
            return result;
        }
        public Appointment[] GetUrgent()
        {
            int matches = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i] is UrgentAppointment)
                {
                    matches++;
                }
            }

            Appointment[] result = new Appointment[matches];
            int index = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i] is UrgentAppointment)
                {
                    result[index] = _appointments[i];
                    index++;
                }
            }

            return result;
        }

        public Appointment[] GetSpecialist()
        {
            int matches = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i] is SpecialistAppointment)
                {
                    matches++;
                }
            }

            Appointment[] result = new Appointment[matches];
            int index = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i] is SpecialistAppointment)
                {
                    result[index] = _appointments[i];
                    index++;
                }
            }

            return result;
        }

        public Appointment[] GetRegular()
        {
            int matches = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i] is RegularAppointment)
                {
                    matches++;
                }
            }

            Appointment[] result = new Appointment[matches];
            int index = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i] is RegularAppointment)
                {
                    result[index] = _appointments[i];
                    index++;
                }
            }

            return result;
        }
        public void DisplayAppointment(Appointment appointment)
        {
            Patient? patient = _patients.FindById(appointment.PatientId);
            Doctor? doctor = _doctors.FindById(appointment.DoctorId);

            string patientName;
            if(patient == null)
            {
                patientName = $"Пацієнт {appointment.PatientId} ";
            }
            else
            {
                patientName = patient.FullName;
            }

            string doctorName;
            if (doctor == null)
            {
                doctorName = $"Лікар {appointment.DoctorId} ";
            }
            else
            {
                doctorName = doctor.FullName;
            }

            string text =
    $"[{appointment.Id}] {appointment.GetDescription()} | " +
    $"{patientName} → {doctorName} | " +
    $"{appointment.ScheduledAt:dd.MM.yyyy HH:mm}–" +
    $"{appointment.EndTime:HH:mm} | {appointment.Status}";

            if (appointment.Notes.Length > 0)
            {
                text += $" | {appointment.Notes}";
            }

            text += $" | {appointment.GetCost():F2} грн";

            Console.WriteLine(text);
        }

        public void DisplayList(Appointment[] appointments)
        {
            if (appointments.Length == 0)
            {
                Console.WriteLine("Немає записів на прийом.");
                return;
            }
            foreach (Appointment appointment in appointments)
            {
                DisplayAppointment(appointment);
            }
        }

        public Appointment? this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                {
                    return null;
                }

                return _appointments[index];
            }
        }
    }
}
