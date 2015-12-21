using Diablo_Wannabe.Entities.Characters;
using Microsoft.Xna.Framework;

namespace Diablo_Wannabe.Entities.Items
{
    public class AttackRateIncreaseCrystal : StatCrystal
    {
        private const int DefaultAttackRateIncrease = -5;

        public AttackRateIncreaseCrystal(Vector2 position) 
            : base(position, DefaultAttackRateIncrease)
        {
            this.Sprite.CurrentFrame.X = 6;
            this.Sprite.CurrentFrame.Y = 6;
            this.Sprite.Update();
        }

        public override void Use(Player player)
        {
            if (player is Knight)
            {
                player.AttackRate += 2*valueModifier;
            }
            else
            {
                player.AttackRate += this.valueModifier;
            }
            this.HasBeenUsed = true;
        }
    }
}