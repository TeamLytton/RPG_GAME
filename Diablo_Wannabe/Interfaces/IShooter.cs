using Microsoft.Xna.Framework;

namespace Diablo_Wannabe.Interfaces
{
    public interface IShooter
    {
        bool IsShooting { get; set; }

        void PlayShootingAnimation(GameTime gameTime);
    }
}