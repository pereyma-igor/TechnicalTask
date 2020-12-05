using System;
using System.Collections.Generic;
using System.Text;

namespace Task
{
    class InputChecks
    {
        public static void CheckLenguage()
        {
            Console.WriteLine("Виберіть мову/Change lenguage \n1 - Українська \n2 - English");
            int i;

            while (!(int.TryParse(Console.ReadLine(), out i)) || (i != 1 && i != 2))
            {
                Console.Clear();
                Console.Write("Невірний ввід!/Wrong input!");
                Console.WriteLine("Виберіть мову/Change lenguage \n1 - Українська \n2 - English");
            }

            if (i == 1)
            {
                Console.WriteLine("Вітаємо! Ви вибрали українську мову!");
                CheckUkrainianNumbers();
            }
            else
                Console.WriteLine("Congratulation! You choose English!");
        }

        public static void CheckUkrainianNumbers()
        {
            Console.WriteLine("Введіть число в форматі xxxxxx,xx! Число не повинне бути більшим за 2147483647,00");
            string str = Convert.ToString(Console.ReadLine());
            while (!double.TryParse(str, out _) || str[str.Length - 3] != ',')
            {
                Console.Clear();
                Console.WriteLine("Невірно введене число! \n Введіть число в форматі xxxxxx,xx! Число не повинне бути більшим за 2147483647,00");
                str = Convert.ToString(Console.ReadLine());
            }
            string strCurrency = "";
            string strCoins = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (i < str.Length - 3)
                    strCurrency += str[i];
                else if (i > str.Length - 3)
                    strCoins += str[i];
            }
            int currency = Int32.Parse(strCurrency);
            int coins = Int32.Parse(strCoins);
            Console.WriteLine($"{UahParse.Str(currency)}{UahParse.Currency(currency)} {UahParse.Str(coins).ToLower()}{UahParse.Coins(coins)}");
        }
    }
}
