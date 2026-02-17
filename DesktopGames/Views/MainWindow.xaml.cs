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
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;      // Timer do aktualizacji zegara
        private int timeElapsed;            // Czas, który upłynął w sekundach
        private int movesCount;             // Liczba wykonanych ruchów (opcjonalnie, można dodać licznik ruchów do gry)

        // Definiuję listę emoji zwierząt
        // Każde emoji występuje dwukrotnie, aby utworzyć pary
        private List<string> animalEmoji = new List<string>()
        {
            "🐶", "🐶",       // Pies
            "🐱", "🐱",       // Kot
            "🐭", "🐭",       // Mysz
            "🐹", "🐹",       // Chomik
            "🐰", "🐰",       // Królik
            "🦊", "🦊",       // Lis
            "🐻", "🐻",       // Niedźwiedź
            "🐼", "🐼",       // Panda
        };

        // Definiuje zmienne do śledzneia aktualnego stanu gry

        private Button firstClicked;                // Pierwszy kliknięty przycisk
        private Button secondClicked;               // Drugi kliknięty przycisk
        private int matchesFound;                   // Liczba znalezionych par
        private bool isGameLocked = false;          // Flaga blokady gry (np. podczas animacji zakrywania kart)

        // Definiuję nowoczesną paletę barw do gry 
        private readonly Brush CardBackBrush = (Brush)new BrushConverter().ConvertFromString("#6c5ce7");    // Fioletowy kolor tła kart
        private readonly Brush CardFrontBrush = (Brush)new BrushConverter().ConvertFromString("#dfe6e9");   // Szary kolor odkrytych kart
        private readonly Brush MatchBrush = (Brush)new BrushConverter().ConvertFromString("#00b894");       // Zielony kolor dla znalezionych par
        private readonly Brush ErrorBrush = (Brush) new BrushConverter().ConvertFromString("#d63031");      // Czerwony kolor dla błędnych par

        // Konstruktor okna głównego, gdzie inicjalizuję komponenty i ustawiam grę
        public MainWindow()
        {
            InitializeComponent();          // Inicjalizuję komponenty okna (generowane przez XAML)
            SetupGame();                    // Wywołuję metodę do ustawienia gry przy inicjalizacji okna
        }

        // Metoda do ustawienia gry
        private void SetupGame()
        {
            // Zabezpieczenie - zatrzymanie starego timera 
            if (timer != null)
            { 
                timer.Stop();               // Zatrzymuję timer, jeśli już istnieje, aby uniknąć konfliktów przy ponownym uruchomieniu gry
                timer = null;               // Resetuję timer do null, aby wskazać, że nie ma aktywnego timera
            }
            
            timeElapsed = 0;                // Ustawiam czas na 0
            matchesFound = 0;               // Ustawiam liczbę znalezionych par na 0
            movesCount = 0;                 // Ustawiam liczbę ruchów na 0 (jeśli chcemy ją śledzić)
            isGameLocked = false;           // Odblokowuję grę, aby można było klikać przyciski

            firstClicked = null;            // Resetuję pierwszy kliknięty przycisk
            secondClicked = null;           // Resetuję drugi kliknięty przycisk


            // Resetuję UI - ustawiam tekst zegara i licznik ruchów

            TimeTextBlock.Text = "Czas: 0 s";                     // Ustawiam tekst zegara na początkowy stan
            MovesTextBlock.Text = "Ruchy: 0";                     // Ustawiam tekst licznika ruchów na początkowy stan
            GameOverOverlay.Visibility = Visibility.Collapsed;    // Ukrywam nakładkę z komunikatem o zakończeniu gry, jeśli była widoczna


            // Definiuję nowy timer do aktualizacji zegara co sekundę, który będzie odliczał czas gry

            timer = new DispatcherTimer();                  // Konfiguruję timer do aktualizacji zegara co sekundę
            timer.Interval = TimeSpan.FromSeconds(1);       // Ustawiam interwał timera na 1 sekundę, aby aktualizować czas co sekundę
            timer.Tick += Timer_Tick;                       // Podłączam metodę obsługi zdarzenia Tick do timera
            timer.Start();                                  // Uruchamiam timer


            // Tworzę obiekt Random do losowania pozycji emoji
            Random random = new Random();   

            // Tasuję listę emoji, aby każda gra była inna
            animalEmoji = animalEmoji.OrderBy(x => random.Next()).ToList();   

            // Czyścię siatkę z poprzednich elementów (jeśli gra jest resetowana)
            GameGrid.Children.Clear();      

            // Pobieram styl dla kart z zasobów, aby zapewnić spójny wygląd przycisków"; 
            Style cardStyle = (Style)this.Resources["CardButtonStyle"];   

            // Tworzę przyciski i przypisuję im emoji jako tag, a zawartość ustawiam na znak zapytania (zakrycie)
            foreach (string emoji in animalEmoji)
            {
                Button button = new Button();          // Tworzę nowy przycisk
                button.Style = cardStyle;              // Ustawiam styl przycisku, aby zapewnić spójny wygląd kart
                button.Content = "";                   // Ustawiam zawartość przycisku na pusty
                button.Tag = emoji;                    // Przypisuję emoji do tagu przycisku, aby łatwo było je porównać podczas kliknięcia
                button.Background = CardBackBrush;     // Ustawiam tło przycisku na kolor tła kart, aby oznaczyć, że jest zakryty

                button.Click += Button_Click;          // Podłączam metodę obsługi kliknięcia do przycisku
                GameGrid.Children.Add(button);         // Dodaję przycisk do siatki
            }
        }

        // Metoda do obsługi kliknięcia przycisku - zresetowanie stanu gry 
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Zabezpieczenie - podpięcie metody Resetu - upewnienie się, że przycisk ma podpiętą metodę Resetu, aby można było zresetować grę po jej zakończeniu
            if (isGameLocked)
            {
                return;     // Jeżeli gra jest zablokowana (np. podczas animacji zakrywania kart), to ignorujemy kliknięcia, aby nie można było resetować gry w trakcie animacji
            }

            // Wywołuję metodę do ustawienia gry, aby zresetować stan gry i UI
            SetupGame();    
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
            // Definiujemy odpowiednie zabezpieczenia, aby uniknąć błędów, np. kliknięcia tego samego przycisku lub kliknięcia więcej niż dwóch przycisków
            // 1. Jeżeli timer nie działa (koniec gry), to ignorujemy kliknięcia
            // 2. Jeżeli przycisk jest już odkryty (nie jest pusty), to ignorujemy kliknięcia
            // 3. Jeżeli animacja trwa (mamy już dwa kliknięte przyciski), to ignorujemy kliknięcia

            // Sprawdzam, czy timer jest aktywny (gra jest w toku) - jeżeli timer nie działa, to gra jest zakończona i ignorujemy kliknięcia
            if (isGameLocked)
            {
                return; // Jeżeli gra jest zablokowana (np. podczas animacji zakrywania kart), to ignorujemy kliknięcia
            }

            Button clickedButton = sender as Button;            // Rzutuję sender na Button

            // Sprawdzam po kolorze tła czy przycisk jest odkryty (jeśli tło nie jest kolorem kart, to znaczy, że jest odkryty i pokazuje emoji, więc ignorujemy kliknięcia)
            if (clickedButton.Background != CardBackBrush)
            {
                return; // Jeżeli przycisk jest już odkryty (nie jest pusty), to ignorujemy kliknięcia
            }

            // Odkrywanie kart

            clickedButton.Content = clickedButton.Tag;          // Ustawiam zawartość klikniętego przycisku na jego tag (emoji), aby odkryć kartę i pokazać emoji
            clickedButton.Background = CardFrontBrush;          // Ustawiam tło klikniętego przycisku na kolor odkrytych kart, aby oznaczyć, że jest odkryty

            // Sprawdzan czy pierwszy przycisk jest null
            if (firstClicked == null)
            {
                firstClicked = clickedButton;       // Ustawiam pierwszy kliknięty przycisk
                return;                             // Zwracam, ponieważ czekam na drugi kliknięty przycisk, aby sprawdzić parę
            }

            secondClicked = clickedButton;          // Ustawiam drugi kliknięty przycisk

            movesCount++;                                      // Zwiększam licznik ruchów (jeśli chcemy go śledzić, można go wyświetlić w UI podobnie jak czas)
            MovesTextBlock.Text = $"Ruchy: {movesCount}";      // Aktualizuję tekst licznika ruchów w UI

            CheckForMatch();                        // Wywołuję metodę do sprawdzania, czy kliknięte przyciski są parą
        }       

        // Metoda do sprawdzania, czy kliknięte przyciski są parą - tutaj będzie logika porównywania zawartości dwóch klikniętych przycisków i aktualizacji stanu gry (np. liczby znalezionych par, zakończenia gry itp.)
        private void CheckForMatch()
        {
            // Sprawdzam, czy oba kliknięte przyciski mają ten sam tag (czy są parą)
            if (firstClicked.Tag.ToString() == secondClicked.Tag.ToString())
            {
                // sprawdzam czy pary są takie same - obsługa przypadku SUKCESU - znalezienia pary

                matchesFound++;                          // Zwiększam liczbę znalezionych par
                firstClicked.Background = MatchBrush;    // Ustawiam tło pierwszego klikniętego przycisku na kolor dla znalezionych par, aby oznaczyć, że ta para została znaleziona
                secondClicked.Background = MatchBrush;   // Ustawiam tło drugiego klikniętego przycisku na kolor dla znalezionych par, aby oznaczyć, że ta para została znaleziona


                // Usuwam obsługę kliknięcia dla tych przycisków, aby nie można było ich ponownie kliknąć (opcjonalnie, można też ustawić je jako nieaktywne lub ukryć, ale w tej implementacji po prostu usuwam obsługę kliknięcia)

                firstClicked.Click -= Button_Click;      // Usuwam obsługę kliknięcia dla pierwszego klikniętego przycisku, aby nie można było go ponownie kliknąć
                secondClicked.Click -= Button_Click;     // Usuwam obsługę kliknięcia dla drugiego klikniętego przycisku, aby nie można było go ponownie kliknąć

                // Restetuję zmienne do śledzenia klikniętych przycisków, ponieważ ta para została już znaleziona i nie musimy ich dalej śledzić

                firstClicked = null;            // Resetuję pierwszy kliknięty przycisk
                secondClicked = null;           // Resetuję drugi kliknięty przycisk

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
                // Obsługuję przypadek PORAŻKI 

                isGameLocked = true;                     // Blokuję grę, aby użytkownik nie mógł klikać innych przycisków podczas animacji zakrywania kart
                firstClicked.Background = ErrorBrush;    // Ustawiam tło pierwszego klikniętego przycisku na kolor dla błędnych par, aby oznaczyć, że ta para jest błędna
                secondClicked.Background = ErrorBrush;   // Ustawiam tło drugiego klikniętego przycisku na kolor dla błędnych par, aby oznaczyć, że ta para jest błędna

                // Jeżeli pary są różne to je zakrywamy z opóźnieniem, aby gracz miał czas zobaczyć, co było pod nimi
                // Używam DispatcherTimer do opóźnienia zakrycia kart

                DispatcherTimer delayTimer = new DispatcherTimer();       // Tworzę nowy timer do opóźnienia
                delayTimer.Interval = TimeSpan.FromMilliseconds(1000);    // Ustawiam interwał na 1000 ms (1 sekunda)

                // Podłączam metodę obsługi zdarzenia Tick do delayTimer, która zostanie wywołana po upływie interwału, aby zakryć karty ponownie
                delayTimer.Tick += (s, args) =>
                {
                    // Stan początkowuy to, że oba przyciski są odkryte i pokazują emoji, więc teraz musimy je zakryć ponownie, ustawiając zawartość na znak zapytania i przywracając tło do pierwotnego koloru
                    if (firstClicked != null)
                    {
                        firstClicked.Content = "";                        // Zakrywam pierwszy kliknięty przycisk (ustawiam zawartość na znak zapytania)
                        firstClicked.Background = CardBackBrush;          // Przywracam tło pierwszego klikniętego przycisku do pierwotnego koloru, aby oznaczyć, że jest ponownie zakryty
                    }

                    // Sprawdzam, czy drugi kliknięty przycisk nie jest null, ponieważ może się zdarzyć, że gracz kliknie tylko jeden przycisk i potem timer się uruchomi (choć w tej implementacji jest to mało prawdopodobne, ale warto mieć takie zabezpieczenie)
                    if (secondClicked != null)
                    {
                        secondClicked.Content = "";                       // Zakrywam drugi kliknięty przycisk (ustawiam zawartość na znak zapytania)
                        secondClicked.Background = CardBackBrush;         // Przywracam tło drugiego klikniętego przycisku do pierwotnego koloru, aby oznaczyć, że jest ponownie zakryty
                    }

                    firstClicked = null;           // Resetuję pierwszy kliknięty przycisk
                    secondClicked = null;          // Resetuję drugi kliknięty przycisk
                    isGameLocked = false;          // Odblokowuję grę, aby użytkownik mógł ponownie klikać przyciski po zakończeniu animacji zakrywania kart
                    delayTimer.Stop();             // Zatrzymuję timer, ponieważ jego zadanie jest wykonane
                };

                // Uruchamiam timer, aby rozpocząć odliczanie do zakrycia kart
                delayTimer.Start();                
            }
        }

        // Metoda ShowGameOver - wyświetla komunikat po zakończeniu gry
        private void ShowGameOver()
        {
            FinalScoreText.Text = $"Czas: {timeElapsed}s, Ruchy: {movesCount}";   // Wyświetlam końcowy wynik (czas i liczba ruchów) w UI
            GameOverOverlay.Visibility = Visibility.Visible;                        // Pokazuję nakładkę z komunikatem o zakończeniu gry    
        }

    }
}
