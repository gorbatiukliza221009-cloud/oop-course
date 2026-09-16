using System;

namespace Lab02
{
    internal static class Task7
    {
        public static void Run()
        {
            Console.Write("Введіть кількість пацієнтів: ");
            int n = int.Parse(Console.ReadLine()!);

            string[] names = new string[n];
            double[] bmis = new double[n];

            for(int i = 0; i < n; i++)
            {
                Console.Write($"Введіть ім'я пацієнта {i + 1}: ");
                names[i] = Console.ReadLine()!;
                Console.Write($"Введіть ІМТ пацієнта {i + 1}: ");
                bmis[i] = double.Parse(Console.ReadLine()!);
            }

            for(int i = 0; i < n-1; i++)
            {
                for(int j = 0; j < n - i - 1; j++)
                {
                    if (bmis[j] < bmis[j + 1])
                    {
                        double tempBmi = bmis[j];
                        bmis[j] = bmis[j + 1];
                        bmis[j + 1] = tempBmi; 

                        string tempName = names[j];
                        names[j] = names[j + 1];
                        names[j + 1] = tempName;
                    }
                }
            }

            Console.WriteLine("=== Рейтинг ІМТ ===");

            for(int i = 0; i < n; i++)
            {
                Console.WriteLine($"{i + 1}. {names[i]}: {bmis[i]:F2}");
            }
        }
    }
}
