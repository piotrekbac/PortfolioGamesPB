using ConsoleGames.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Piotr Bacior - Portfolio Project 2026

namespace ConsoleGames.Games
{
    // Klasa reprezentująca grę w kółko i krzyżyk (Tic Tac Toe)
    public class TicTacToeGame
    {
        // Konstruktor klasy TicTacToeGame, inicjalizujący planszę i ustawiający aktualnego gracza na 'X'
        // Używam planszy 3x3 do gry w kółko i krzyżyk, będzie to tablica dwuwymiarowa typu char 

        private char[,] board;          // Plansza do gry
        private char currentPlayer;     // Aktualny gracz ('X' lub 'O')
        private int turnsCount;         // Liczba wykonanych ruchów

        // Metoda uruchamiająca grę w kółko i krzyżyk
        public void Run()
        {
            // Inicjalizuję planszę i ustawiam początkowe wartości
            board = new char[3, 3]
            {
                { '1', '2', '3' },
                { '4', '5', '6' },
                { '7', '8', '9' }
            };

            currentPlayer = 'X';        // Aktualny gracz zaczyna jako 'X'
            turnsCount = 0;             // Liczba ruchów na początku wynosi 0
            bool gameEnded = false;     // Flaga wskazująca, czy gra się zakończyła

            // Główna pętla gry
            while (!gameEnded)
            {
                DrawBoard();      // Rysuję planszę w konsoli

                // Wypisuję informacje o aktualnym graczu i proszę o wybór pola
                Console.WriteLine($"\nTura gracza: {currentPlayer}");
                Console.WriteLine("Wybierz pole (1-9) lub 'q' aby wyjść: \n");

                string input = Console.ReadLine();   // Odczytuję wejście od użytkownika

                // Sprawdzam, czy użytkownik chce zakończyć grę
                if (input.ToLower() == "q")
                {
                    return;     // Jeśli użytkownik wpisze 'q', wychodzę z gry
                }

                // Jeżeli użytkownik wpisał liczbę od 1 do 9, przetwarzam wybór pola
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 9)
                {
                    // Konwertujemy numer 1-9 na indeksy wiersza i kolumny w tablicy 2D (wiersz, kolumna)
                    // przykładowo: wybór 1 odpowiada (0,0), wybór 5 odpowiada (1,1), wybór 9 odpowiada (2,2)
                    // Dla 5: (5-1)/3 = 1 (wiersz), (5-1)%3 = 1 (kolumna) zatem (1,1) 

                    int row = (choice - 1) / 3;    // Obliczam wiersz na podstawie wyboru
                    int col = (choice - 1) % 3;    // Obliczam kolumnę na podstawie wyboru

                    // Sprawdzam, czy wybrane pole jest już zajęte przez 'X' lub 'O'
                    if (board[row, col] != 'X' && board[row, col] != 'O')
                    {
                        board[row, col] = currentPlayer;   // Ustawiam znak aktualnego gracza na wybranym polu
                        turnsCount++;                       // Zwiększam licznik ruchów

                        // Sprawdzam, czy aktualny gracz wygrał grę
                        if (CheckWin())
                        {
                            DrawBoard();      // Rysuję planszę w konsoli

                            AuthorInfo.WriteColor($"\nGracz {currentPlayer} wygrał! Gratulacje!\n", ConsoleColor.Green);
                            gameEnded = true;    // Ustawiam flagę zakończenia gry na true
                        }

                    }
                }
            }
        }

        // Definiuję metodę do kolorowania X i O na planszy
        private void DrawCell(int row, int col)
        {
            // Pobieram wartość z planszy i ustawiam kolor w zależności od tego, czy to 'X', 'O' czy puste pole
            char val = board[row, col];

            // Jeżeli wartość to 'X', ustawiam kolor na ciemnoczerwony
            if (val == 'X')
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }

            // Jeżeli wartość to 'O', ustawiam kolor na ciemnoniebieski
            else if (val == 'O')
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }

            // W przeciwnym razie ustawiam kolor na szary dla pustych pól
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            // Wyświetlam wartość na planszy
            Console.Write(val);

            // Resetuję kolor konsoli do domyślnego
            Console.ResetColor();
        }

        // Definiuję metodę do rysowania planszy w konsoli 
        private void DrawBoard()
        {
            // Wywołuję metodę DisplayHeader z klasy AuthorInfo, aby wyświetlić nagłówek gry
            AuthorInfo.DisplayHeader("Gra w Kółko i Krzyżyk");

            // Rysuję planszę w konsoli dla pierwszego wiersza 

            Console.WriteLine("     |     |      ");
            DrawCell(0, 0);                 // Rysuję komórkę (0,0) - pierwszy wiersz, pierwsza kolumna
            Console.Write("  |  "); 
            DrawCell(0, 1);                 // Rysuję komórkę (0,1) - pierwszy wiersz, druga kolumna
            Console.Write("  |  "); 
            DrawCell(0, 2);                 // Rysuję komórkę (0,2) - pierwszy wiersz, trzecia kolumna
            Console.WriteLine();


            // Rysuję planszę w konsoli dla drugiego wiersza 

            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");

            DrawCell(1, 0);                 // Rysuję komórkę (1,0) - drugi wiersz, pierwsza kolumna
            Console.WriteLine("  |  ");
            DrawCell(1, 1);                 // Rysuję komórkę (1,1) - drugi wiersz, druga kolumna
            Console.WriteLine("  |  ");
            DrawCell(1, 2);                 // Rysuję komórkę (1,2) - drugi wiersz, trzecia kolumna
            Console.WriteLine();


            // Rysuję planszę w konsoli dla trzeciego wiersza

            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            DrawCell(2, 0);                 // Rysuję komórkę (2,0) - trzeci wiersz, pierwsza kolumna
            Console.Write("  |  ");
            DrawCell(2, 1);                 // Rysuję komórkę (2,1) - trzeci wiersz, druga kolumna
            Console.Write("  |  ");
            DrawCell(2, 2);                 // Rysuję komórkę (2,2) - trzeci wiersz, trzecia kolumna
            Console.WriteLine();
        }

        // Definiuję metodę do sprawdzania, czy aktualny gracz wygrał grę
        private bool CheckWin()
        {
            // Sprawdzam wiersze pod kątem zwycięstwa 
            for (int i = 0; i < 3; i++)
            {
                // Sprawdzam wiersze - czy wszystkie trzy komórki w wierszu należą do aktualnego gracza
                if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer)
                {
                    return true;        // Jeśli tak, zwracam true - aktualny gracz wygrał
                }
            }

            // Sprawdzam kolumny pod kątem zwycięstwa
            for (int i = 0; i < 3; i++)
            {
                // Sprawdzam kolumny - czy wszystkie trzy komórki w kolumnie należą do aktualnego gracza
                if (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer)
                { 
                    return true;        // Jeśli tak, zwracam true - aktualny gracz wygrał
                }
            }


            // Sprawdzam pierwszą przekątną pod kątem zwycięstwa
            if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer)
            {
                return true;    // Zwycięstwo na przekątnej od lewego górnego do prawego dolnego rogu
            }

            // Sprawdzenie drugiej przekątnej pod kątem zwycięstwa
            if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer)
            {
                return true;    // Zwycięstwo na przekątnej od prawego górnego do lewego dolnego rogu
            }


            return false;   // Brak zwycięstwa w przypadku żadnej z powyższych sytuacji
        }
    }
}
