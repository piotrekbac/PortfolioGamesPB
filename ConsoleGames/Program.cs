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
            // Ustawiam tytuł okna konsoli 
            Console.Title = "Console Games - Projekt Portfolio Piotr Bacior 2026";

            // Inicjalizuję zmienną sterującą pętlą główną programu
            bool keepRunning = true;

            // Pętla główna programu, która pozwala na ponowne uruchomienie gry po jej zakończeniu
            while (keepRunning)
            {
                AuthorInfo.DisplayHeader("Hub gier konsolowych - Piotr Bacior");

                // Wyświetlam menu wyboru gry dla użytkownika i przedstawienie opcji
                Console.WriteLine("Wybierz aplikację, którą chcesz uruchomić: \n");
                Console.WriteLine("1. Gra: Zgadnij liczbę");
                Console.WriteLine("2. Gra: Kółko i krzyżyk");
                Console.WriteLine("3. Gra: Wisielec");
                Console.WriteLine("4. Gra: Snake");
                Console.WriteLine("0. Wyjdź z programu\n");
                Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n");

                // Proszę użytkownika o wybór opcji
                Console.Write("Twój wybór: ");

                // Odczytuję wybór użytkownika z konsoli i przechowuję go w zmiennej "choise"
                string choice = Console.ReadLine();

                // Przetwarzam wybór użytkownika za pomocą instrukcji switch
                switch (choice)
                {
                    // W przypadku wyboru "1" uruchamiam grę w zgadywanie liczb
                    case "1": 
                        NumberGuessingGame guessingGame = new NumberGuessingGame();
                        guessingGame.Run();
                        break;

                    // W przypadku wyboru "2" uruchamiam grę w kółko i krzyżyk
                    case "2":
                        TicTacToeGame ticTacToeGame = new TicTacToeGame();
                        ticTacToeGame.Run();
                        break;

                    // W przypadku wyboru "3" uruchamiam grę w wisielca
                    case "3":
                        HangmanGame hangmanGame = new HangmanGame();
                        hangmanGame.Run();
                        break;

                    // W przypadku wyboru "4" uruchamiam grę w snake'a
                    case "4":
                        SnakeGame snakeGame = new SnakeGame();
                        snakeGame.Run();
                        break;

                    // W przypadku wyboru "0" ustawiam zmienną sterującą na false, aby zakończyć program
                    case "0":
                        keepRunning = false;
                        Console.WriteLine("\nDziękuję za wizytę! Do zobaczenia!");

                        System.Threading.Thread.Sleep(1500);        // Mała pauza, żeby użytkownik zdążył przeczytać komunikat
                        break;

                    // W przypadku nieprawidłowego wyboru informuję użytkownika i proszę o ponowny wybór
                    default:
                        // Wyświetlam komunikat o błędzie dla nieprawidłowego wyboru
                        AuthorInfo.WriteError("Nieprawidłowy wybór. Naciśnij dowolny klawisz...");

                        // Czekam na naciśnięcie klawisza przez użytkownika przed ponownym wyświetleniem menu
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
