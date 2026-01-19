using ConsoleGames.Helpers;
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

        private List<Point> objects;             // Lista obiektów na planszy gry
        private Point? bonusFood;                // Pozycja bonusowego jedzenia na planszy gry - możliwy null
        private int bonusFoodTimer;              // Licznik czasu dla bonusowego jedzenia


        // Główna metoda uruchamiająca grę Snake
        public void Run()
        {
            Console.CursorVisible = false;      // Ukrywam kursor konsoli
            Console.Clear();                    // Czyści ekran konsoli

            // Inicjalizacja węża jako lista punktów na planszy, gdzie index 0 to głowa
            List<Point> snake = new List<Point>
            {
                new Point(width / 2, height / 2),       // Głowa węża
                new Point(width / 2, height / 2 + 1),   // Tułów węża
                new Point(width / 2, height / 2 + 2)    // Ogon węża
            };

            Direction currentDirection = Direction.Up;      // Aktualny kierunek ruchu węża
            bool gameOver = false;                          // Flaga zakończenia gry 
            score = 0;                                      // Inicjalizacja wyniku gracza
            speed = 100;                                    // Inicjalizacja początkowej prędkości ruchu węża


            objects = new List<Point>();                    // Inicjalizacja listy obiektów na planszy gry


            Point food = GenerateFood(snake);               // Generuję początkową pozycję jedzenia na planszy
            DrawBorder();                                   // Rysuję obramowanie planszy gry

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
                        break;          // Ruch w dół

                    case Direction.Left: newHead.X--;
                        break;          // Ruch w lewo

                    case Direction.Right: newHead.X++; 
                        break;          // Ruch w prawo
                }

                // Sprawdzam kolizcję ze ścianami planszy
                if (newHead.X <= 0 || newHead.X >= width || newHead.Y <= 0 || newHead.Y >= height)
                {
                    gameOver = true;      // Kończę grę, jeśli wąż uderzy w ścianę  
                    continue;             // Przechodzę do następnej iteracji pętli
                }

                // Sprawdzmmy czy głowa nie wchodzi w ciało - pomijay ostatni element (ogon) bo on się przesuwa
                if (snake.Take(snake.Count - 1).Any(p => p.X == newHead.X && p.Y == newHead.Y))
                {
                    gameOver = true;      // Kończę grę, jeśli wąż uderzy w siebie
                    continue;             // Przechodzę do następnej iteracji pętli
                }

                // Inicjujemy ruch węża 
                snake.Insert(0, newHead);      // Dodaję nową głowę węża na początku listy

                // Definiuję logikę zjedzenia jedzenia przez węża
                if (newHead.X == food.X && newHead.Y == food.Y)
                {
                    score += 10;                     // Zwiększam wynik gracza o 10 punktów
                    food = GenerateFood(snake);      // Generuję nową pozycję jedzenia na planszy

                    // Przyspieszam ruch węża po zjedzeniu jedzenia
                    if (speed > 30)
                    {
                        speed = -2;         // Zwiększam prędkość ruchu węża, po zjedzeniu jedzenia
                    }
                }

                // Jeśli wąż nie zjadł jedzenia, usuwam ostatni segment (ogon) naszego węża
                else
                {
                    Point tail = snake[snake.Count - 1];            // Pobieram pozycję ogona węża
                    Console.SetCursorPosition(tail.X, tail.Y);      // Ustawiam kursor na pozycji ogona węża
                    Console.Write(" ");                             // Kasujemy ogon węża z ekranu 
                    snake.RemoveAt(snake.Count - 1);                // Usuwam ogon węża z listy
                }

                // Rysuję głowę węża na nowej pozycji
                Console.SetCursorPosition(newHead.X, newHead.Y);    // Ustawiam kursor na nowej pozycji głowy węża
                Console.ForegroundColor = ConsoleColor.Green;       // Ustawiam kolor głowy węża na zielony
                Console.Write("O");                                 // Rysuję głowę węża

                //Rysuję ciało węża na poprzedniej pozycji głowy
                Console.SetCursorPosition(head.X, head.Y);          // Ustawiam kursor na pozycji poprzedniej głowy węża
                Console.ForegroundColor = ConsoleColor.DarkGreen;   // Ustawiam kolor tułowia węża na ciemnozielony
                Console.Write("o");                                 // Rysuję tułów węża

                // Rysuję jedzenie na planszy
                Console.SetCursorPosition(food.X, food.Y);          // Ustawiam kursor na pozycji jedzenia
                Console.ForegroundColor = ConsoleColor.Red;         // Ustawiam kolor jedzenia na czerwony
                Console.Write("@");                                 // Rysuję jedzenie

                Console.SetCursorPosition(0, height + 1);          // Ustawiam kursor poniżej planszy gry
                Console.ForegroundColor = ConsoleColor.White;      // Ustawiam kolor tekstu na biały

                // Wyświetlam aktualny wynik gracza
                Console.Write($"Wynik: {score} (Prędkość: {110 - speed})");            
                
                Thread.Sleep(speed);      // Opóźniam kolejną iterację pętli na podstawie prędkości ruchu węża
            }

            Console.Clear();                    // Czyści ekran konsoli po zakończeniu gry
            Console.CursorVisible = true;       // Przywracam widoczność kursora konsoli

            // Wyświetlam komunikat o zakończeniu gry
            AuthorInfo.WriteColor("\nKoniec gry", ConsoleColor.DarkRed);

            // Wyświetlam końcowy wynik gracza
            Console.WriteLine($"Twój wynik końcowy: {score}\n");

            // Wyświetlam instrukcję powrotu do menu głównego
            AuthorInfo.WriteColor("Naciśnij dowolny klawisz, aby wrócić do menu głównego...", ConsoleColor.Gray);

            Console.ReadKey();      // Czekam na naciśnięcie dowolnego klawisza przez gracza
        }

        // Definiuję metodę rysującą pojedynczy piksel na planszy gry - metoda pomocnicza
        private void DrawPixel(int x, int y, string symbol, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);      // Ustawiam kursor na podanej pozycji
            Console.ForegroundColor = color;      // Ustawiam kolor tekstu na podany kolor
            Console.Write(symbol);                // Rysuję podany symbol na planszy
            Console.ResetColor();                 // Resetuję kolor konsoli do domyślnego
        }

        // Definiuję metodę zarządzającą logiką bonusowego jedzenia na planszy gry
        private void ManageBonusFood(Random random, List<Point> snake, Point normalFood)
        {
            // Jeżeli bonusowego jedzenia nie ma, to mamy 2% szany na jego pojawienie się w każdej klatce, na planszy
            if (bonusFood == null)
            {
                // Obsługujemy 1 na 50 szansę na pojawienie się bonusowego jedzenia - 2% szans
                if (random.Next(0, 50) == 0)
                {
                    bonusFood = GenerateFood(snake);      // Generuję pozycję bonusowego jedzenia na planszy

                    // Musimy upewnić się, że bonus nie wpadł na normalne jedzenie 
                    while (bonusFood.Value.X == normalFood.X && bonusFood.Value.Y == normalFood.Y)
                    {
                        bonusFood = GenerateFood(snake);  // Upewniam się, że bonusowe jedzenie nie koliduje z normalnym jedzeniem
                    }

                    bonusFoodTimer = 40;    // Ustawiam czas istnienia bonusowego jedzenia na 40 klatek
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
