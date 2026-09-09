using System;

namespace Lab01
{
    internal class Task5
    {
        public static void Run()
        {
            Console.Write("Введіть номер дня (1-7):");
            int day = int.Parse(Console.ReadLine()!);
            string schedule = day switch
            {
                1 => "Понеділок: 8:00 - 18:00",
                2 => "Вівторок: 8:00 - 18:00",
                3 => "Середа: 9:00 - 17:00",
                4 => "Четвер: 8:00 - 18:00",
                5 => "П'ятниця: 8:00 - 16:00",
                6 => "Субота: 9:00 - 14:00",
                7 => "Неділя - вихідний",
                _   => "невідомий день"
            };
            Console.WriteLine(schedule);
                    }
    }
}
