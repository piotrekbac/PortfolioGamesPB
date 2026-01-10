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
    }
}
