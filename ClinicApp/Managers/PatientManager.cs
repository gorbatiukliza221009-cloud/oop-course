using ClinicApp.Enums;
using ClinicApp.Models;
using System.Collections.Generic;
using System;

namespace ClinicApp.Managers
{
    public class PatientManager
    {
        private List<Patient> _patients = new List<Patient>();

        public int Count
        {
            get { return _patients.Count; }
        }

        public void Add(Patient patient)
        {
            _patients.Add(patient);

            Console.WriteLine($"Пацієнта [{patient.Id}] {patient.FullName} додано.");
        }

        public Patient? FindById(int id)
        {
            for (int i = 0; i < _patients.Count; i++)
            {
                if (_patients[i].Id == id)
                {
                    return _patients[i];
                }
            }
            return null;
        }
        public bool TryFindById(
    int id,
    [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out Patient patient)
        {
            Patient? found = FindById(id);

            if (found == null)
            {
                patient = null!;
                return false;
            }

            patient = found;
            return true;
        }

        public Patient[] FindByBloodType(BloodType bloodType)
        {
            List<Patient> result = new List<Patient>();

            for (int i = 0; i < _patients.Count; i++)
            {
                if (_patients[i].BloodType == bloodType)
                {
                    result.Add(_patients[i]);
                }
            }

            return result.ToArray();
        }

        public Patient[] FindByName(string name)
        {
            string query = name.ToLower();
            List<Patient> result = new List<Patient>();

            for (int i = 0; i < _patients.Count; i++)
            {
                if (_patients[i].FirstName.ToLower().Contains(query) ||
                    _patients[i].LastName.ToLower().Contains(query))
                {
                    result.Add(_patients[i]);
                }
            }

            return result.ToArray();
        }



        public bool Remove(int id)
        {
            for (int i = 0; i < _patients.Count; i++)
            {
                if (_patients[i].Id == id)
                {
                    _patients.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public Patient[] GetAll()
        {
            return _patients.ToArray();
        }

        public void DisplayAll()
        {
            Console.WriteLine($"\n=== Пацієнти ({_patients.Count}) ===");
            if (_patients.Count == 0)
            {
                Console.WriteLine("Список пацієнтів порожній.");

            }
            else
            {
                for (int i = 0; i < _patients.Count; i++)
                {
                    Console.WriteLine(_patients[i]);
                }
            }
        }

        public void DisplayStats()
        {
            Console.WriteLine($"\n=== Статистика пацієнтів ===");

            if(_patients.Count==0)
            {
                Console.WriteLine("Список пацієнтів порожній.");
                return;
            }

            int ageSum = 0;
            int youngestIndex = 0;
            int oldestIndex = 0;    
            int adultCount = 0;

            for(int i = 0; i < _patients.Count; i++)
            {
                ageSum += _patients[i].Age;
                if (_patients[i].Age < _patients[youngestIndex].Age)
                {
                    youngestIndex = i;
                }
                if (_patients[i].Age > _patients[oldestIndex].Age)
                {
                    oldestIndex = i;
                }
                if (_patients[i].IsAdult)
                {
                    adultCount++;
                }
            }

            double averageAge = (double)ageSum / _patients.Count;

            Console.WriteLine($"Всього: {_patients.Count}");
            Console.WriteLine($"Середній вік: {averageAge:F1} р.");
            Console.WriteLine($"Наймолодший: {_patients[youngestIndex].FullName} " +
        $"({_patients[youngestIndex].Age} р.)");
            Console.WriteLine($"Найстарший: {_patients[oldestIndex].FullName} " +
        $"({_patients[oldestIndex].Age} р.)");
            Console.WriteLine($"Дорослих: {adultCount} з {_patients.Count}");

        }
        public Patient this[int index]
        {
            get
            {
                if (index < 0 || index >= _patients.Count)
                {
                    return null!;
                }

                return _patients[index];
            }
        }
    }
}
