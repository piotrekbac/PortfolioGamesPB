using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Piotr Bacior - 2026 - Console Games - RPG Game - GameCharacter class

namespace ConsoleGames.Games.RPG
{
    // Klasa bazowa dla postaci w grze RPG - musimy utworzyć albo gracza albo potwora
    public abstract class GameCharacter
    {
        // Ustawiamy odpowiednie właściwości dla postaci - nazwa, poziom życia, atak i obronę
        public string Name { get; set; }        // Nazwa postaci
        public int Health { get; set; }         // Poziom życia postaci
        public int MaxHealth { get; set; }      // Maksymalny poziom życia postaci
        public int Damage { get; set; }         // Siła ataku postaci
        public int Defense { get; set; }        // Siła obrony postaci

        // Ustawiamy pozycję na mapie - x i y
        public int X { get; set; }              // Pozycja X postaci na mapie
        public int Y { get; set; }              // Pozycja Y postaci na mapie
        public char Symbol { get; set; }        // Symbol reprezentujący postać na mapie
        public ConsoleColor Color { get; set; } // Kolor reprezentujący postać na mapie

        // Konstruktor do inicjalizacji postaci z odpowiednimi wartościami
        public GameCharacter(string name, int health, int damage, char symbol, ConsoleColor color)
        {
            Name = name;                   // Ustawiamy nazwę postaci
            MaxHealth = health;            // Ustawiamy maksymalny poziom życia postaci
        }
    }
}
