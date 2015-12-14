
using Diablo_Wannabe.ImageProcessing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Entities
{
    public class GameObject
    {
        public Vector2 Position;

        public virtual void LoadContent()
        {
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}