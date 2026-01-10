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
        }
    }
}
