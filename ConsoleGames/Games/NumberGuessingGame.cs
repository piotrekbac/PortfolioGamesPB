using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGames.Helpers;

// Piotr Bacior - Portfolio Project 2026

namespace ConsoleGames.Games
{
    // Tworzę klasę NumberGuessingGame, która będzie zawierać logikę gry w zgadywanie liczb.
    public class NumberGuessingGame
    {
        public void Run()
        {
            // Wywołuję metodę DisplayHeader z klasy AuthorInfo, aby wyświetlić nagłówek gry
            AuthorInfo.DisplayHeader("Gra w Zgadywanie Liczb");

            // Wyświetlam powitanie użytkownika i instrukcje gry
            Console.WriteLine("Cześć graczu! Wylosowałem liczbę z zakresu 1 do 100");
            Console.WriteLine("Spróbuj ją zgadnąć w jak najmniejszej liczbie prób!\n");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n");

            // Inicjalizuję generator liczb losowych i losuję liczbę do zgadnięcia
            Random random = new Random();
            int targetNumber = random.Next(1, 101); // Losuję liczbę z zakresu 1-100

            // Inicjalizuję zmienne do śledzenia liczby prób i stanu zgadnięcia
            int attempts = 0;
            bool isGuessed = false;
        }
    }
}
