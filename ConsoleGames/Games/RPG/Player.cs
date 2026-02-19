using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Piotr Bacior - 2026 - Console Games - RPG Game - Player class

namespace ConsoleGames.Games.RPG
{
    // Klasa reprezentująca gracza w grze RPG - dziedziczy po klasie GameCharacter, ponieważ gracz jest postacią w grze
    public class Player : GameCharacter
    {
        // Konstruktor do inicjalizacji gracza z odpowiednimi wartościami - wywołujemy konstruktor klasy bazowej, przekazując odpowiednie parametry, a dodatkowo ustawiamy poziom gracza na 1
        public int Level { get; set; }           // Poziom gracza - im wyższy poziom, tym silniejszy gracz
        public int Experience { get; set; }      // Doświadczenie gracza - zdobywane za pokonanie potworów, im więcej doświadczenia, tym szybciej gracz awansuje na wyższy poziom
    }
}
