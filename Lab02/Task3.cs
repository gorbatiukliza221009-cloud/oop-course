using System;

namespace Lab02
{
    internal class Task3
    {
        public static void Run()
        {
            string[] days =
                {
                "Понеділок",
                "Вівторок",
                "Середа",
                "Четвер",
                "П'ятниця",
                "Субота",
                "Неділя"
            };

            int[] counts = new int[7];

            for (int i = 0; i < 7; i++)
            {
                counts[i] = int.Parse(Console.ReadLine()!);
            }

            int total = 0;
            int maxIndex = 0;
            int minIndex = 0;

            for (int i = 0; i < 7; i++)
            {
                total += counts[i];
                if (counts[i] > counts[maxIndex])
                {
                    maxIndex = i;
                }
                if (counts[i] < counts[minIndex])
                {
                    minIndex = i;
                }
            }

            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"{days[i],-12}: {counts[i]} пацієнтів");
            }

            Console.WriteLine($"Разом: {total}");
            Console.WriteLine($"Найбільше: {days[maxIndex]} ({counts[maxIndex]})");
            Console.WriteLine($"Найменше: {days[minIndex]} ({counts[minIndex]})");

        }
    }
}
