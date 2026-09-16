using System;

namespace Lab02
{
    internal static class Task8
    {
        public static void Run()
        {
            Console.Write("Введіть кількість відділень: ");
            int d = int.Parse(Console.ReadLine()!);

            Console.Write("Введіть кількість тижнів: ");
            int w = int.Parse(Console.ReadLine()!);

            int[,,] patients = new int[d, w, 2];

            for (int i = 0; i < d; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        Console.Write($"Відділення {i + 1}, тиждень {j + 1}, " + $"{(k == 0 ? "ранок" : "вечір")}: ");
                        patients[i, j, k] = int.Parse(Console.ReadLine()!);
                    }
                }
            }

            int[] totals = new int[d];

            for (int i = 0; i < d; i++)
            {
                Console.WriteLine($"Відділення {i + 1}:");

                for (int j = 0; j < w; j++)
                {
                    int morning = patients[i, j, 0];
                    int evening = patients[i, j, 1];

                    int weekTotal = morning + evening;
                    totals[i] += weekTotal;

                    Console.WriteLine(
                        $"  Тиждень {j + 1}: ранок {morning}, " +
                        $"вечір {evening} → разом {weekTotal}");
                }

                Console.WriteLine($"Разом: {totals[i]} пацієнтів");
            }

            int maxIndex = 0;

            for(int i = 1; i < d; i++)
            {
                if (totals[i] > totals[maxIndex])
                {
                    maxIndex = i;
                }
            }

            Console.WriteLine($"Найзавантаженіше: Відділення {maxIndex + 1} " +
                $"({totals[maxIndex]} пацієнтів)");


        }
    }
}
