using System;

namespace Lab01
{
    internal class Task7
    {
        public static void Run()
        {
            Console.Write("Введіть  какількість прийомів:");
            int n = int.Parse(Console.ReadLine()!);

            decimal[] prices = new decimal[n];

            for (int i = 0; i < n; i++)
            {
                Console.Write($"Введіть ціну прийому {i + 1}:");
                prices[i] = decimal.Parse(Console.ReadLine()!);
            }

            decimal sum = 0;
            decimal min = prices[0];
            decimal max = prices[0];

            foreach (decimal price in prices)
            {
                sum += price;
                if (price < min)
                {
                    min = price;
                }
                if (price > max)
                {
                    max = price;
                }
            }

            decimal average = sum / n;

            int aboveAverageCount = 0;

            for (int i = 0; i < n; i++)
            {
                if (prices[i] > average)
                {
                    aboveAverageCount++;
                }
            }

            int expensiveIndex = -1;
            int j = 0;

            while (j < n)
            {
                if (prices[j] > 1000)
                {
                    expensiveIndex = j;
                    break;
                }

                j++;
            }

                Console.WriteLine("===Звіт по прийомах===");
                Console.WriteLine($"Кількість: {n}");
                Console.WriteLine($"Загальна сума: {sum:F2} грн");
                Console.WriteLine($"Середня: {average:F2} грн");
                Console.WriteLine($"Мін / Макс: {min:F2} грн / {max:F2} грн");
                Console.WriteLine($"Вище середнього: {aboveAverageCount} з {n}");

                if (expensiveIndex != -1)
                {
                    Console.WriteLine($"Перший > 1000:  {expensiveIndex + 1} - {prices[expensiveIndex]:F2} грн");
                }
                else
                {
                    Console.WriteLine("Перший > 1000: немає");
                }

                Console.WriteLine("========================");

        }
    }
}
