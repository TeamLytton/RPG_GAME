
using Diablo_Wannabe.Entities.Characters;
using Diablo_Wannabe.Interfaces;
using Microsoft.Xna.Framework;

namespace Diablo_Wannabe.Entities.Items
{
    public class HealingPotion : Item, IHeal
    {
        public int HealingPower { get; set; }

        public HealingPotion(Vector2 position) 
            : base(position)
        {
            this.HealingPower = 50;

            this.Sprite.CurrentFrame.X = 8;
            this.Sprite.CurrentFrame.Y = 2;
            this.Sprite.Update();
        }

        public override void Use(Player player)
        {
            this.Heal(player);
        }

        public void Heal(Player player)
        {
            player.Health += this.HealingPower;
        }
    }
}