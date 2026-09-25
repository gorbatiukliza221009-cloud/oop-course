using System;

namespace ClinicApp
{
    public class DoctorManager
    {
        private const int MaxDoctors = 50;
        private Doctor[] _doctors = new Doctor[MaxDoctors];
        private int _count = 0;
        public int Count
        {
            get { return _count; }
        }

        public void Add(Doctor doctor)
        {
            if (_count >= MaxDoctors)
            {
                Console.WriteLine("Досягнуто ліміту пацієнтів.");
                return;
            }

            _doctors[_count]=doctor;
            _count++;
            Console.WriteLine($"Лікаря {doctor.Id} {doctor.FullName} додано.");
        }

        public Doctor? FindById(int id)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_doctors[i].Id == id)
                {
                    return _doctors[i];
                }
            }
            return null;
        }

        public Doctor[] FindBySpeciality(string speciality)
        {
            string query = speciality.ToLower();
            int matches = 0;

            for (int i = 0; i < _count; i++)
                if (_doctors[i].Speciality.ToLower().Contains(query))
                    matches++;
            Doctor[] result = new Doctor[matches];
            int resultIndex = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_doctors[i].Speciality.ToLower().Contains(query))
                {
                    result[resultIndex++] = _doctors[i];
                    resultIndex++;
                }
            }
            return result;
        }

        public Doctor[] GetAll()
        {
            Doctor[] result = new Doctor[_count];
            for(int i = 0; i < _count; i++)
            {
                result[i] = _doctors[i];
            }
            return result;
        }

        public bool Remove(int id)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_doctors[i].Id == id)
                {
                    for (int j = i; j < _count - 1; j++)
                    {
                        _doctors[j] = _doctors[j + 1];
                    }
                    _count--;
                    Array.Clear(_doctors, _count, 1); 
                    return true;
                }
            }
            return false;
        }

        public void DisplayAll()
        {
            Console.WriteLine($"\n=== Лікарі ({_count} / {MaxDoctors}) ===");

            if(_count == 0)
            {
                Console.WriteLine("Список лікарів порожній.");
                return;
            }

            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine(_doctors[i]);
            }
        }
        public void DisplayStats()
        {
            Console.WriteLine($"\n=== Статистика лікарів ===");
            
            if (_count == 0)
            {
                Console.WriteLine("Список лікарів порожній.");
            }
            
            int availableDoctors = 0;
            for(int i = 0; i < _count; i++)
            {
                if (_doctors[i].IsAvailableNow)
                {
                    availableDoctors++;
                }
            }

            Console.WriteLine($"Всього:         {_count}");
            Console.WriteLine($"Доступні зараз: {availableDoctors}");
            Console.WriteLine("По спеціальностях:");



            for (int i = 0; i < _count; i++)
            {
                bool alredyShown = false;

                for (int j = 0; j < i; j++)
                {
                    if (_doctors[j].Speciality.ToLower() == _doctors[i].Speciality.ToLower())
                    {
                        alredyShown = true;
                        break;
                    }
                }

                if(alredyShown)
                {
                    continue;
                }

                int specialityCount = 0;

                for(int j = 0; j < _count; j++)
                {
                    if (_doctors[j].Speciality.ToLower() == _doctors[i].Speciality.ToLower())
                    {
                        specialityCount++;
                    }
                }
                Console.WriteLine($"  {_doctors[i].Speciality}: {specialityCount}");

            }
      
        }

    }
}
