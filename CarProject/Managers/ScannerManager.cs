using CarProject.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProject.Mangers
{
    public static class ScannerManager
    {
        public static int ReadInteger(string caption)
        {
        l1:

            Console.Write(caption);
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            if (!int.TryParse(Console.ReadLine(), out int value))
            {
                PrintError("It is Not True Information, Try Again: ");
                goto l1;
            }

            Console.ResetColor();
            return value;
        }

        public static double ReadDouble(string caption)
        {
        l1:
            Console.Write(caption);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            if (!double.TryParse(Console.ReadLine(), out double value))
            {
                PrintError("It is Not True Information, Try Again: ");
                goto l1;
            }

            Console.ResetColor();
            return value;
        }

        public static string ReadString(string caption)
        {
        l1:
            Console.Write(caption);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            string value = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(value))
            {
                PrintError("It is Not True Information, Try Again: ");
                goto l1;
            }

            Console.ResetColor();
            return value;
        }

        public static Menu ReadMenu(string caption)
        {
        l1:
            Console.Write(caption);

            if (!Enum.TryParse(Console.ReadLine(), out Menu m))
            {
                PrintError("Select from the Menu: ");
                goto l1;
            }
            Console.ResetColor();
            return m;
        }

        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static DateTime ReadDate(string caption)
        {
        l1:
            Console.Write($"{caption} [yyyy]");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime value))
            {
                PrintError("It is Not True Information, Try Again: ");
                goto l1;
            }
            Console.ResetColor();
            return value;
        }

        public static FuelType FuelType(string caption)
        {
        l1:
            Console.Write(caption);

            if (!Enum.TryParse(Console.ReadLine(), out FuelType m))
            {
                PrintError("Select from the Menu: ");
                goto l1;
            }
            Console.ResetColor();
            return m;
        }

    }
}
