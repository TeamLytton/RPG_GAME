using Diablo_Wannabe.Entities.Characters;
using Microsoft.Xna.Framework;

namespace Diablo_Wannabe.Entities.Items
{
    public abstract class StatCrystal : Item
    {
        protected int valueModifier;
        public bool HasBeenUsed { get; set; }

        protected StatCrystal(Vector2 position , int valueModifier) 
            : base(position)
        {
            this.valueModifier = valueModifier;
        }
    }
}