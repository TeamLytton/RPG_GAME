using Diablo_Wannabe.Entities;
using Diablo_Wannabe.Interfaces;

namespace Diablo_Wannabe.Characters
{
    class Mage : Player, IHero
    {
        private const int MageArmor = 50;
        private const int MageHealth = 100;
        private const int MagePower =100;

        public int Armor { get; set; } = MageArmor;

        public int Health { get; set; } = MageHealth;

        public int Power { get; set; } = MagePower;
    }
}