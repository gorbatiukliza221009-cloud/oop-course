using System;

namespace Lab01
{
    internal class Task6
    {
        public static void Run()
        {
            Console.Write("Введіть номер картки:");
            int cardNumber = int.Parse(Console.ReadLine()!);
            int lastDigit = cardNumber % 10;

            string department = lastDigit switch
            {
                0 or 1 => "загальна терапія",
                2 or 3 => "хірургія",
                4 or 5 => "кардіологія",
                6 or 7 => "неврологія",
                8 or 9 => "офтальмологія",
                _ => "невідомий відділ"
            };

            string benefit= cardNumber%2==0? "так" : "ні";
            string exemination = cardNumber % 3 == 0 ? "так" : "ні";

            Console.WriteLine($"Відділення: {department}");
            Console.WriteLine($"Пільгова: {benefit}");
            Console.WriteLine($"Огляд: {exemination}");
        }
    }
}
