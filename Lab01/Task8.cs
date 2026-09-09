using System;

namespace Lab01
{
    internal class Task8
    {
        public static void Run()
        {
            double weight = double.Parse(Console.ReadLine()!);
            double height = double.Parse(Console.ReadLine() !);

            double price = double.Parse(Console.ReadLine() !) ;
            int quantity = int.Parse(Console.ReadLine() !) ;
            int discount = int.Parse(Console.ReadLine()!) ;

            int birthYear = int.Parse(Console.ReadLine()!);

            int systolic = int.Parse(Console.ReadLine()!);
            int diastolic = int.Parse(Console.ReadLine()!);

            double bmi = CalculateBMI(weight, height);
            string bmiCategory = GetBMICategory(bmi);

            double cost = CalculateCost(price, quantity, discount);

            int age = CalculateAge(birthYear);
            string ageCategory = GetAgeCategory(age);

            string pressureStatus = GetPressureStatus(systolic, diastolic);

            Console.WriteLine($"ІМТ: {bmi:F2} -> {bmiCategory}");
            Console.WriteLine($"Сума: {cost:F2} грн");
            Console.WriteLine($"Вік: {age} р., категорія: {ageCategory}");
            Console.WriteLine($"Тиск: {systolic}/{diastolic} - {pressureStatus}");
        }

        static double CalculateBMI(double weight, double height)
        {
            return weight/ (height * height);
        }

        static string GetBMICategory(double bmi)
        {
            if (bmi < 18.5)
            {
                return "недостатня вага";
            }

            else if (bmi < 25)
            {
                return "норма";
            }

            else if (bmi < 30)
            {
                return "надмірна вага";
            }

            else
            {
                return "ожиріння";
            }
        }

        static double CalculateCost(double price, int quantity, int discount)
        {
            return price*quantity*(1-discount/100.0);
        }

        static int CalculateAge(int birthYear)
        {
            return 2026 - birthYear;
        }

        static string GetAgeCategory(int age)
        {
            if (age < 18)
            {
                return "дитина";
            }

            else if (age < 60)
            {
                return "дорослий";
            }

            else
            {
                return "пенсіонер";
            }
        }

            static string GetPressureStatus(int systolic, int diastolic)
            {
                if(systolic <120&& diastolic < 80)
                {
                    return "норма";
                }

                else if(systolic <130 && diastolic <80)
                {
                    return "підвищений";
                }

                else if(systolic <140 || diastolic <90)
                {
                    return "гіпертонія 1 ступеня";
                }

                else
                {
                    return "гіпертонія 2 ступеня";
                }
            }


    }
}
