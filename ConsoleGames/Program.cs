using System;
using ConsoleGames.Helpers;
using ConsoleGames.Games;

// Piotr Bacior - Portfolio Project 2026

namespace ConsoleGames
{
    // Główna klasa programu zawierająca punkt wejścia Main
    internal class Program
    {
        // Punkt wejścia aplikacji
        static void Main(string[] args)
        {
            // Inicjalizuję zmienną sterującą pętlą główną programu
            bool keepRunning = true;

            // Pętla główna programu, która pozwala na ponowne uruchomienie gry po jej zakończeniu
            while (keepRunning)
            {
                // Wywołuję metodę DisplayHeader z klasy AuthorInfo, aby wyświetlić nagłówek gry
                AuthorInfo.DisplayHeader("Hub Gier Konsolowych");

                // Wyświetlam menu wyboru gry dla użytkownika i przedstawienie opcji
                Console.WriteLine("Wybierz aplikację, którą chcesz uruchomić: ");
                Console.WriteLine("1. Gra: Zgadnij liczbę");
                Console.WriteLine("0. Wyjdź z programu");
                Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n");

                // Proszę użytkownika o wybór opcji
                Console.Write("Twój wybór: ");
            }
            
            
        }
    }
}
