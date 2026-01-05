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
            Console.WriteLine("Możesz wpisać 'q' w dowolnym momencie, aby wyjść.\n");
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
                int? userGuess = GetValidNumberOrExit(attempts);

                // Sprawdzam, czy podana liczba jest mniejsza, większa czy równa wylosowanej liczbie
                if (userGuess == targetNumber)
                {
                    // Ustawiamy flagę zgadnięcia na true i wyświetlam gratulacje w odpowiedniej szacie graficznej
                    isGuessed = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nGratulacje! Zgadłeś liczbę {targetNumber} w {attempts} próbach.");
                    Console.ResetColor();
                }

                // Informuję użytkownika, że liczba jest za mała 
                else if (userGuess < targetNumber)
                {

                    Console.WriteLine("Za mało! Spróbuj wyższej liczby.\n");
                }

                // Informuję użytkownika, że liczba jest za duża
                else
                {
                   Console.WriteLine("Za dużo! Spróbuj niższej liczby.\n");
                }
            }

            // Po zakończeniu gry wyświetlam komunikat i czekam na naciśnięcie klawisza przez użytkownika
            Console.WriteLine();
            Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do menu...\n");

            // Czekam na naciśnięcie klawisza przez użytkownika
            Console.ReadKey();
        }

        // Metoda do pobierania i walidacji liczby od użytkownika
        private int? GetValidNumberOrExit(int attempNumber)
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
