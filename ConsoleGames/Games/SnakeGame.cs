using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Piotr Bacior - Portfolio Project 2026

namespace ConsoleGames.Games
{
    // Klasa implementująca grę Snake 
    public class SnakeGame
    {
        // Definiuję strukturę pomocniczą Point do reprezentacji punktów na planszy
        struct Point
        {
            // Współrzędne punktu na planszy
            public int X { get; set; }
            public int Y { get; set; }

            // Konstruktor inicjalizujący punkt z podanymi współrzędnymi
            public Point(int x, int y)
            {
                X = x;      // Współrzędna X punktu
                Y = y;      // Współrzędna Y punktu
            }
        }

        // Definiuję wyliczenie Direction do reprezentacji kierunków ruchu węża
        enum Direction { Up, Down, Left, Right }


        // Ustawienia planszy dla gry Snake

        private int width = 40;                  // Szerokość planszy gry
        private int height = 20;                 // Wysokość planszy gry
        private int score;                       // Aktualny wynik gracza
        private int speed;                       // Prędkość ruchu węża

        // Główna metoda uruchamiająca grę Snake
        public void Run()
        {

        }
    }
}
