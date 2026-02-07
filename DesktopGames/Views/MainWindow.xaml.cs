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
        }

        // Metoda do ustawienia gry
        private void SetupGame()
        {
            timeElapsed = 0;                // Ustawiam czas na 0
            matchesFound = 0;               // Ustawiam liczbę znalezionych par na 0

            // Konfiguruję timer do aktualizacji zegara co sekundę
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
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
            Button clickedButton = sender as Button;    // Rzutuję sender na Button

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
        }
    }
}