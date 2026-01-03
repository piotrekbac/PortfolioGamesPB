using System;
using ConsoleGames.Helpers;

// Piotr Bacior - Portfolio Project 2026

namespace ConsoleGames
{
    // Główna klasa programu zawierająca punkt wejścia Main
    internal class Program
    {
        // Punkt wejścia aplikacji
        static void Main(string[] args)
        {
            // Wywołuję metodę DisplayHeader z klasy AuthorInfo, aby wyświetlić nagłówek gry
            AuthorInfo.DisplayHeader("Hub Gier Konsolowych");

            // Wyświetlam powitanie użytkownika i instrukcje zamknięcia programu
            Console.WriteLine("Witaj w projekcie gier konsolowych!");
            Console.WriteLine("Naciśnij dowolny klawisz, aby zamknąć...\n");
            Console.ReadKey();
        }
    }
}
