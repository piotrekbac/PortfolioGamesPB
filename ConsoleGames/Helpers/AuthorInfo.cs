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
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine($"       PROJEKT PORTFOLIO: PIOTR BACIOR      ");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n");
            
        }

        // Definiuję metodę WriteColor do wyświetlania kolorowych komunikatów w konsoli
        public static void WriteColor(string message, ConsoleColor color, bool newLine = true)
        {
            // Ustawiam kolor tekstu w konsoli i wyświetlam komunikat
            Console.ForegroundColor = color;
            if (newLine)
            {
                Console.WriteLine(message);
            }

            // Jeśli newLine jest false, wyświetlam komunikat bez nowej linii
            else
            {
                Console.Write(message);
            }

            // Resetuję kolor tekstu do domyślnego
            Console.ResetColor();
        }

        // Definiuję metodę WriteError do wyświetlania komunikatów o błędach w kolorze czerwonym
        public static void WriteError(string errorMessage)
        {
            // Ustawiam kolor tekstu na czerwony i wyświetlam komunikat o błędzie
            Console.ForegroundColor = ConsoleColor.Red;

            // Wyświetlam komunikat o błędzie z prefiksem "BŁĄD: "
            Console.WriteLine($" BŁĄD: {errorMessage}");

            // Resetuję kolor tekstu do domyślnego
            Console.ResetColor();
        }
    }
}
