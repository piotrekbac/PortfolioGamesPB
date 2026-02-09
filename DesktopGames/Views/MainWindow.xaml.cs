using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;         // Korzystamy z Threading dla DispatcherTimer

// Piotr Bacior - Projekt portfolio 2026 

namespace DesktopGames.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;      // Timer do aktualizacji zegara
        private int timeElapsed;            // Czas, który upłynął w sekundach

        // Definiuję listę emoji zwierząt
        private List<string> animalEmoji = new List<string>()
        {
            "🐶", "🐶",
            "🐱", "🐱",
            "🐭", "🐭",
            "🐹", "🐹",
            "🐰", "🐰",
            "🦊", "🦊",
            "🐻", "🐻",
            "🐼", "🐼",
        };

        // Definiuje zmienne do śledzneia aktualnego stanu gry

        private Button firstClicked;        // Pierwszy kliknięty przycisk
        private Button secondClicked;       // Drugi kliknięty przycisk
        private int matchesFound;           // Liczba znalezionych par

        public MainWindow()
        {
            InitializeComponent();
            SetupGame();                    // Wywołuję metodę do ustawienia gry przy inicjalizacji okna
        }

        // Metoda do ustawienia gry
        private void SetupGame()
        {
            timeElapsed = 0;                // Ustawiam czas na 0
            matchesFound = 0;               // Ustawiam liczbę znalezionych par na 0

            // Konfiguruję timer do aktualizacji zegara co sekundę
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);

            timer.Tick += Timer_Tick;       // Podłączam metodę obsługi zdarzenia Tick do timera
            timer.Start();                  // Uruchamiam timer

            Random random = new Random();   // Tworzę obiekt Random do losowania pozycji emoji

            animalEmoji = animalEmoji.OrderBy(x => random.Next()).ToList();   // Tasuję listę emoji, aby każda gra była inna

            GameGrid.Children.Clear();      // Czyścię siatkę z poprzednich elementów (jeśli gra jest resetowana)

            // Tworzę przyciski i przypisuję im emoji jako tag, a zawartość ustawiam na znak zapytania (zakrycie)
            foreach (string emoji in animalEmoji)
            {
                Button button = new Button();          // Tworzę nowy przycisk
                button.FontSize = 32;                  // Ustawiam rozmiar czcionki, aby emoji były dobrze widoczne
                button.Content = "?";                  // Ustawiam zawartość przycisku na znak zapytania (zakrycie)
                button.Tag = emoji;                    // Przypisuję emoji do tagu przycisku, aby łatwo było je porównać podczas kliknięcia
                button.Click += Button_Click;          // Podłączam metodę obsługi kliknięcia do przycisku

                GameGrid.Children.Add(button);         // Dodaję przycisk do siatki
            }
        }

        // Metoda do obsługi kliknięcia przycisku - obsługa kliku
        private void Timer_Tick(object sender, EventArgs e)
        {
            timeElapsed++;                                      // Zwiększam czas o 1 sekundę
            TimeTextBlock.Text = $"Czas: {timeElapsed} s";      // Aktualizuję tekst zegara
        }

        // Metoda do obsługi kliknięcia przycisku - tutaj będzie logika gry, np. sprawdzanie par
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;      // Rzutuję sender na Button

            // Definiujemy odpowiednie zabezpieczenia, aby uniknąć błędów, np. kliknięcia tego samego przycisku lub kliknięcia więcej niż dwóch przycisków
            // 1. Jeżeli timer nie działa (koniec gry), to ignorujemy kliknięcia
            // 2. Jeżeli przycisk jest już odkryty (nie jest pusty), to ignorujemy kliknięcia
            // 3. Jeżeli animacja trwa (mamy już dwa kliknięte przyciski), to ignorujemy kliknięcia

            clickedButton.Content = clickedButton.Tag;    // Ustawiam zawartość przycisku na jego tag (emoji)

            // Obsługa sprawdzenie kliknięcia pierwszej karty (pierwszego buttona)
            if (firstClicked == null)
            {
                firstClicked = clickedButton;    // Ustawiam pierwszy kliknięty przycisk
                return;                          // Kończę metodę, czekając na drugie kliknięcie
            }

            secondClicked = clickedButton;      // Ustawiam drugi kliknięty przycisk
        }

        // Metoda do sprawdzania, czy kliknięte przyciski są parą - tutaj będzie logika porównywania zawartości dwóch klikniętych przycisków i aktualizacji stanu gry (np. liczby znalezionych par, zakończenia gry itp.)
        private void CheckForMatch()
        {
            // Sprawdzam, czy oba kliknięte przyciski mają ten sam tag (czy są parą)
            if (firstClicked.Tag.ToString() == secondClicked.Tag.ToString())
            {
                // sprawdzam czy pary są takie same 

                matchesFound++;               // Zwiększam liczbę znalezionych par
                firstClicked = null;          // Resetuję pierwszy kliknięty przycisk
                secondClicked = null;         // Resetuję drugi kliknięty przycisk

                // Sprawdzam, czy wszystkie pary zostały znalezione (w tym przypadku 8 par)
                if (matchesFound == 8)
                {
                    timer.Stop();                                                                           // Zatrzymuję timer, ponieważ gra jest zakończona
                    MessageBox.Show($"Gratulacje! Znalazłeś wszystkie pary w {timeElapsed} sekund!");       // Wyświetlam komunikat o zakończeniu gry z czasem
                }
            }

            // Sprawdzam, czy kliknięte przyciski nie są parą (nie mają tego samego tagu)
            else
            {
                // Jeżeli pary są różne to je zakrywamy z opóźnieniem, aby gracz miał czas zobaczyć, co było pod nimi
                // Używam DispatcherTimer do opóźnienia zakrycia kart

                DispatcherTimer delayTimer = new DispatcherTimer();      // Tworzę nowy timer do opóźnienia
                delayTimer.Interval = TimeSpan.FromMilliseconds(800);    // Ustawiam interwał na 800 ms (0.8 sekundy)
                delayTimer.Tick += (s, args) =>
                {
                    // Zakrycie kart 
                    firstClicked.Content = "?";    // Ustawiam zawartość pierwszego przycisku na znak zapytania (zakrycie)
                    secondClicked.Content = "?";   // Ustawiam zawartość drugiego przycisku na znak zapytania (zakrycie)

                    firstClicked = null;           // Resetuję pierwszy kliknięty przycisk
                    secondClicked = null;          // Resetuję drugi kliknięty przycisk

                    delayTimer.Stop();             // Zatrzymuję timer, ponieważ jego zadanie jest wykonane
                };
                delayTimer.Start();                // Uruchamiam timer, aby rozpocząć odliczanie do zakrycia kart
            }

        }
    }
}