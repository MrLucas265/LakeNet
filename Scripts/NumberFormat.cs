using System.IO;
using System;
using System.Linq;

public class NumberFormat
{
    // Very simple example, gonna throw exception for numbers bigger than 10^12
    static readonly string[] ShortCashsuffixes = { "", "k", "M", "B" };
    static readonly string[] Datasuffixes = {"B","KB", "MB", "GB", "TB","PB"};

    public static string ShortCurrency(long cash, string prefix = "$")
    {
        int k;
        if (cash == 0)
            k = 0;    // log10 of 0 is not valid
        else
            k = (int)(Math.Log10(cash) / 3); // get number of digits and divide by 3
        var dividor = Math.Pow(10, k * 3);  // actual number we print
        var text = prefix + (cash / dividor).ToString("F2") + ShortCashsuffixes[k];
        return text;
    }

    public static string Data(double value, string prefix = "")
    {
        var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");
        int power = 0;

        if(person.Gateway.CurrentOS.Options.DataSuffix == false)
        {
            power = 1000;
        }
        else
        {
            power = 1024;
        }

        if(value >= power)
        {
            string[] suffixes = {"KB", "MB", "GB","TB", "PB", "EB", "ZB","YB"};
            for (int i = 0; i < suffixes.Length; i++)
            {
                if (value < (Math.Pow(power, i + 1)))
                {
                    return ThreeNonZeroDigits(value /
                        Math.Pow(power, i)) +
                        "" + suffixes[i];
                }
            }

            return ThreeNonZeroDigits(value /
                Math.Pow(power, suffixes.Length - 1)) +
                "" + suffixes[suffixes.Length - 1];
        }
        else
        {
            return value + "Bytes";
        }
    }

    private static string ThreeNonZeroDigits(double value)
    {
        if (value >= 1000)
        {
            // No digits after the decimal.
            return value.ToString("0,0");
        }
        else if (value >= 100)
        {
            // No digits after the decimal.
            return value.ToString("0.0");
        }
        else if (value >= 10)
        {
            // One digit after the decimal.
            return value.ToString("0.0");
        }
        else
        {
            // Two digits after the decimal.
            return value.ToString("0.00");
        }
    }
}