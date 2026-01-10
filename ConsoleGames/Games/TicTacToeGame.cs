using ConsoleGames.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Uwzględeiamy następujące przestrzenie nazw w celu obsługi ruchów komputera i opóźnień
using System.Collections.Generic;
using System.Threading;

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
        private bool isSinglePlayer;    // Flaga wskazująca, czy gra jest jednoosobowa (przeciwko komputerowi)

        // Metoda uruchamiająca grę w kółko i krzyżyk
        public void Run()
        {
            // Ustawiam tryb gry (jednoosobowy lub dwuosobowy)
            if (!SetupGameMode())
            {
                return; // Użytkownik zdecydował się wyjść z wyboru trybu gry
            }

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

                // sprawdzam, czy teraz ruch ma komputer czy gracz (tylko w trybie jednoosobowym - gracz vs komputer)
                if (isSinglePlayer && currentPlayer == 'O')
                {
                    Console.WriteLine($"Tura komputera (O)...");
                    PerformComputerMove();      // Wykonuję ruch komputera
                }

                // obsługujemy ruch człowieka
                else
                {
                    Console.WriteLine($"\nTura gracza: {currentPlayer}");
                    Console.WriteLine("Wybierz pole (1-9) lub naciśnij klawisz 'q' aby wyjść: ");

                    // Logika ruchu gracza jest w osobnej metodzie PerformPlayerMove
                    if (!PerformPlayerMove())
                    {
                        return; // Użytkownik zdecydował się wyjść z gry
                    }
                }

                // Sprawdzam, czy aktualny gracz wygrał grę
                if (CheckWin())
                {
                    // Rysuję planszę 
                    DrawBoard();

                    // Wyświetlam komunikat o zwycięstwie komputera lub gracza
                    if (isSinglePlayer && currentPlayer == 'O')
                    {
                        AuthorInfo.WriteColor("\nKomputer (O) wygrał! Spróbuj ponownie.\n", ConsoleColor.Red);
                    }

                    // Jeśli wygrał człowiek to wyświetlam komunikat o zwycięstwie gracza
                    else
                    {
                        AuthorInfo.WriteColor($"\nBRAWO! Wygrywa gracz: {currentPlayer}\n", ConsoleColor.Green);
                    }

                    gameEnded = true;   // Ustawiam flagę zakończenia gry na true
                }

                // Sprawdzam, czy gra zakończyła się remisem (wszystkie pola zajęte, brak zwycięzcy)
                else if (turnsCount == 9)
                {
                    // Rysuję planszę
                    DrawBoard();

                    // Wyświetlam komunikat o remisie
                    AuthorInfo.WriteColor("\nRemis! Nikt nie wygrał.\n", ConsoleColor.Yellow);

                    gameEnded = true;   // Ustawiam flagę zakończenia gry na true
                }

                // Zmiana gracza na następnego, jeśli gra się nie zakończyła
                else
                {
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';   // Zmieniam aktualnego gracza na następnego
                }
            }

            // Po zakończeniu gry wyświetlam komunikat i czekam na naciśnięcie klawisza przez użytkownika
            Console.WriteLine("\nNaciśnij dowolny klawisz, aby wrócić do menu...");

            Console.ReadKey();   // Czekam na naciśnięcie klawisza przez użytkownika przed powrotem do menu
        }
        

        // Definiuję metodę do ustawiania trybu gry
        private bool SetupGameMode()
        {
            AuthorInfo.DisplayHeader("Wybór Trybu Gry w Kółko i Krzyżyk");
            Console.WriteLine("Wybierz tryb gry:");
            Console.WriteLine("1. Gra jednoosobowa (przeciwko komputerowi)");
            Console.WriteLine("2. Gra dwuosobowa (gracz vs gracz)");
            Console.Write("\nTwój wybór (1 lub 2): ");

            // Główna pętla wyboru trybu gry
            while (true)
            {
                string input = Console.ReadLine();   // Odczytuję wybór od użytkownika

                // Sprawdzam wybór użytkownika i ustawiam tryb gry
                if (input == "1")
                {
                    isSinglePlayer = true;    // Ustawiam tryb jednoosobowy
                    return true;              // Zwracam true, aby kontynuować grę
                }
                else if (input == "2")
                {
                    isSinglePlayer = false;   // Ustawiam tryb dwuosobowy
                    return true;              // Zwracam true, aby kontynuować grę
                }
                else if (input.ToLower() == "q")
                {
                    return false;   // Użytkownik zdecydował się wyjść z wyboru trybu gry
                }
                else
                {
                    AuthorInfo.WriteError("Nieprawidłowy wybór. Wybierz 1, 2 lub q: ");   // Komunikat o nieprawidłowym wyborze
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

        // Definiuję metodę do wykonywania ruchu gracza
        private bool PerformPlayerMove()
        {
            // Główna pętla gry
            while (true)
            {
                string input = Console.ReadLine();   // Odczytuję wejście od użytkownika

                // Sprawdzam, czy użytkownik chce zakończyć grę
                if (input.ToLower() == "q")
                {
                    return false;     // Jeśli użytkownik wpisze 'q', wychodzę z gry
                }

                // Jeżeli użytkownik nie wpisał liczby od 1 do 9, wyświetlam komunikat o błędzie
                else
                {
                    AuthorInfo.WriteError("Nieprawidłowy wybór. To pole jest już zajęte. Wybierz pole od 1 do 9 lub 'q' aby wyjść.");   // Komunikat o nieprawidłowym wyborze
                }

                // Jeżeli użytkownik wpisał liczbę od 1 do 9, przetwarzam wybór pola
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 9)
                {
                    int row = (choice - 1) / 3;    // Obliczam wiersz na podstawie wyboru
                    int col = (choice - 1) % 3;    // Obliczam kolumnę na podstawie wyboru

                    // Sprawdzam, czy ruch jest prawidłowy
                    if (IsValidMove(row, col))
                    {
                        board[row, col] = currentPlayer;    // Ustawiam znak aktualnego gracza na wybranym polu
                        turnsCount++;                       // Zwiększam licznik ruchów
                        return true;                        // Ruch wykonany pomyślnie
                    }

                    // Jeśli pole jest już zajęte, wyświetlam komunikat o błędzie
                    else
                    {
                        AuthorInfo.WriteError("To pole jest już zajęte. Wybierz inne pole.");   // Komunikat o zajętym polu
                    }
                }

                // Jeśli użytkownik wpisał nieprawidłowy wybór, wyświetlam komunikat o błędzie
                else
                {
                    AuthorInfo.WriteError("Nieprawidłowy wybór. Wybierz pole od 1 do 9 lub 'q' aby wyjść.");   // Komunikat o nieprawidłowym wyborze
                }
            }
        }

        // Definiuję metodę do wykonywania ruchu komputera 
        private void PerformComputerMove()
        {
            List<int> availableMoves = new List<int>();   // Lista dostępnych ruchów

            // definiuję zmienną dla licznika ruchów i zapisuję ją pod nazwą "counter"
            int counter = 1;

            // Przeglądam planszę i dodaję dostępne ruchy do listy
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // jeżeli pole nie jest zajęte przez 'X' lub 'O', dodaję je do listy dostępnych ruchów
                    if (board[i,j] != 'X' && board[i,j] != 'O')
                    {
                        availableMoves.Add(counter);   // Dodaję numer pola do listy dostępnych ruchów
                    }

                    counter++;   // Zwiększam licznik ruchów
                }
            }

            // Wybieram losowy ruch z dostępnych ruchów 
            Random random = new Random();                           // Tworzę instancję generatora liczb losowych
            int randomIndex = random.Next(availableMoves.Count);    // Wybieram losowy indeks z listy dostępnych ruchów
            int selectedMove = availableMoves[randomIndex];         // Pobieram wybrany ruch na podstawie losowego indeksu

            int row = (selectedMove - 1) / 3;    // Obliczam wiersz na podstawie wybranego ruchu
            int col = (selectedMove - 1) % 3;    // Obliczam kolumnę na podstawie wybranego ruchu

            board[row, col] = 'O';     // Komputer zawsze gra jako 'O'
            turnsCount++;              // Zwiększam licznik ruchów
        }

        // Definiuję metodę do sprawdzania, czy ruch jest prawidłowy
        private bool IsValidMove(int row, int col)
        {
            return board[row, col] != 'X' && board[row, col] != 'O';   // Sprawdzam, czy pole nie jest zajęte
        }

        // Definiuję metodę do rysowania planszy w konsoli 
        private void DrawBoard()
        {
            // Wywołuję metodę DisplayHeader z klasy AuthorInfo, aby wyświetlić nagłówek gry
            AuthorInfo.DisplayHeader("Gra w Kółko i Krzyżyk");

            // defnuję zmienną trybu gry na podstawie wartości isSinglePlayer 
            string modeInfo = isSinglePlayer ? "Tryb jednoosobowy (przeciwko komputerowi)" : "Tryb dwuosobowy (gracz vs gracz)";

            Console.WriteLine($"\nWybrałeś tryb: {modeInfo}\n");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n");

            // Rysuję planszę w konsoli dla pierwszego wiersza 

            Console.WriteLine("     |     |      ");
            Console.Write("  ");            
            DrawCell(0, 0);                 // Rysuję komórkę (0,0) - pierwszy wiersz, pierwsza kolumna
            Console.Write("  |  "); 
            DrawCell(0, 1);                 // Rysuję komórkę (0,1) - pierwszy wiersz, druga kolumna
            Console.Write("  |  "); 
            DrawCell(0, 2);                 // Rysuję komórkę (0,2) - pierwszy wiersz, trzecia kolumna
            Console.WriteLine();


            // Rysuję planszę w konsoli dla drugiego wiersza 

            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");

            Console.Write("  ");
            DrawCell(1, 0);                 // Rysuję komórkę (1,0) - drugi wiersz, pierwsza kolumna
            Console.Write("  |  ");
            DrawCell(1, 1);                 // Rysuję komórkę (1,1) - drugi wiersz, druga kolumna
            Console.Write("  |  ");
            DrawCell(1, 2);                 // Rysuję komórkę (1,2) - drugi wiersz, trzecia kolumna
            Console.WriteLine();


            // Rysuję planszę w konsoli dla trzeciego wiersza

            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");

            Console.Write("  ");
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
