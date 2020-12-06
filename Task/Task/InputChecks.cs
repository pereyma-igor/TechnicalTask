using System;
using System.Collections.Generic;
using System.Text;

namespace Task
{
    class InputChecks
    {
        /// <summary>
        /// Метод, який перевіряє вибір мови користувачем та за вибором мови викликає метод CheckUkrainianNumbers або CheckEnglishNumbers
        /// </summary>
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
                Console.Clear();
                Console.WriteLine("Вітаємо! Ви вибрали українську мову!");
                CheckUkrainianNumbers();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Congratulation! You choose English!");
                CheckEnglishNumbers();
            }
        }

        /// <summary>
        /// Метод, який перевіряє коректність вводу українського числа користувачем та викликає метод для пропису цього числа
        /// </summary>
        public static void CheckUkrainianNumbers()
        {
            Console.WriteLine("Введіть число в форматі xxxxxx,xx! Число не повинне бути більшим за 2147483647,00");
            
            string str = Convert.ToString(Console.ReadLine());
            while (!double.TryParse(str, out _) || str[str.Length - 3] != ',' 
                || double.Parse(str) > 2147483647.00 
                || double.Parse(str) <0 )
            {
                Console.Clear();
                Console.WriteLine("Невірно введене число! \n Введіть число в форматі xxxxxx,xx! Число не повинне бути більшим за 2147483647,00");
                str = Convert.ToString(Console.ReadLine());
            }

            Console.Clear();
            Console.WriteLine($"Ви ввели число: {double.Parse(str)}");

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

            if (coins == 0)
                Console.WriteLine($"{UahParse.Str(currency)}{UahParse.Currency(currency)}");
            else if (currency == 0)
                Console.WriteLine($"{UahParse.Str(coins).ToLower()}{UahParse.Coins(coins)}");
            else
                Console.WriteLine($"{UahParse.Str(currency)}{UahParse.Currency(currency)} {UahParse.Str(coins).ToLower()}{UahParse.Coins(coins)}");
        }

        /// <summary>
        /// A method that checks the correctness of the input of the Ukrainian number by the user and calls the method to write this number
        /// </summary>
        public static void CheckEnglishNumbers()
        {
            Console.Clear();
            Console.WriteLine("Enter a number in the format xxxxxx.xx! The number should not exceed 2147483647.00");

            string str = Convert.ToString(Console.ReadLine());
            while (str[^3] != '.'|| !double.TryParse(str.Replace('.', ','), out _) 
                || double.Parse(str.Replace('.', ',')) > 2147483647.00
                || double.Parse(str.Replace('.', ',')) < 0)
            {
                Console.Clear();
                Console.WriteLine("Invalid number entered! \nEnter a number in the format xxxxxx.xx! The number should not exceed 2147483647.00");
                str = Convert.ToString(Console.ReadLine());
            }

            Console.Clear();
            Console.WriteLine($"You entered a number: {double.Parse(str.Replace('.', ','))}");

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

            if(coins == 0)
                Console.WriteLine($"{UsdParse.Str(currency)}{UsdParse.Currency(currency)}");
            else if(currency == 0)
                Console.WriteLine($"{UsdParse.Str(coins).ToLower()}{UsdParse.Coins(coins)}");
            else 
                Console.WriteLine($"{UsdParse.Str(currency)}{UsdParse.Currency(currency)} and {UsdParse.Str(coins).ToLower()}{UsdParse.Coins(coins)}");
        }

    }
}
