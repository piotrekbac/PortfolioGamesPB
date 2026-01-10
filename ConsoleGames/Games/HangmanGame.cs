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
            Random random = new Random();
        }
    }
}
