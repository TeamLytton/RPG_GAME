using Diablo_Wannabe.ImageProcessing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Interfaces
{
    public interface IItem : IPickable, IUsable
    {
        Vector2 Position { get; set; }

        SpriteSheet Sprite { get; set; }

        void Draw(SpriteBatch spriteBatch);
    }
}