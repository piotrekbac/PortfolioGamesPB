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
        // Definiuję metodę Play, w której znajduje się główna logika gry
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

            while (!isGuessed)
            {
                attempts++;

            }
        }

        // Metoda do pobierania i walidacji liczby od użytkownika
        private int GetValidNumber()
        {
            // Pętla do momentu uzyskania poprawnej liczby
            while (true)
            {
                // Pytam użytkownika o podanie liczby
                Console.WriteLine("Twój typ: ");

                // Odczytuję dane wejściowe od użytkownika
                string input = Console.ReadLine();

                // Próbuję przekonwertować dane wejściowe na liczbę całkowitą
                if (int.TryParse(input, out int result))
                {
                    return result;      // Zwracam poprawną liczbę
                }
            }
        }
    }
}
