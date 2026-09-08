using System;

namespace Lab01
{
    internal class Task2
    {
        public static void Run()
        {
            double price = double.Parse(Console.ReadLine()!);
            int quantity = int.Parse(Console.ReadLine()!);
            int discount = int.Parse(Console.ReadLine()!);
            double sum = price * quantity*(1-discount/100.0);
            Console.WriteLine($"Сумма: {sum:F2}");
        }
    }
}
