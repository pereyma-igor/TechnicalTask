using System;
using System.Text;
using Task;

public class UahParse
{
    private static string[] hunds =
    {
            "", "сто  ", "двісті ", "триста ", "чотириста ",
            "п'ятсот ", "шістсот ", "сімсот ", "вісімсот ", "дев'ятсот "
        };
    private static string[] tens =
    {
            "", "десять ", "двадцять ", "тридцять ", "сорок ", "п'ятдесят ",
            "шістдесят ", "сімдесят ", "вісімдесят ", "дев'яносто "
        };

    /// <summary>
    /// перевод числа з урахуванням закінчення
    /// </summary>
    /// <param name="val">Число</param>
    /// <param name="male">рід іменника, який відноситься до числа</param>
    /// <param name="one">рід іменника в однині</param>
    /// <param name="two">рід іменника від двох до 4 </param>
    /// <param name="five">рід іменника від 5 і більше</param>
    /// <returns>Переводить число, враховуючи закінчення</returns>
    public static string Str(int val, bool male, string one, string two, string five)
    {
        string[] frac20 =
        {
                "", "одна", "два", "три", "чотири", "п'ять", "шість",
                "сім", "вісімь", "дев'ять", "десять", "одинадцать",
                "дванадцять", "тринадцять", "чотирнадцять", "п'ятнадцять",
                "шістнадцять", "сімнадцять", "вісімнадцять", "дев'ятнадцять"
            };

        int num = val % 1000;
        if (0 == num) return "";
        if (num < 0) throw new ArgumentOutOfRangeException("val", "Параметр не може бути від'ємним");
        if (num > 2147483647) throw new ArgumentOutOfRangeException("val", "Параметр не може бути більшим за 2147483647.00");
        if (!male)
        {
            frac20[1] = "один";
            frac20[2] = "дві";
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

        r.Append(Case(num, one, two, five));

        if (r.Length != 0) r.Append(" ");
        return r.ToString();
    }


    /// <summary>
    /// Вибір правильного відмінника закінчення іменника
    /// </summary>
    /// <param name="val">Число</param>
    /// <param name="one">Форма іменника в єдиному числі</param>
    /// <param name="two">Форма іменника від двох до чотирьох</param>
    /// <param name="five">Форма іменника від п'яти</param>
    /// <returns>Повертає закінчення іменника,яке відповідає відміннику</returns>
    public static string Case(int val, string one, string two, string five)
    {
        int t = (val % 100 > 20) ? val % 10 : val % 20;

        switch (t)
        {
            case 1: return one;
            case 2: case 3: case 4: return two;
            default: return five;
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
            r.Append(UahParse.Str(n, true, "", "", ""));

        n /= 1000;

        r.Insert(0, UahParse.Str(n, false, " тисяча", " тисячі", " тисяч"));
        n /= 1000;

        r.Insert(0, UahParse.Str(n, true, " міліон", " міліонів", " міліонів"));
        n /= 1000;

        r.Insert(0, UahParse.Str(n, true, " міліард", " міліардів", " міліардів"));
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
            return "гривня";
        else if (val % 10 > 1 && val % 10 < 5)
            return "гривні";
        else return "гривень";
    }

    public static string Coins(int val)
    {
        if (val % 10 == 1)
            return "копійка";

        else if (val % 10 > 1 && val % 10 < 5)
                return "копійки";

        else return "копійок";
    }
}
