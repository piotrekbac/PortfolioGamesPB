using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Piotr Bacior - Portfolio Project 2026

namespace ConsoleGames.Helpers
{
    // Tworzę statyczną klasę AuthorInfo, która zawiera metodę DisplayHeader do wyświetlania informacji o autorze i tytule gry.
    public static class AuthorInfo
    {
        // Definiuję metodę DisplayHeader, która przyjmuje tytuł gry jako parametr i wyświetla go wraz z informacjami o autorze.
        public static void DisplayHeader(string gameTitle)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine($"   PROJEKT PORTFOLIO: PIOTR BACIOR");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{gameTitle}\n");
            Console.ResetColor();
        }
    }
}
