using System;
using System.Text;
using System.Text.RegularExpressions;

namespace task_3
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = InputText();
            ShowResultOfSearchingDigits(text);
        }

        static string InputText()
        {
            var text = new StringBuilder(string.Empty, 2000);
            while(string.IsNullOrEmpty(text.ToString()))
            {
                Console.Clear();
                Console.WriteLine("Введите непустую строку текста:");
                text.Append(Console.ReadLine());
            }
            return text.ToString();
        }

        static bool IsContainingDigits(string text)
        {
            bool digitIsFounded = false;
            var pattern = new Regex("[0-9]+");
            var match = pattern.Match(text);
            if (match.Success)
                digitIsFounded = true;
            return digitIsFounded;
        }

        static void ShowResultOfSearchingDigits(string text)
        {
            if (IsContainingDigits(text))
                Console.WriteLine("В тексте есть не менее одной арабской цифры");
            else
                Console.WriteLine("В тексте нет арабских цифр");
        }
    }
}
