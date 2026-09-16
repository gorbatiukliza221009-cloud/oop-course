using System;

namespace Lab02
{
    internal static class Task5
    {
        public static void Run()
        {
            int n = int.Parse(Console.ReadLine()!);
            int[,] matrix = new int[n,n];

            for (int i = 0; i < n; i++)
            {
                string[] values = Console.ReadLine()!.Split(' ');

                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = int.Parse(values[j]);
                }
            }
                int sum1 = 0;
                int sum2 = 0;
                int[] mainDiagonal = new int[n];
                int[] secondaryDiagonal = new int[n];

                for (int i = 0; i < n; i++)
                {
                    mainDiagonal[i] = matrix[i, i];
                    sum1 += matrix[i, i];
                    secondaryDiagonal[i] = matrix[i, n-1-i];
                    sum2 += matrix[i, n-1-i];
                }

            Console.WriteLine($"Головна діагональ: {string.Join(", ", mainDiagonal)} (сума = {sum1})");
            Console.WriteLine($"Побічна діагональ: {string.Join(", ", secondaryDiagonal)} (сума = {sum2})");
        }
    }
}
