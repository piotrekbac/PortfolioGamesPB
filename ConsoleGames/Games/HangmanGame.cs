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
        // Definiuję bazę naszych haseł w postaci słownika przechowującego listy stringów pod nazwą wordCategories
        private Dictionary<string, List<string>> wordCategories = new Dictionary<string, List<string>>
        {
            // Kategoria: Roślinność 
            {
                "Roślinność",
                new List<string>
                {
                    "motyl", "kwiatek", "polana", "łąka", "drzewo", "słońce", "chmura", "deszcz",
                }
            },

            // Kategoria: Zwierzęta
            {
                "Zwierzęta",
                new List<string> {
                    "lew", "tygrys", "słoń", "żyrafa", "kangur", "pingwin", "delfin", "wieloryb",
                }
            },

            // Kategoria: Stolice Europy
            {
                "Stolice Europy",
                new List<string>
                {
                    "warszawa", "berlin", "paryż", "madryt", "rzym", "londyn", "praga", "budapeszt",
                }
            },

            // Kategoria: Marki Samochodów
            {
                "Marki Samochodów",
                new List<string>
                {
                    "toyota", "ford", "bmw", "audi", "honda", "chevrolet", "nissan", "volkswagen",
                }
            }
        };

        private string targetWord;                  // Słowo do odgadnięcia
        private List<char> guessedLetters;          // Lista odgadniętych liter
        private int lives;                          // Liczba pozostałych żyć
        private string currentCategoryName;         // Aktualnie wybrana kategoria słów

        // Definiuję metodę Run, w której znajduje się główna logika gry
        public void Run()
        {
            // Inicjalizuję grę
            SetupGame();

            bool gameEnded = false;     // Flaga do śledzenia, czy gra się zakończyła

            // Główna pętla gry
            while (!gameEnded)
            {
                DrawInterface();    // Rysuję interfejs gry

                // Proszę użytkownika o podanie litery lub odgadnięcie hasła
                Console.WriteLine("Podaj literę lub spróbuj odgadnąć hasło: ");

                string input = Console.ReadLine().ToLower();   // Odczytuję wejście od użytkownika i konwertuję na małe litery  

                // Sprawdzam, czy użytkownik wpisał pojedynczą literę odpowiadającą za wyjście z programu
                if (input == "q" || input == "exit")
                {
                    return;     // Kończę działanie gry i wychodzę z metody Run
                }


                // Obsługuję logike gry

                // Odgadywanie jednej litery 
                if (input.Length == 1)
                {
                    char letter = input[0];         // Pobieram pierwszą literę z wejścia użytkownika
                    ProcessLetterGuess(letter);     // Przetwarzam zgadywanie pojedynczej litery
                }

                // Odgadywanie całego hasła
                else if (input.Length > 1)
                {
                    ProcesWordGuess(input);         // Przetwarzam zgadywanie całego hasła 
                }

                // Obsługa błędnego wejścia - jeżeli użytkownik nie podał ani litery, ani hasła
                else
                {
                    AuthorInfo.WriteError("Nie podano żadnej litery ani hasła. Proszę spróbować ponownie.");
                }

                // Sprawdzam warunki zakończenia gry - czy gracz wygrał lub przegrał
                if (CheckWin())
                {
                    DrawInterface();    // Rysuję interfejs gry
                    AuthorInfo.WriteColor("Gratulacje! Odgadłeś hasło!", ConsoleColor.Green);
                    gameEnded = true;   // Ustawiam flagę gameEnded na true, aby zakończyć grę
                }

                // Sprawdzam, czy gracz stracił wszystkie życia - przegrana gracza
                else if (lives <= 0)
                {
                    DrawInterface();    // Rysuję interfejs gry
                    AuthorInfo.WriteError($"Niestety, przegrałeś! Prawidłowe hasło to: {targetWord}");
                    gameEnded = true;   // Ustawiam flagę gameEnded na true, aby zakończyć grę
                }

                // Jeżeli gra się zakończyła, proszę użytkownika o naciśnięcie dowolnego klawisza, aby kontynuować
                Console.WriteLine();
                AuthorInfo.WriteColor("Naciśnij dowolny klawisz, aby kontynuować...", ConsoleColor.DarkGray);
                Console.ReadKey();   // Czekam na naciśnięcie dowolnego klawisza przez użytkownika
            }
        }

        // Definiuję metodę SelectCategory, która pozwala użytkownikowi wybrać kategorię haseł
        private bool SelectCategory()
        {
            // Wyświetlam nagłówek wyboru kategorii za pomocą klasy AuthorInfo
            AuthorInfo.DisplayHeader("Wisielec - wybór kategorii haseł");
            Console.WriteLine("Wybierz tematykę haseł: \n");


            // Dynamicznie generujemy listę opcji na podstawie kluczy w słowniku wordCategories
            // Dzięki temu dodajemy nową kategorię w kodzie, a menu aktualizuje się automatycznie

            int index = 1; // Inicjalizuję indeks do numerowania kategorii
            List<string> keys = wordCategories.Keys.ToList(); // Pobieram listę kluczy (nazw kategorii) ze słownika wordCategories

            // Iteruję przez klucze i wyświetlam je z numeracją
            foreach (var key in keys)
            {
                Console.WriteLine($"[{index}] {key}");      // Wyświetlam numerowaną kategorię
                index++;                                    // Zwiększam indeks dla kolejnej kategorii
            }

            // Wyświetlam opcję wyjścia z wyboru kategorii
            Console.WriteLine("Wpisz 'q', aby wyjść z wyboru kateogrii.");
            Console.WriteLine("\n=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n");
            Console.Write("Twój wybór: "); // Proszę użytkownika o wybór kategorii

            // Pętla do obsługi wyboru kategorii przez użytkownika
            while (true)
            {
                string input = Console.ReadLine().ToLower(); // Odczytuję wejście od użytkownika i konwertuję na małe litery

                // Sprawdzam, czy użytkownik wpisał 'q' lub 'exit' - w takim przypadku wychodzę z wyboru kategorii 
                if (input.ToLower() == "q" || input.ToLower() == "exit")
                {
                    return false; // Użytkownik wybrał wyjście, zwracam false
                }

                // Sprawdzam, czy wejście użytkownika jest poprawnym numerem kategorii
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= keys.Count)
                {
                    // Użytkownik dokonuje wyboru, sprawdzamy numer i mapujemy go na nazwę kategorii 
                    // przykładowo jeżeli użytkownik wybierze 1, to currentCategoryName będzie równe keys[0], czyli pierwszej kategorii w słowniku

                    // Ustawiam aktualnie wybraną kategorię na podstawie wyboru użytkownika - listy są indeksowane od 0 dlatego odejmujemy 1
                    currentCategoryName = keys[choice - 1]; 

                    return true; // Zwracam true, ponieważ użytkownik dokonał poprawnego wyboru kategorii
                }
            }
        }

        // Definiuję metodę SetupGame, która inicjalizuje grę
        private void SetupGame()
        {
            // Inicjalizuję losowanie słowa
            Random random = new Random();

            // Losuję słowo do odgadnięcia z listy wordRepository
            targetWord = wordRepository[random.Next(wordRepository.Count)];

            // Inicjalizuję listę odgadniętych liter
            guessedLetters = new List<char>();

            // Spację w haśle traktuję jako odgadniętą literę - ułatwia to grę
            if (targetWord.Contains(" "))
            {
                guessedLetters.Add(' '); // Dodaj spację do odgadniętych liter, jeśli słowo zawiera spację
            }

            lives = 6; // Ustawiam liczbę żyć na 6 - odpowiednio do rysunku wisielca
        }

        // Definiuję metodę ProcessLetterGuess, która przetwarza zgadywanie pojedynczej litery
        private void ProcessLetterGuess(char letter)
        {
            // Sprawdzam, czy podana została litera 
            if (!char.IsLetter(letter))
            {
                // Wypisuję komunikat o błędzie, jeżeli podany znak nie jest literą
                AuthorInfo.WriteError("To nie jest litera! Proszę podać prawidłową literę.");
                return;
            }

            // Sprawdzam, czy litera była już odgadnięta wcześniej 
            if (guessedLetters.Contains(letter))
            {
                // Wypisuję komunikat o błędzie, jeżeli litera została już odgadnięta
                AuthorInfo.WriteError($"Litera '{letter}' została już odgadnięta. Spróbuj inną literę.");
                return;
            }

            guessedLetters.Add(letter); // Dodaj literę do odgadniętych liter

            // Sprawdzam, czy podana litera znajduje się w docelowym słowie
            if (targetWord.Contains(letter))
            {
                AuthorInfo.WriteColor("Dobrze! Trafiłeś literę!", ConsoleColor.Green);
                System.Threading.Thread.Sleep(600); // Mała pauza, żeby użytkownik zdążył przeczytać komunikat
            }

            // Jeżeli litera nie występuje w haśle - odejmuję jedno życie i wypisuję komunikat o niepoprawnym strzale
            else
            {
                lives--; // Odejmij jedno życie za błędne zgadywanie

                // Wypisuję komunikat o błędzie, jeżeli litera nie występuje w haśle
                AuthorInfo.WriteColor($"Niestety, litera '{letter}' nie występuje w haśle. Tracisz jedno życie.", ConsoleColor.Red);
                System.Threading.Thread.Sleep(600); // Mała pauza, żeby użytkownik zdążył przeczytać komunikat
            }
        }

        // Definuję metodę ProcesWordGuess, która przetwarza zgadywanie całego słowa
        private void ProcesWordGuess(string word)
        {
            // Sprawdzam, czy podane słowo jest równe docelowemu słowu
            if (word == targetWord)
            {
                // jeżeli gracz zgadł całe słowo, dodajemy wszystkie litery do guessedLetters 
                foreach (char cha in targetWord)
                {
                    if (!guessedLetters.Contains(cha))
                    {
                        guessedLetters.Add(cha);        // Dodaj literę do odgadniętych liter
                    }
                }
            }

            // Jeżeli podane słowo jest niepoprawne - odejmujemy 2 życia graczowi - oto kara za porywcze zgadywanie
            else
            {
                lives = -2; // Ustawiam lives na -2, aby zakończyć grę jako przegraną
                AuthorInfo.WriteError("To nie jest to hasło! Za porywcze zgadywanie odejmuję Ci 2 życia!");

                System.Threading.Thread.Sleep(1500); // Mała pauza, żeby użytkownik zdążył przeczytać komunikat
            }
        }

        // Definiuję metodę CheckWin, która sprawdza, czy gracz wygrał grę
        private bool CheckWin()
        {
            // Metoda LINQ .all() sprawdzacza   czy wszystkie litery w targetWord znajdują się w guessedLetters
            return targetWord.All(c => guessedLetters.Contains(c));
        }

        // Definiuję metodę DrawInterface, która rysuje interfejs gry w konsoli
        private void DrawInterface()
        {
            // Wyświetlam nagłówek gry za pomocą klasy AuthorInfo
            AuthorInfo.DisplayHeader("Wisielec");

            // Rysuję szubienicę na podstawie liczby pozostałych żyć
            DrawSzubienica(lives);


            // Rysuję hasło z maskowaniem 

            Console.WriteLine("\nHasło do zgadnięcia: ");       // Wyświetlam tekst "Hasło do zgadnięcia: "
            Console.ForegroundColor = ConsoleColor.Yellow;      // Ustawiam kolor tekstu na żółty dla hasła
            Console.Write(" ");                                 // Dodaję odstęp przed hasłem

            // Iteruję przez każdą literę w docelowym słowie
            foreach (char c in targetWord)
            {
                // Sprawdzam, czy litera została odgadnięta
                if (guessedLetters.Contains(c))
                {
                    Console.WriteLine($"{c}");  // Wyświetlam odgadniętą literę
                }

                // obsługuję przypadek jeżeli litera nie została odgadnięta
                else
                {
                    //Jeżeli litera nie została odgadnięta, wyświetlam podkreślenie jako miejsce na literę
                    Console.WriteLine("_ ");
                }
            }

            Console.ResetColor();       // Resetuję kolor tekstu do domyślnego
            Console.WriteLine();        // Dodaję pustą linię dla lepszej czytelności

            // Obsługuję rysowanie wykorzystanych liter
            Console.Write("Użyj litery: ");
            Console.ForegroundColor = ConsoleColor.DarkGray;            // Ustawiam kolor tekstu na cyjanowy dla wskazówek użytkownika
            Console.WriteLine(string.Join(", ", guessedLetters));       // Wyświetlam odgadnięte litery oddzielone przecinkami
            Console.ResetColor();                                       // Resetuję kolor tekstu do domyślnego

            Console.WriteLine("\n=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n");
        }

        // Definiuję metodę DrawSzubienica, która rysuje wisielca w konsoli na podstawie liczby pozostałych żyć
        private void DrawSzubienica(int currentLives)
        {
            // Ustawiam kolor tekstu na ciemnocyjanowy
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            // Obliczam liczbę błędów na podstawie pozostałych żyć
            int mistakes = 6 - currentLives;

            // Rysuję szubienicę i wisielca w konsoli
            Console.WriteLine("  _______");
            Console.WriteLine("  |/    |");

            // Głowa wisielca 
            if (mistakes >= 1)
            {
                Console.WriteLine("  |     O");
            }
            else
            {
                Console.WriteLine("  |");
            }

            // Tułów i ramiona naszego wisielca
            if (mistakes == 2)
            {
                Console.WriteLine("  |     |");
            }
            else if (mistakes == 3)
            {
                Console.WriteLine("  |    /|");
            }
            else if (mistakes >= 4)
            {
                Console.WriteLine("  |    /|\\");
            }
            else
            {
                Console.WriteLine("  |");
            }

            // Nogi wisielca
            if (mistakes == 5)
            {
                Console.WriteLine("  |    /");
            }
            else if (mistakes >= 6)
            {
                Console.WriteLine("  |    / \\");
            }
            else
            {
                Console.WriteLine("  |");
            }

            // Podstawa szubienicy
            Console.WriteLine("  |");
            Console.WriteLine(" _|___\n");

            Console.ResetColor(); // Resetuję kolor tekstu do domyślnego

        }
    }
}
