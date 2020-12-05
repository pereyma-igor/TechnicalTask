using System;
using System.Text;
using Task;

public static class UsdParse
{
    private static string[] hunds =
    {
            "", "one hundred  ", "two hundred ", "three hundred ", "four hundred ",
            "five hundred ", "six hundred ", "seven hundred ", "eight hundred ", "nine hundred "
        };
    private static string[] tens =
    {
            "", "ten ", "twenty ", "thirty ", "fourty ", "fifty ",
            "sixty ", "seventy ", "eighty ", "ninety "
        };

    /// <summary>
    /// перевод числа з урахуванням закінчення
    /// </summary>
    /// <param name="val">Число</param>
    /// <param name="one">рід іменника в однині</param>
    /// <returns>Переводить число, враховуючи закінчення</returns>
    public static string Str(int val, bool male, string one, string two)
    {
        string[] frac20 =
        {
                "", "one", "two", "three", "four", "five", "six",
                "seven", "eight", "nine", "ten", "eleven",
                "twelve", "thirteen", "fourteen", "fifteen",
                "sixteen", "seventeen", "eighteen", "nineteen"
            };

        int num = val % 1000;
        if (0 == num) return "";
        if (num < 0) throw new ArgumentOutOfRangeException("val", "The parameter cannot be negative");
        if (num > 2147483647) throw new ArgumentOutOfRangeException("val", "The parameter cannot be greater than 2147483647.00");
        if (!male)
        {
            frac20[1] = "one ";
            frac20[2] = "two ";
        }

        StringBuilder r = new StringBuilder(hunds[num / 100]);

        if (num % 100 < 20)
        {
            r.Append(frac20[num % 100]);
        }
        else
        {
            r.Append(tens[num % 100 / 10]);
            r.Append(frac20[num % 10]);
        }

        r.Append(Case(num, one, two));

        if (r.Length != 0) r.Append(" ");
        return r.ToString();
    }


    /// <summary>
    /// Вибір правильного відмінника закінчення іменника
    /// </summary>
    /// <param name="val">Число</param>
    /// <param name="one">Форма іменника в єдиному числі</param>
    /// <param name="two">Форма іменника від двох до чотирьох</param>
    /// <returns>Повертає закінчення іменника,яке відповідає відміннику</returns>
    public static string Case(int val, string one, string two)
    {
        int t = (val % 100 > 20) ? val % 10 : val % 20;

        switch (t)
        {
            case 1: return one;
            default: return two;
        }
    }

    /// <summary>
    /// Перевід цілого числа в рядок
    /// </summary>
    /// <param name="val">Число</param>
    /// <returns>Повертає стрінг числа</returns>
    public static string Str(int val)
    {
        bool minus = false;
        if (val < 0) { val = -val; minus = true; }

        int n = (int)val;

        StringBuilder r = new StringBuilder();

        if (0 == n) r.Append("0 ");
        if (n % 1000 != 0)
            r.Append(UsdParse.Str(n, true, "", ""));

        n /= 1000;

        r.Insert(0, UsdParse.Str(n, false, " thousand", " thousands"));
        n /= 1000;

        r.Insert(0, UsdParse.Str(n, true, " million", " millions"));
        n /= 1000;

        r.Insert(0, UsdParse.Str(n, true, " billion", " billions"));
        n /= 1000;

        r[0] = char.ToUpper(r[0]); //робимо першу букву великою

        if (val % 10 == 1)
            return $"{r.ToString()}";
        else if (val % 10 > 1 && val % 10 < 5)
            return $"{r.ToString()}";
        else return $"{r.ToString()}";
    }

    public static string Currency(int val)
    {
        if (val % 10 == 1)
            return "dollar";
        else return "dollars";
    }

    public static string Coins(int val)
    {
        if (val % 10 == 1)
            return "cent";
        else return "cents";
    }
}
