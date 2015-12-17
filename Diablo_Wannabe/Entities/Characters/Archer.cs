using Diablo_Wannabe.Interfaces;
using Microsoft.Xna.Framework;

namespace Diablo_Wannabe.Entities.Characters
{
    public class Archer : Player, IShooter
    {
        private const int ArcherDefaultMoveSpeed = 3;
        private const int ArcherDefaultWeaponRange = 500;
        private const int ArcherDefaultHealth = 80;
        private const int ArcherDefaultArmor = 25;
        private const int ArcherDefaultDamage = 40;

        public Archer(string path) 
            : base(path, ArcherDefaultMoveSpeed, ArcherDefaultWeaponRange, ArcherDefaultHealth, ArcherDefaultArmor, ArcherDefaultDamage)
        {
        }

        public bool IsShooting { get; set; }
        public void PlayShootingAnimation(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}