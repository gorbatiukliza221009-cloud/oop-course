using System;

namespace Lab02
{
    internal static class Task6
    {
        public static void Run()
        {
            int n = int.Parse(Console.ReadLine()!);
            int[][] costs = new int[n][];

            for (int i = 0; i < n; i++)
            {
                int k = int.Parse(Console.ReadLine()!);

                costs[i] = new int[k];

                for (int j = 0; j < k; j++)
                {
                    costs[i][j] = int.Parse(Console.ReadLine()!);
                }
            }

            int maxIncome = 0;
            int maxDoctor = 0;

            for (int i = 0; i < n; i++)
            {
                int sum = 0;

                for (int j = 0; j < costs[i].Length; j++)
                {
                    sum += costs[i][j];
                }

                double avg= (double)sum/ (double)costs[i].Length;

                Console.WriteLine(
                    $"Лікар {i + 1}: {costs[i].Length} прийоми," +
                    $"сума={sum} грн, середня={avg:F2} грн");

                if (sum > maxIncome)
                {
                    maxIncome = sum;
                    maxDoctor = i;
                }
            }

            Console.WriteLine($"Найбільший дохід: Лікар {maxDoctor + 1} ({maxIncome} грн)");
        }
    }
}
