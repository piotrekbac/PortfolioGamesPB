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

        private List<Point> obstacles;           // Lista obiektów na planszy gry
        private Point? bonusFood;                // Pozycja bonusowego jedzenia na planszy gry - możliwy null
        private int bonusFoodTimer;              // Licznik czasu dla bonusowego jedzenia

        private Point? rottenFood;               // Pozycja zepsutego jedzenia na planszy gry - możliwy null
        private string notification = "";        // Komunikat wyświetlany graczowi
        private int notificationTimer = 0;       // Licznik czasu dla komunikatu

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


            // Inicjalizacja przeszkód i bonusu na planszy gry

            obstacles = new List<Point>();                  // Inicjalizacja listy obiektów na planszy gry
            GenerateObstacles(5, snake);                    // Generuję 5 przeszkód na planszy gry
            bonusFood = null;                               // Inicjalizacja braku bonusowego jedzenia na planszy
            bonusFoodTimer = 0;                             // Ustawiam licznik czasu bonusowego jedzenia na 0

            rottenFood = null;                              // Inicjalizacja braku zepsutego jedzenia na planszy

            Random random = new Random();                   // Tworzę nową instancję generatora liczb losowych
            Point food = GenerateFood(snake);               // Generuję początkową pozycję jedzenia na planszy

            DrawBorder();                                   // Rysuję obramowanie planszy gry
            DrawObstacles();                                // Rysuję przeszkody na planszy gry

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

                // Pobieram aktualną pozycję głowy węża
                Point head = snake[0];      

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

                // Sprawdzamy kolizję z przeszkodami na planszy gry 
                if (obstacles.Any( o => o.X == newHead.X && o.Y == newHead.Y))
                {
                    gameOver = true;      // Kończę grę, jeśli wąż uderzy w przeszkodę
                    continue;             // Przechodzę do następnej iteracji pętli
                }

                // Inicjujemy ruch węża 
                snake.Insert(0, newHead);      // Dodaję nową głowę węża na początku listy

                bool shouldRemoveTail = true;    // Flaga wskazująca, czy należy usunąć ogon węża - domyślnie usuwamy ogon

                // Definiuję logikę zjedzenia jedzenia przez węża
                if (newHead.X == food.X && newHead.Y == food.Y)
                {
                    score += 10;                     // Zwiększam wynik gracza o 10 punktów
                    shouldRemoveTail = false;        // Ustawiam flagę usunięcia ogona na false (wąż rośnie) 
                    food = GenerateFood(snake);      // Generuję nową pozycję jedzenia na planszy

                    // Przyspieszam ruch węża po zjedzeniu jedzenia
                    if (speed > 30)
                    {
                        speed -= 2;         // Zwiększam prędkość ruchu węża, po zjedzeniu jedzenia
                    }

                    ShowNotification("Zjedzono jedzenie! +10 pkt");   // Wyświetlam powiadomienie o zjedzeniu jedzenia
                }

                // Obsługa logiki zjedzenia bonusowego jedzenia przez węża
                else if (bonusFood.HasValue && newHead.X == bonusFood.Value.X && newHead.Y == bonusFood.Value.Y)
                {
                    score += 50;                     // Zwiększam wynik gracza o 50 punktów za zjedzenie bonusowego jedzenia
                    shouldRemoveTail = false;        // Ustawiam fkagę usunięcia ogona na false (wąż rośnie)
                    bonusFood = null;                // Usuwam bonusowe jedzenie z planszy

                    ShowNotification("Zjedzono BONUS! +50 pkt");   // Wyświetlam powiadomienie o zjedzeniu bonusowego jedzenia
                }

                // Obsługa logiki zjedzenia zepsutego jedzenia przez węża
                else if (rottenFood.HasValue && newHead.X == rottenFood.Value.X && newHead.Y == rottenFood.Value.Y)
                {
                    score -= 20;                     // Zmniejszam wynik gracza o 20 punktów za zjedzenie zepsutego jedzenia
                    rottenFood = null;               // Usuwam zepsute jedzenie z planszy
                    ShowNotification("Zjedzono TRUCIZNĘ! -20 pkt");   // Wyświetlam powiadomienie o zjedzeniu zepsutego jedzenia

                    // Efekt trucizny - wąż traci długość
                    if (snake.Count > 1)
                    {
                        ClearTail(snake);                   // Czyścimy ogon węża z planszy  
                        snake.RemoveAt(snake.Count - 1);    // Usuwam ogon węża z listy (wąż skraca się)
                    }
                }

                // Usuwanie ogona węża - skutek ruchu węża 
                if (shouldRemoveTail)
                {
                    ClearTail(snake);                        // Czyścimy ogon węża z planszy
                    snake.RemoveAt(snake.Count - 1);         // Usuwam ogon węża z listy
                }

                ManageBonusFood(random, snake, food);               // Zarządzam logiką bonusowego jedzenia na planszy gry
                ManageRottenFood(random, snake, food);              // Zarządzam logiką zepsutego jedzenia na planszy gry


                // Rysuję wężą na planszy gry

                DrawPixel(newHead.X, newHead.Y, "O", ConsoleColor.Green);    // Rysujsę nową głowę węża na planszy
                DrawPixel(head.X, head.Y, "o", ConsoleColor.DarkGreen);      // Rysuję tułów węża na planszy

                // Rysuję jedzenie na planszy gry
                DrawPixel(food.X, food.Y, "@", ConsoleColor.Red);            // Rysuję jedzenie na planszy


                // Rysuję bonusowe jedzenie na planszy gry, jeśli istnieje
                if (bonusFood.HasValue)
                {
                    DrawPixel(bonusFood.Value.X, bonusFood.Value.Y, "$", ConsoleColor.Yellow);   // Rysuję bonusowe jedzenie na planszy
                }

                // Rysuję zepsute jedzenie na planszy gry, jeśli istnieje
                if (rottenFood.HasValue)
                {
                    DrawPixel(rottenFood.Value.X, rottenFood.Value.Y, "X", ConsoleColor.Magenta);   // Rysuję zepsute jedzenie na planszy
                }

                // Obsługuję interfejs użytkownika gry - UI - podział na 2 linie

                // Linia 1 - Statystyki stałe gry
                Console.SetCursorPosition(0, height + 1);                    // Ustawiam kursor na początku linii 1 pod planszą gry
                Console.ForegroundColor = ConsoleColor.White;                // Ustawiam kolor tekstu na biały

                Console.Write($"Wynik: {score} | Prędkość: {110 - speed} | Długość: {snake.Count}   ");   // Wyświetlam wynik, prędkość i długość węża


                // Linia 2 - Powiadomienia dynamiczne oraz timer
                Console.SetCursorPosition(0, height + 2);                    // Ustawiam kursor na początku linii 2 pod planszą gry`
                Console.Write(new string(' ', 40));                          // Czyścimy linię powiadomień spacjami, aby stare napisany nie zostawały

                // Sprawdzam, czy licznik powiadomień jest większy od 0 i wyświetlam odpowiednie powiadomienie
                if (notificationTimer > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;                // Ustawiam kolor tekstu na cyjanowy
                    Console.Write(" >> " + notification);                       // Wyświetlam powiadomienie z prefiksem " >> " - dla urozmaicenia
                    notificationTimer--;                                        // Zmniejszam licznik czasu powiadomienia
                }

                // Wyświetlam licznik w sytuacji, gdy nie ma powiadomienia
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;              // Ustawiam kolor tekstu na żółty
                    Console.Write($"Czas bonusu: {bonusFoodTimer}   ");         // Wyświetlam licznik czasu bonusowego jedzenia
                }

                // Sprawdzam prędkość ruchu węża i wprowadzam opóźnienie na podstawie prędkości
                if (speed > 0)
                {
                    Thread.Sleep(speed);      // Wprowadzam opóźnienie na podstawie prędkości ruchu węża
                }
            }

            // Obsługa ekranu końcowego

            int messageY = height + 4;        // Pozycja Y dla komunikatu końcowego - przesuwamy w dół, aby nie zasłonić interfejsu gry UI

            Console.SetCursorPosition(0, messageY);                                                         // Ustawiam kursor na początku linii komunikatu końcowego
            AuthorInfo.WriteColor("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=", ConsoleColor.DarkBlue);      // Rysuję dekoracyjną linię

            Console.SetCursorPosition(0, messageY + 1);                                                     // Ustawiam kursor na początku linii komunikatu końcowego
            AuthorInfo.WriteColor("KONIEC GRY! Dziękujemy za grę w Snake!", ConsoleColor.DarkRed);          // Wyświetlam komunikat końcowy gry

            Console.SetCursorPosition(0, messageY + 2);                                                     // Ustawiam kursor na początku linii komunikatu końcowego
            AuthorInfo.WriteColor($"Twój końcowy wynik to: {score} punktów.", ConsoleColor.Yellow);         // Wyświetlam końcowy wynik gracza
        }

        // Definiuję metodę wyświetlającą powiadomienia dla gracza - metoda pomocnicza
        private void ShowNotification(string message)
        {
            notification = message;          // Ustawiam komunikat powiadomienia
            notificationTimer = 20;          // Ustawiam licznik czasu powiadomienia na 20 klatek
        }

        // Definiuję metodę czyszczącą ogon węża - metoda pomocnicza
        private void ClearTail(List<Point> snake)
        {
            Point tail = snake[snake.Count - 1];            // Pobieram pozycję ogona węża
            Console.SetCursorPosition(tail.X, tail.Y);      // Ustawiam kursor na pozycji ogona węża
            Console.Write(" ");                             // Czyścimy znak ogona z planszy
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
                if (random.Next(0,100) == 0)    // ustawiamy 1% szansy na pojawienie się bonusowego jedzenia
                {
                    bonusFood = GenerateValidPoint(snake, normalFood);      // Generuję nową pozycję bonusowego jedzenia na planszy
                    bonusFoodTimer = 50;                                    // Ustawiam licznik czasu bonusowego jedzenia na 50 klatek
                }

                // Jeżeli bonusowe jedzenie istnieje, to zmniejszamy jego licznik czasu
                else
                {
                    bonusFoodTimer--;      // Zmniejszam licznik czasu bonusowego jedzenia

                    // Sprawdzam, czy licznik czasu bonusowego jedzenia osiągnął 0
                    if (bonusFoodTimer <= 0)
                    {
                        ClearPoint(rottenFood.Value);    // Czyścimy zepsute jedzenie z planszy
                        bonusFood = null;                // Usuwam bonusowe jedzenie z planszy
                    }
                }
            }
        }

        // Definiuję metodę zarządzającą logiką zepsutego jedzenia na planszy gry
        private void ManageRottenFood(Random random, List<Point> snake, Point normalFood)
        {
            // Warunek obsługujący logikę, gdy zepsutego jedzenia nie ma na planszy
            if (rottenFood == null)
            {
                // Trucizna pojawia się częściej niż bonus 
                if (random.Next(0, 80) == 0)
                {
                    rottenFood = GenerateValidPoint(snake, normalFood);    // Generuję nową pozycję zepsutego jedzenia na planszy   

                    // nie ma licznika czasu dla zepsutego jedzenia - pozostaje na planszy do momentu zjedzenia lub zakończenia gry
                }
            }

            // Warunek obsługujący logikę, gdy zepsute jedzenie jest na planszy
            else
            {
                // Trucizna znika sama po jakimś czasie
                if (random.Next(0, 100) == 0)
                {
                    ClearPoint(rottenFood.Value);     // Czyścimy zepsute jedzenie z planszy
                    rottenFood = null;                // Usuwam zepsute jedzenie z planszy
                }
            }
        }

        // Definiuję metodę czyszczącą punkt na planszy gry - metoda pomocnicza
        private void ClearPoint(Point p)
        {
            Console.SetCursorPosition(p.X, p.Y);    // Ustawiam kursor na podanej pozycji
            Console.Write(" ");                     // Czyścimy znak z planszy
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

        // Definiuję metodę rysującą przeszkody na planszy gry
        private void DrawObstacles()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;      // Ustawiam kolor przeszkód na ciemnoszary

            // Rysuję każdą przeszkodę na planszy gry
            foreach (var obj in obstacles)
            {
                Console.SetCursorPosition(obj.X, obj.Y);      // Ustawiam kursor na pozycji przeszkody
                Console.Write("#");                           // Rysuję przeszkodę na planszy
            }

            Console.ResetColor();      // Resetuję kolor konsoli do domyślnego
        }

        // Definiuję metodę generującą przeszkody na planszy gry
        private void GenerateObstacles(int count, List<Point> snake)
        {
            Random random = new Random();        // Tworzę nową instancję generatora liczb losowych

            // Generuję określoną liczbę przeszkód na planszy gry
            for (int i = 0; i < count; i++)
            {
                Point obstacle;                  // Deklaruję zmienną do przechowywania pozycji przeszkody

                // Pętla sprawdzająca, czy przeszkoda nie pojawi się na ciele węża lub na innych przeszkodach
                do
                {
                    obstacle = new Point(random.Next(2, width - 1), random.Next(2, height - 1));   // Generuję losową pozycję przeszkody w obrębie planszy
                }

                while (snake.Any(s => s.X == obstacle.X && s.Y == obstacle.Y) || obstacles.Any(o => o.X == obstacle.X && o.Y == obstacle.Y));
                {
                    obstacles.Add(obstacle);      // Dodaję przeszkodę do listy przeszkód (gdy znaleziono wolne miejsce)
                }
            }
        }

        // Definiuję metodę generującą pozycję jedzenia na planszy gry
        private Point GenerateFood(List<Point> snake)
        {
            Random random = new Random();        // Tworzę nową instancję generatora liczb losowych
            Point newFood;                          // Deklaruję zmienną do przechowywania pozycji jedzenia

            // Pętla sprawdzająca, czy jedzenie nie pojawi się na ciele węża
            do
            {
                newFood = new Point(random.Next(1, width), random.Next(1, height));   // Generuję losową pozycję jedzenia w obrębie planszy
            }

            // Sprawdzam, czy jedzenie nie koliduje z ciałem węża oraz na przeszkodach
            while (snake.Any(p => p.X == newFood.X && p.Y == newFood.Y) || obstacles.Any(o => o.X == newFood.X && o.Y == newFood.Y));
            {
                return newFood;      // Zwracam wygenerowaną pozycję jedzenia
            }
        }

        // Definiuję metodę generującą ważny punkt na planszy gry, unikając kolizji z wężem i normalnym jedzeniem
        private Point GenerateValidPoint(List<Point> snake, Point normalFood)
        {
            Random random = new Random();        // Tworzę nową instancję generatora liczb losowych
            Point p;                             // Deklaruję zmienną do przechowywania pozycji punktu - przechowuję wartość w zmiennej o nazwie "p"

            // Pętla sprawdzająca, czy punkt nie pojawi się na ciele węża lub na normalnym jedzeniu
            do
            {
                p = new Point(random.Next(1, width), random.Next(1,height));   // Generuję losową pozycję punktu w obrębie planszy
            }


            // Dopóki punkt koliduje z ciałem węża, przeszkodami, normalnym jedzeniem, bonusowym jedzeniem lub zepsutym jedzeniem

            while (snake.Any(s => s.X == p.X && s.Y == p.Y) ||                                          // Sprawdzam kolizję z ciałem węża
                   obstacles.Any(o => o.X == p.X && o.Y == p.Y) ||                                      // Sprawdzam kolizję z przeszkodami
                   (p.X == normalFood.X && p.Y == normalFood.Y) ||                                      // Sprawdzam kolizję z normalnym jedzeniem
                   (bonusFood.HasValue && p.X == bonusFood.Value.X && p.Y == bonusFood.Value.Y) ||      // Sprawdzam kolizję z bonusowym jedzeniem
                   (rottenFood.HasValue && p.X == rottenFood.Value.X && p.Y == rottenFood.Value.Y));    // Sprawdzam kolizję ze zepsutym jedzeniem
            {
                return p;      // Zwracam wygenerowaną ważną pozycję punktu
            }

        }
    }
}