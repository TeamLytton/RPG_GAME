
using Diablo_Wannabe.ImageProcessing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Interfaces
{
    public interface IProjectile
    {
        SpriteSheet Sprite { get; set; }
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        int FlySpeed { get; set; }
        bool Exists { get; set; }
        void Fly(int damage, GameTime gameTime);
        void Draw(SpriteBatch spritBatch);
    }
}