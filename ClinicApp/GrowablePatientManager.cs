using System;

namespace ClinicApp
{
    public class GrowablePatientManager
    {
        private Patient[] _patients = new Patient[4];
        private int _count = 0;

        public int Count
        {
            get { return _count; }
        }

        public int Capacity
        {
            get { return _patients.Length; }
        }

        private void Grow()
        {
            int oldCapacity = _patients.Length;
            int newCapacity = _patients.Length * 2;
            Patient[] newPatients = new Patient[newCapacity];
            for (int i = 0; i < oldCapacity; i++)
            {
                newPatients[i] = _patients[i];
            }

            _patients = newPatients;
            Console.WriteLine($"Масив заповнений! Розширення: {oldCapacity} → {newCapacity}");

        }

        public void Add(Patient patient)
        {
            if (_count >= _patients.Length)
            {
                Grow();
            }
            _patients[_count] = patient;
            _count++;

            Console.WriteLine(
            $"  Додано [{patient.Id}]. Розмір: {Count} / {Capacity}");
        }

        public Patient? FindById(int id)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_patients[i].Id == id)
                {
                    return _patients[i];
                }
            }
            return null;

        }

        public bool Remove(int id)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_patients[i].Id == id)
                {
                    for (int j = i; j < _count - 1; j++)
                    {
                        _patients[j] = _patients[j + 1];
                    }
                    Array.Clear(_patients, _count, 1);
                    _count--;
                    return true;
                }
            }
            return false;
        }

        public void DisplayAll()
        {
            Console.WriteLine($"\n=== Пацієнти ({Count} / {Capacity}) ===");

            if(_count == 0)
            {
                Console.WriteLine("  Список пацієнтів порожній.");
                return;
            }   

            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine($"  [{_patients[i].Id}] {_patients[i].FullName}");
            }
        }
    }
}
