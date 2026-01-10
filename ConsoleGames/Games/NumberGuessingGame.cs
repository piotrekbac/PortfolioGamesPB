using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleGames.Helpers;

// Piotr Bacior - Portfolio Project 2026

namespace ConsoleGames.Games
{
    // Tworzę klasę NumberGuessingGame, która będzie zawierać logikę gry w zgadywanie liczb.
    public class NumberGuessingGame
    {
        // Zmienna przechowująca maksymalny zakres losowanej liczby
        private int maxRange;

        // Definiuję metodę Play, w której znajduje się główna logika gry
        public void Run()
        {
            // Wywołuję metodę SelectDifficulty, aby umożliwić użytkownikowi wybór poziomu trudności
            if (!SelectDifficulty())
            {
                return; // Użytkownik zdecydował się wyjść z menu wyboru poziomu trudności
            }

            // Wywołuję metodę DisplayHeader z klasy AuthorInfo, aby wyświetlić nagłówek gry
            AuthorInfo.DisplayHeader("Gra w Zgadywanie Liczb");

            // Wyświetlam powitanie użytkownika i instrukcje gry
            Console.WriteLine("Cześć graczu! Wylosowałem liczbę z zakresu 1 do 100");
            Console.WriteLine("Spróbuj ją zgadnąć w jak najmniejszej liczbie prób!\n");
            Console.WriteLine("Możesz wpisać 'q' w dowolnym momencie, aby wyjść.\n");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n");

            // Inicjalizuję generator liczb losowych i losuję liczbę do zgadnięcia
            Random random = new Random();
            int targetNumber = random.Next(1, maxRange + 1); // Losuję liczbę z zakresu od 1 do maxRange + 1 

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

                // Jeżeli użytkownik wpisał 'q' lub 'exit', wychodzę z gry, i wracamy do menu
                if (userGuess == null)
                {
                    AuthorInfo.WriteColor("\nWychodzisz z gry. Do zobaczenia!\n", ConsoleColor.DarkGray);

                    Thread.Sleep(1500);     // Małą pauza, żeby użytkownik zdążył przeczytać komunikat
                    return;                 // Użytkownik zdecydował się wyjść z gry
                }

                // Sprawdzam, czy podana liczba jest mniejsza, większa czy równa wylosowanej liczbie
                if (userGuess == targetNumber)
                {
                    // Ustawiamy flagę zgadnięcia na true i wyświetlam gratulacje w odpowiedniej szacie graficznej
                    isGuessed = true;
                    AuthorInfo.WriteColor($"\nGRATULACJE!", ConsoleColor.Green);
                    Console.WriteLine($"Odgadłeś liczbę: {targetNumber}");
                    Console.WriteLine($"Potrzebowałeś na to {attempts} prób!\n");
                }

                // Informuję użytkownika, że liczba jest za mała 
                else if (userGuess < targetNumber)
                {
                    AuthorInfo.WriteColor("Za mało! Spróbuj wyższej liczby.\n", ConsoleColor.DarkCyan);
                }

                // Informuję użytkownika, że liczba jest za duża
                else
                {
                   AuthorInfo.WriteColor("Za dużo! Spróbuj niższej liczby.\n", ConsoleColor.Magenta);
                }
            }

            // Po zakończeniu gry wyświetlam komunikat i czekam na naciśnięcie klawisza przez użytkownika
            Console.WriteLine();
            AuthorInfo.WriteColor("Dziękuję za grę! Naciśnij dowolny klawisz, aby wrócić do menu głównego.", ConsoleColor.Gray);

            // Czekam na naciśnięcie klawisza przez użytkownika
            Console.ReadKey();
        }

        // Metoda do wyświetlania menu wyboru poziomu trudności
        private bool SelectDifficulty()
        {
            AuthorInfo.DisplayHeader("Wybierz poziom trudności");
            Console.WriteLine("1. Poziom ŁATWY - (zakres 1-100) - Klasyk na rozgrzewkę");
            Console.WriteLine("2. Poziom ŚREDNI - (zakres 1-500) - Dla bardziej doświadczonych graczy");
            Console.WriteLine("3. Poziom TRUDNY - (zakres 1-1000) - Tylko dla odważnych!\n");
            Console.WriteLine("Naciśnij 'q' aby wrócić do menu głównego");
            Console.WriteLine("\n=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n");
            Console.Write("Twój wybór: ");

            // Pętla do momentu uzyskania poprawnego wyboru poziomu trudności
            while (true)
            {
                // Odczytuję wybór użytkownika z konsoli
                string input = Console.ReadLine();

                // Obsługa wyboru łatwego poziomu trudności (1-100)
                if (input == "1")
                {
                    maxRange = 100;     // Ustawiam zakres na 100
                    return true;        // Ustawiam zakres na 100 i zwracam true
                }

                // Obsługa wyboru średniego poziomu trudności (1-500)
                else if (input == "2")
                {
                    maxRange = 500;     // Ustawiam zakres na 500
                    return true;        // Ustawiam zakres na 500 i zwracam true
                }

                // Obsługa wyboru trudnego poziomu trudności (1-1000)
                else if (input == "3")
                {
                    maxRange = 1000;    // Ustawiam zakres na 1000
                    return true;        // Ustawiam zakres na 1000 i zwracam true
                }

                // Obsługa wyjścia z menu wyboru poziomu trudności
                else if (input.ToLower() == "q" || input.ToLower() == "exit")
                {
                    return false;       // Użytkownik chce wyjść z menu wyboru poziomu trudności
                }

                // Obsługa nieprawidłowego wyboru
                else
                {
                    AuthorInfo.WriteError("Nieprawidłowy wybór. Proszę wybrać 1, 2, 3 lub 'q' aby wyjść.");
                }
            }
        }

        // Metoda do pobierania i walidacji liczby od użytkownika
        private int? GetValidNumberOrExit(int attemptNumber, int currentMaxRange)
        {
            // Pętla do momentu uzyskania poprawnej liczby
            while (true)
            {
                // Wyświetlam informację o numerze próby
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Próba {attemptNumber}");

                Console.ResetColor();          // Resetuję kolor konsoli do domyślnego

                // Odczytuję dane wejściowe od użytkownika
                string input = Console.ReadLine();

                // Sprawdzam, czy gracz chce wyjść z gry
                if (input.ToLower() == "q" || input.ToLower() == "exit")
                {
                    return null; // Użytkownik chce wyjść z gry
                }

                // Próbuję przekonwertować dane wejściowe na liczbę całkowitą
                if (int.TryParse(input, out int result))
                {
                    // Sprawdzam, czy liczba jest w zakresie od 1 do 100
                    if (result < 1 || result > currentMaxRange)
                    {
                        // Wyświetlam komunikat o błędzie, jeśli liczba jest poza zakresem
                        AuthorInfo.WriteError("Liczba musi być w zakresie od 1 do 100. Spróbuj ponownie.");

                        continue;   // Kontynuuję pętlę, aby użytkownik mógł wprowadzić poprawną liczbę
                    }

                    return result; // Zwracam poprawnie wprowadzoną liczbę
                }

                // W przypadku błędu wyświetlam komunikat i proszę o ponowne wprowadzenie
                AuthorInfo.WriteError("Nieprawidłowe dane wejściowe. Proszę wpisać liczbę całkowitą od 1 do 100 lub 'q' aby wyjść.");
            }
        }
    }
}
