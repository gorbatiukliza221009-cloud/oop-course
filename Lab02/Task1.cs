using System;

namespace Lab02
{
    internal class Task1
    {
        public static void Run()
        {
            int n= int.Parse(Console.ReadLine()!);
            double[] weights= new double[n];

            for(int i = 0; i <n; i++)
            {
                weights[i] = double.Parse(Console.ReadLine()!);
            }

            double sum = 0;
            double min = weights[0];
            double max = weights[0];

            foreach(double weight in weights)
            {
                sum += weight;

                if(weight < min)
                {
                    min = weight;
                }
                if(weight > max)
                {
                    max = weight;
                }
            }

            double average = sum / n;

            int aboveAverage = 0;

            foreach(double weight in weights)
            {
                if (weight > average)
                {
                    aboveAverage++;
                }
            }

            Console.WriteLine($"Кількість: {n}");
            Console.WriteLine($"Середня вага: {average:F1} кг");
            Console.WriteLine($"Мін /Макс: {min:F1} / {max:F1} кг");
            Console.WriteLine($"Вище середнього: {aboveAverage} з {n}");
        }
    }
}
