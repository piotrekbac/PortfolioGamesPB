using ConsoleGames.Helpers;
using System;
using System.Collections.Generic;
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
            AuthorInfo.DisplayHeader("Gra w Kółko i Krzyżyk");

            // Rysuję planszę w konsoli dla pierwszego wiersza 

            Console.WriteLine("     |     |      ");
            DrawCell(0, 0);                 // Rysuję komórkę (0,0) - pierwszy wiersz, pierwsza kolumna
            Console.Write("  |  "); 
            DrawCell(0, 1);                 // Rysuję komórkę (0,1) - pierwszy wiersz, druga kolumna
            Console.Write("  |  "); 
            DrawCell(0, 2);                 // Rysuję komórkę (0,2) - pierwszy wiersz, trzecia kolumna
            Console.WriteLine();


        }
    }
}
