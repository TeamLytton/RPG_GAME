using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace Diablo_Wannabe.Entities
{
    public class OrcMace : Enemy
    {
        private const string DefaultPath = "Entities/orc-mace-";
        private const int DefaultMovementSpeed = 2;
        private const int DefaultHealth = 70;
        private const int DefaultWeaponRange = 45;
        private const int DefaultArmor = 20;
        private const int DefaultDamage = 15;

        public OrcMace(Vector2 position) 
            : base(position, DefaultPath, DefaultMovementSpeed, DefaultHealth, DefaultWeaponRange, DefaultArmor, DefaultDamage)
        {
        }
    }
}