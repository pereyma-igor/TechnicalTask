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
            "", "ten ", "twenty-", "thirty-", "fourty-", "fifty-",
            "sixty-", "seventy-", "eighty-", "ninety-"
        };

    /// <summary>
    /// Number translation with ending
    /// </summary>
    /// <param name="val">Число</param>
    /// <param name="one">The genus of the noun in the singular</param>
    /// <returns>Translates a number, given the ending</returns>
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
    /// Choosing the correct noun ending
    /// </summary>
    /// <param name="val">Number</param>
    /// <param name="one">The form of the noun in the singular</param>
    /// <param name="two">The form of the noun is different</param>
    /// <returns>Returns the ending of a noun that corresponds to an excellent</returns>
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
    /// Convert an integer to a string
    /// </summary>
    /// <param name="val">Number</param>
    /// <returns>String</returns>
    public static string Str(int val)
    {
        if (val < 0) { val = -val;}

        int n = (int)val;

        StringBuilder r = new StringBuilder();

        if (0 == n) r.Append("0 ");
        if (n % 1000 != 0)
            r.Append(UsdParse.Str(n, true, "", ""));

        n /= 1000;

        r.Insert(0, UsdParse.Str(n, false, " thousand", " thousand"));
        n /= 1000;

        r.Insert(0, UsdParse.Str(n, true, " million", " millions"));
        n /= 1000;

        r.Insert(0, UsdParse.Str(n, true, " billion", " billions"));
        n /= 1000;

        r[0] = char.ToUpper(r[0]); // capitalize the first letter

        if (val % 10 == 1)
            return $"{r.ToString()}";
        else if (val % 10 > 1 && val % 10 < 5)
            return $"{r.ToString()}";
        else return $"{r.ToString()}";
    }

    /// <summary>
    /// Method for calling verbal ending after converting to dollar
    /// </summary>
    /// <param name="val">Number</param>
    /// <returns>If the singular is dollar / if the plural is dollars</returns>
    public static string Currency(int val)
    {
        if (val % 10 == 1)
            return "dollar";
        else return "dollars";
    }

    /// <summary>
    /// Method for calling verbal ending after converting to cents
    /// </summary>
    /// <param name="val">Number</param>
    /// <returns>If the singular is cent / if the plural is cents</returns>
    public static string Coins(int val)
    {
        if (val % 10 == 1)
            return "cent";
        else return "cents";
    }
}
