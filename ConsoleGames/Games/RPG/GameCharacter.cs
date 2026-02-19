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
        public int Armor { get; set; }          // Dodatkowa wartość obrony postaci (np. z pancerza)

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
            Health = health;               // Ustawiamy aktualny poziom życia postaci
            Damage = damage;               // Ustawiamy siłę ataku postaci
            Symbol = symbol;               // Ustawiamy symbol reprezentujący postać na mapie
            Color = color;                 // Ustawiamy kolor reprezentujący postać na mapie
            Armor = 0;                     // Domyślna wartość pancerza postaci to 0
        }

        // Tworzymy metodę wirtualną - TakeDamage, która będzie odpowiedzialna za zadawanie obrażeń postaci - będzie ona nadpisywana w klasach potomnych 
        public virtual void TakeDamage(int damage)
        {
            // Obliczamy rzeczywiste obrażenia, uwzględniając obronę postaci - jeśli obrażenia są mniejsze niż obrona, to postać nie otrzymuje obrażeń
            int actualDamage = Math.Max(0, damage - Armor);

            // Zmniejszamy poziom życia postaci o obliczone obrażenia
            Health -= actualDamage;

            // Zapobiegamy ujemnemu poziomowi życia - jeśli poziom życia spadnie poniżej 0, ustawiamy go na 0
            if (Health < 0)
            {
                Health = 0; // Zapobiegamy ujemnemu poziomowi życia 
            }
        }

        // Tworzymy metodę isDead, która będzie sprawdzać, czy postać jest martwa - jeśli poziom życia jest równy 0, to postać jest martwa
        public bool isDead()
        {
            // Sprawdzamy, czy poziom życia postaci jest równy 0 - jeśli tak, to postać jest martwa, w przeciwnym razie jest żywa
            return Health <= 0; 
        }

        // Tworzymy metodę Heal, która będzie odpowiedzialna za leczenie postaci - zwiększa poziom życia postaci o określoną wartość, ale nie przekracza maksymalnego poziomu życia
        public void Heal(int amount)
        {
            Health += amount; // Zwiększamy poziom życia postaci o określoną wartość

            // Zapobiegamy przekroczeniu maksymalnego poziomu życia - jeśli poziom życia przekroczy maksymalny poziom życia, ustawiamy go na maksymalny poziom życia
            if (Health > MaxHealth)
            {
                Health = MaxHealth; // Zapobiegamy przekroczeniu maksymalnego poziomu życia
            }
        }
    }
}
