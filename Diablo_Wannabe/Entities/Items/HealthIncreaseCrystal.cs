using Diablo_Wannabe.Entities.Characters;
using Microsoft.Xna.Framework;

namespace Diablo_Wannabe.Entities.Items
{
    public class HealthIncreaseCrystal : StatCrystal
    {
        private const int DefaultMaxHealthIncrease = 10;

        public HealthIncreaseCrystal(Vector2 position) 
            : base(position, DefaultMaxHealthIncrease)
        {
            this.Sprite.CurrentFrame.X = 7;
            this.Sprite.CurrentFrame.Y = 6;
            this.Sprite.Update();
        }

        public override void Use(Player player)
        {
            player.MaxHealth += this.valueModifier;
            player.Health += this.valueModifier;
            this.HasBeenUsed = true;

        }
    }
}