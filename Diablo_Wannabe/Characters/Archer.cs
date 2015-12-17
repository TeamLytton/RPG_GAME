using Diablo_Wannabe.Entities;
using Diablo_Wannabe.Interfaces;

namespace Diablo_Wannabe.Characters
{
    class Archer : Player, IHero
    {
        private const int ArcherArmor = 70;
        private const int ArcherHealth = 120;
        private const int ArcherPower = 60;

        public int Armor { get; set; } = ArcherArmor;

        public int Health { get; set; } = ArcherHealth;

        public int Power { get; set; } = ArcherPower;
    }
}