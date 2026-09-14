using System;

namespace Lab02
{
    internal class Task4
    {
        public static void Run()
        {
            int n = int.Parse(Console.ReadLine()!);
            int m = int.Parse(Console.ReadLine()!);

            int[,] matrix = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                string[] values = Console.ReadLine()!.Split(' ');

                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = int.Parse(values[j]);
                }
            }

            for (int i = 0; i < n; i++)
            {
                int sum = 0;

                for (int j = 0; j < m; j++)
                {
                    sum += matrix[i, j];
                }

                Console.WriteLine($"Лікар {i + 1}: {sum} прийомів");
            }

            Console.Write("По днях: ");

            for (int j = 0; j < m; j++)
            {
                int sum = 0;

                for (int i = 0; i < n; i++)
                {
                    sum += matrix[i, j];
                }

                Console.Write(sum);

                if (j < m - 1)
                {
                    Console.Write(", ");
                }
            }

            Console.WriteLine();

            int max = matrix[0, 0];
            int maxRow = 0;
            int maxCol = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (matrix[i, j] > max)
                    {
                        max = matrix[i, j];
                        maxRow = i;
                        maxCol = j;
                    }
                }
            }

            Console.WriteLine(
                $"Максимум: {max} (Лікар {maxRow + 1}, День {maxCol + 1})");
        }
    }
}
    
    