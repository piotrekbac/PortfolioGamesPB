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
            board = new char[3, 3];
            {
                { '1', '2', '3' },
                { '4', '5', '6' },
                { '7', '8', '9' }
            };

            currentPlayer = 'X';        // Aktualny gracz zaczyna jako 'X'
            turnsCount = 0;             // Liczba ruchów na początku wynosi 0
            bool gameEnded = false;     // Flaga wskazująca, czy gra się zakończyła
        }
    }
}
