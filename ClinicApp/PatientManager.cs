using System;

namespace ClinicApp
{
    public class PatientManager
    {
        private const int MaxPatients = 100;
        private Patient[] _patients = new Patient[MaxPatients];
        private int _count = 0;

        public int Count
        {
            get { return _count; }
        }

        public void Add(Patient patient)
        {
            if (_count >= MaxPatients)
            {
                Console.WriteLine("Досягнуто ліміту пацієнтів.");
                return;
            }
            _patients[_count++] = patient;
            _count++;

            Console.WriteLine($"Пацієнта [{patient.Id}] {patient.FullName} додано.");
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

        public Patient[] FindByName(string name)
        {
            string query = name.ToLower();
            int matches = 0;

            for (int i = 0; i < _count; i++)

                if (_patients[i].FirstName.ToLower().Contains(query) || _patients[i].LastName.ToLower().Contains(query))
                {
                    matches++;
                }
            Patient[] result = new Patient[matches];
            int resultIndex = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_patients[i].FirstName.ToLower().Contains(query) || _patients[i].LastName.ToLower().Contains(query))
                {
                    result[resultIndex++] = _patients[i];
                    resultIndex++;
                }
            }
            return result;
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

                    _count--;
                    Array.Clear(_patients, _count, 1);
                    return true;
                }
            }
            return false;
        }

        public void DisplayAll()
        {
            Console.WriteLine($"\n=== Пацієнти ({_count} / {MaxPatients}) ===");
            if (_count == 0)
            {
                Console.WriteLine("Список пацієнтів порожній.");

            }
            else
            {
                for (int i = 0; i < _count; i++)
                {
                    Console.WriteLine(_patients[i]);
                }
            }
        }

        public void DisplayStats()
        {
            Console.WriteLine($"\n=== Статистика пацієнтів ===");

            if(_count==0)
            {
                Console.WriteLine("Список пацієнтів порожній.");
                return;
            }

            int ageSum = 0;
            int youngestIndex = 0;
            int oldestIndex = 0;    
            int adultCount = 0;

            for(int i = 0; i < _count; i++)
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

            double averageAge = (double)ageSum / _count;

            Console.WriteLine($"Всього: {_count}");
            Console.WriteLine($"Середній вік: {averageAge:F1} р.");
            Console.WriteLine($"Наймолодший: {_patients[youngestIndex].FullName} " +
        $"({_patients[youngestIndex].Age} р.)");
            Console.WriteLine($"Найстарший: {_patients[oldestIndex].FullName} " +
        $"({_patients[oldestIndex].Age} р.)");
            Console.WriteLine($"Дорослих: {adultCount} з {_count}");

        }
    }
}
