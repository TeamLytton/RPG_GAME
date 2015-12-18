
using Microsoft.Xna.Framework;

namespace Diablo_Wannabe.Interfaces
{
    public interface ICaster : IUnit
    {
        bool IsCasting { get; set; }

        void PlayCastAnimation(GameTime gameTime);
    }
}