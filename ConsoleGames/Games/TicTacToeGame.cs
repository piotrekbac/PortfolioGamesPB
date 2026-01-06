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
    }
}
