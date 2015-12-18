using Diablo_Wannabe.Entities.Characters;
using Microsoft.Xna.Framework;

namespace Diablo_Wannabe.Entities.Items
{
    public class DamageIncreaseCrystal : StatCrystal
    {
        private const int DefaultDamageIncrease = 5;

        public DamageIncreaseCrystal(Vector2 position) 
            : base(position, DefaultDamageIncrease)
        {
            this.Sprite.CurrentFrame.X = 6;
            this.Sprite.CurrentFrame.Y = 7;
            this.Sprite.Update();
        }

        public override void Use(Player player)
        {
            player.Damage += this.valueModifier;
            this.HasBeenUsed = true;

        }
    }
}