
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.ImageProcessing
{
    public class SpriteSheet
    {
        public Texture2D Texture;
        private string path;
        public Vector2 Position;
        public Vector2 Origin;
        public Vector2 FrameDimensions;
        public Vector2 CurrentFrame;
        public Rectangle SourceRect;
        public float RotationAngle;
        private int totalFramesX;
        private int totalFramesY;

        public SpriteSheet(int totalFramesX, int totalFramesY, Vector2 Position, string Path)
        {
            this.totalFramesX = totalFramesX;
            this.totalFramesY = totalFramesY;
            this.Position = Position;
            this.path = Path;
            this.Origin = Vector2.Zero;
            this.FrameDimensions = Vector2.Zero;
            this.CurrentFrame = Vector2.Zero;
            this.SourceRect = Rectangle.Empty;
            this.RotationAngle = 0.0f;
        }

        public void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>(path);
            this.Origin = new Vector2(Texture.Width / (totalFramesX * 2), Texture.Height / (totalFramesY * 2));
            this.FrameDimensions = new Vector2(Texture.Width / totalFramesX, Texture.Height / totalFramesY);
            this.SourceRect = new Rectangle((int)this.FrameDimensions.X * (int)CurrentFrame.X, (int)this.FrameDimensions.Y * (int)this.CurrentFrame.Y, (int)this.FrameDimensions.X, (int)this.FrameDimensions.Y);
        }

        public void Update(GameTime gameTime)
        {
            this.SourceRect = new Rectangle((int)this.FrameDimensions.X * (int)CurrentFrame.X, (int)this.FrameDimensions.Y * (int)this.CurrentFrame.Y, (int)this.FrameDimensions.X, (int)this.FrameDimensions.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,
                Position,
                SourceRect,
                Color.White,
                RotationAngle,
                Origin,
                1f,
                SpriteEffects.None,
                0.0f);
        }
    }
}