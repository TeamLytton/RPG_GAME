
using Diablo_Wannabe.Entities.StatsBars;
using Microsoft.Xna.Framework;

namespace Diablo_Wannabe.Entities.Enemies
{
    public class OrcMace : Enemy
    {
        private const string DefaultPath = "Entities/orc-mace-";
        private const int DefaultMovementSpeed = 2;
        private const int DefaultHealth = 100;
        private const int DefaultWeaponRange = 45;
        private const int DefaultArmor = 20;
        private const int DefaultDamage = 50;

        public OrcMace(Vector2 position) 
            : base(position, DefaultPath, DefaultMovementSpeed, DefaultHealth, DefaultWeaponRange, DefaultArmor, DefaultDamage)
        {
        }
    }
}