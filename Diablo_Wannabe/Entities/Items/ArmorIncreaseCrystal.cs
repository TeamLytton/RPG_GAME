using Diablo_Wannabe.Entities.Characters;
using Microsoft.Xna.Framework;

namespace Diablo_Wannabe.Entities.Items
{
    public class ArmorIncreaseCrystal : StatCrystal
    {
        private const int DefaultArmorIncrease = 5;

        public ArmorIncreaseCrystal(Vector2 position) 
            : base(position, DefaultArmorIncrease)
        {
            this.Sprite.CurrentFrame.X = 4;
            this.Sprite.CurrentFrame.Y = 6;
            this.Sprite.Update();
        }

        public override void Use(Player player)
        {
            player.Armor += DefaultArmorIncrease;
            this.HasBeenUsed = true;

        }
    }
}