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
            Console.CursorVisible = false;      // Ukrywam kursor konsoli
            Console.Clear();                    // Czyści ekran konsoli

            // Inicjalizacja węża jako lista punktów na planszy, gdzie index 0 to głowa
            List<Point> snake = new List<Point>
            {
                new Point(width / 2, height / 2),       // Głowa węża na środku planszy
                new Point(width / 2, height / 2 + 1),   // Tułów węża
                new Point(width / 2, height / 2 + 2)    // Ogon węża
            };

            Direction currentDirection = Direction.Up;      // Aktualny kierunek ruchu węża
            bool gameOver = false;                          // Flaga zakończenia gry 
            score = 0;                                      // Inicjalizacja wyniku gracza
            speed = 100;                                    // Inicjalizacja początkowej prędkości ruchu węża

            Point food = GenerateFood(snake);              // Generuję początkową pozycję jedzenia na planszy
            DrawBorder();                                  // Rysuję obramowanie planszy gry

            // Główna pętla gry - w czasie rzeczywistym 
            while (!gameOver)
            {
                // Obsługuję sprawdzenie kliknięcia klawisza przez gracza - bez zatrzymywania gry
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;      // Odczytuję naciśnięty klawisz bez wyświetlania go w konsoli

                    // Zmieniam kierunek ruchu węża na podstawie naciśniętego klawisza
                    if (key == ConsoleKey.UpArrow && currentDirection != Direction.Down)
                    {
                        currentDirection = Direction.Up;        // Zmieniam kierunek ruchu na górę
                    }

                    else if (key == ConsoleKey.DownArrow && currentDirection != Direction.Up)
                    {
                        currentDirection = Direction.Down;      // Zmieniam kierunek ruchu na dół
                    }

                    else if (key == ConsoleKey.LeftArrow && currentDirection != Direction.Right)
                    {
                        currentDirection = Direction.Left;      // Zmieniam kierunek ruchu na lewo
                    }

                    else if (key == ConsoleKey.RightArrow && currentDirection != Direction.Left)
                    {
                        currentDirection = Direction.Right;     // Zmieniam kierunek ruchu na prawo
                    }

                    // Kończę grę, jeśli naciśnięto klawisz Q
                    else if (key == ConsoleKey.Q)
                    {
                        gameOver = true;                       // Kończę grę, jeśli naciśnięto klawisz Q
                    }
                }

                Point head = snake[0];      // Pobieram aktualną pozycję głowy węża

                // Aktualizuję pozycję głowy węża na podstawie aktualnego kierunku ruchu
                Point newHead = new Point(head.X, head.Y);

                // Obsluga ruchu węża w zależności od kierunku
                switch (currentDirection)
                {
                    case Direction.Up: newHead.Y--; 
                        break;          // Ruch w górę

                    case Direction.Down: newHead.Y++; 
                        break;        // Ruch w dół

                    case Direction.Left: newHead.X--;
                        break;      // Ruch w lewo

                    case Direction.Right: newHead.X++; 
                        break;     // Ruch w prawo
                }

                // Sprawdzam kolizcję ze ścianami planszy
                if (newHead.X <= 0 || newHead.X >= width || newHead.Y <= 0 || newHead.Y >= height)
                {
                    gameOver = true;      // Kończę grę, jeśli wąż uderzy w ścianę  
                    continue;             // Przechodzę do następnej iteracji pętli
                }
            }
        }

        // Definiuję metodę rysującą obramowanie planszy gry 
        private void DrawBorder()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;       // Ustawiam kolor obramowania na ciemny cyjan

            // Rysuję górną krawędź obramowania planszy
            for (int x = 0; x <= width; x++)
            {
                Console.SetCursorPosition(x, 0);                  // Ustawiam kursor na górną krawędź
                Console.Write("#");                               // Rysuję górną krawędź obramowania
                Console.SetCursorPosition(x, height);             // Ustawiam kursor na dolną krawędź
                Console.Write("#");                               // Rysuję dolną krawędź obramowania
            }

            // Rysuję boczne krawędzie obramowania planszy
            for (int y = 0; y <= height; y++)
            {
                Console.SetCursorPosition(0, y);                  // Ustawiam kursor na lewą krawędź
                Console.Write("#");                               // Rysuję lewą krawędź obramowania
                Console.SetCursorPosition(width, y);              // Ustawiam kursor na prawą krawędź
                Console.Write("#");                               // Rysuję prawą krawędź obramowania
            }

            Console.ResetColor();      // Resetuję kolor konsoli do domyślnego
        }

        // Definiuję metodę generującą pozycję jedzenia na planszy gry
        private Point GenerateFood(List<Point> snake)
        {
            Random random = new Random();        // Tworzę nową instancję generatora liczb losowych
            Point food;                          // Deklaruję zmienną do przechowywania pozycji jedzenia

            // Pętla sprawdzająca, czy jedzenie nie pojawi się na ciele węża
            do
            {
                food = new Point(random.Next(1, width), random.Next(1, height));   // Generuję losową pozycję jedzenia w obrębie planszy
            }
            while (snake.Any(p => p.X == food.X && p.Y == food.Y));      // Sprawdzam, czy jedzenie nie koliduje z ciałem węża

            return food;      // Zwracam wygenerowaną pozycję jedzenia

        }
    }
}
