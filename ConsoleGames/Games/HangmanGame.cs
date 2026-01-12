using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ConsoleGames.Helpers;

// Piotr Bacior - Portfolio Project 2026

namespace ConsoleGames.Games
{
    // Tworzę klasę HangmanGame, która będzie zawierać logikę gry w wisielca.
    public class HangmanGame
    {
        // Definiuję bazę naszych haseł w postaci listy stringów pod nazwą wordRepository
        private List<string> wordRepository = new List<string>
        {
            "motyl", "kwiatek", "polana", "łąka", "drzewo", "słońce", "chmura", "deszcz",
        };

        private string targetWord;              // Słowo do odgadnięcia
        private List<char> guessedLetters;      // Lista odgadniętych liter
        private int lives;                      // Liczba pozostałych żyć

        // Definiuję metodę Run, w której znajduje się główna logika gry
        public void Run()
        {

        }

        // Definiuję metodę SetupGame, która inicjalizuje grę
        private void SetupGame()
        {
            // Inicjalizuję losowanie słowa
            Random random = new Random();

            // Losuję słowo do odgadnięcia z listy wordRepository
            targetWord = wordRepository[random.Next(wordRepository.Count)];

            // Inicjalizuję listę odgadniętych liter
            guessedLetters = new List<char>();

            // Spację w haśle traktuję jako odgadniętą literę - ułatwia to grę
            if (targetWord.Contains(" "))
            {
                guessedLetters.Add(' '); // Dodaj spację do odgadniętych liter, jeśli słowo zawiera spację
            }

            lives = 6; // Ustawiam liczbę żyć na 6 - odpowiednio do rysunku wisielca
        }

        // Definiuję metodę ProcessLetterGuess, która przetwarza zgadywanie pojedynczej litery
        private void ProcessLetterGuess(char letter)
        {
            // Sprawdzam, czy podana została litera 
            if (!char.IsLetter(letter))
            {
                // Wypisuję komunikat o błędzie, jeżeli podany znak nie jest literą
                AuthorInfo.WriteError("To nie jest litera! Proszę podać prawidłową literę.");
                return;
            }

            // Sprawdzam, czy litera była już odgadnięta wcześniej 
            if (guessedLetters.Contains(letter))
            {
                // Wypisuję komunikat o błędzie, jeżeli litera została już odgadnięta
                AuthorInfo.WriteError($"Litera '{letter}' została już odgadnięta. Spróbuj inną literę.");
                return;
            }

            guessedLetters.Add(letter); // Dodaj literę do odgadniętych liter

            // Sprawdzam, czy podana litera znajduje się w docelowym słowie
            if (targetWord.Contains(letter))
            {
                AuthorInfo.WriteColor("Dobrze! Trafiłeś literę!", ConsoleColor.Green);
                System.Threading.Thread.Sleep(600); // Mała pauza, żeby użytkownik zdążył przeczytać komunikat
            }

            // Jeżeli litera nie występuje w haśle - odejmuję jedno życie i wypisuję komunikat o niepoprawnym strzale
            else
            {
                lives--; // Odejmij jedno życie za błędne zgadywanie

                // Wypisuję komunikat o błędzie, jeżeli litera nie występuje w haśle
                AuthorInfo.WriteColor($"Niestety, litera '{letter}' nie występuje w haśle. Tracisz jedno życie.", ConsoleColor.Red);
                System.Threading.Thread.Sleep(600); // Mała pauza, żeby użytkownik zdążył przeczytać komunikat
            }
        }

        // Definuję metodę ProcesWordGuess, która przetwarza zgadywanie całego słowa
        private void ProcesWordGuess(string word)
        {
            // Sprawdzam, czy podane słowo jest równe docelowemu słowu
            if (word == targetWord)
            {
                // jeżeli gracz zgadł całe słowo, dodajemy wszystkie litery do guessedLetters 
                foreach (char cha in targetWord)
                {
                    if (!guessedLetters.Contains(cha))
                    {
                        guessedLetters.Add(cha);        // Dodaj literę do odgadniętych liter
                    }
                }
            }

            // Jeżeli podane słowo jest niepoprawne - odejmujemy 2 życia graczowi - oto kara za porywcze zgadywanie
            else
            {
                lives = -2; // Ustawiam lives na -2, aby zakończyć grę jako przegraną
                AuthorInfo.WriteError("To nie jest to hasło! Za porywcze zgadywanie odejmuję Ci 2 życia!");

                System.Threading.Thread.Sleep(1500); // Mała pauza, żeby użytkownik zdążył przeczytać komunikat
            }
        }

        // Definiuję metodę CheckWin, która sprawdza, czy gracz wygrał grę
        private bool CheckWin()
        {
            // Metoda LINQ .all() sprawdzacza   czy wszystkie litery w targetWord znajdują się w guessedLetters
            return targetWord.All(c => guessedLetters.Contains(c));
        }

        // Definiuję metodę DrawInterface, która rysuje interfejs gry w konsoli
        private void DrawInterface()
        {
            // Wyświetlam nagłówek gry za pomocą klasy AuthorInfo
            AuthorInfo.DisplayHeader("Wisielec");

            // Rysuję szubienicę na podstawie liczby pozostałych żyć
            DrawSzubienica(lives);


            // Rysuję hasło z maskowaniem 

            Console.WriteLine("\nHasło do zgadnięcia: ");       // Wyświetlam tekst "Hasło do zgadnięcia: "
            Console.ForegroundColor = ConsoleColor.Yellow;      // Ustawiam kolor tekstu na żółty dla hasła
            Console.Write(" ");                                 // Dodaję odstęp przed hasłem
        }

        // Definiuję metodę DrawSzubienica, która rysuje wisielca w konsoli na podstawie liczby pozostałych żyć
        private void DrawSzubienica(int currentLives)
        {
            // Ustawiam kolor tekstu na ciemnocyjanowy
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            // Obliczam liczbę błędów na podstawie pozostałych żyć
            int mistakes = 6 - currentLives;

            // Rysuję szubienicę i wisielca w konsoli
            Console.WriteLine("  _______");
            Console.WriteLine("  |/    |");

            // Głowa wisielca 
            if (mistakes >= 1)
            {
                Console.WriteLine("  |     O");
            }
            else
            {
                Console.WriteLine("  |");
            }

            // Tułów i ramiona naszego wisielca
            if (mistakes == 2)
            {
                Console.WriteLine("  |     |");
            }
            else if (mistakes == 3)
            {
                Console.WriteLine("  |    /|");
            }
            else if (mistakes >= 4)
            {
                Console.WriteLine("  |    /|\\");
            }
            else
            {
                Console.WriteLine("  |");
            }

            // Nogi wisielca
            if (mistakes == 5)
            {
                Console.WriteLine("  |    /");
            }
            else if (mistakes >= 6)
            {
                Console.WriteLine("  |    / \\");
            }
            else
            {
                Console.WriteLine("  |");
            }

            // Podstawa szubienicy
            Console.WriteLine("  |");
            Console.WriteLine(" _|___\n");

            Console.ResetColor(); // Resetuję kolor tekstu do domyślnego

        }
    }
}
