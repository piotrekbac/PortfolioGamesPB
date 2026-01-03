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

            // Pętla główna gry, która trwa do momentu zgadnięcia liczby
            while (!isGuessed)
            {
                // Zwiększam licznik prób
                attempts++;

                // Pobieram od użytkownika jego typ i waliduję go
                int userGuess = GetValidNumber();

                // Sprawdzam, czy podana liczba jest mniejsza, większa czy równa wylosowanej liczbie
                if (userGuess == targetNumber)
                {
                    // Ustawiamy flagę zgadnięcia na true i wyświetlam gratulacje w odpowiedniej szacie graficznej
                    isGuessed = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nGratulacje! Zgadłeś liczbę {targetNumber} w {attempts} próbach.");
                    Console.ResetColor();
                }
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

                // W przypadku błędu wyświetlam komunikat i proszę o ponowne wprowadzenie
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Błąd: To nie jest poprawna liczba. Spróbuj ponownie.\n");
                Console.ResetColor();
            }
        }
    }
}
