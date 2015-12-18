
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Entities.StatsBars
{
    public class HealthBar
    {
        private SpriteSheet redBar;
        private SpriteSheet greenBar;

        public HealthBar(Vector2 position)
        {
            this.redBar = new SpriteSheet(1, 1, position, "statsbars/red_bar");
            this.greenBar = new SpriteSheet(60, 1, position, "statsbars/green_bar");
            this.redBar.LoadContent(ScreenManager.Instance.Content);
            this.greenBar.LoadContent(ScreenManager.Instance.Content);
            this.greenBar.Origin = Vector2.Zero;
            this.redBar.Origin = Vector2.Zero;

        }

        public void Draw(SpriteBatch spriteBatch, int health, int maxHealth, Vector2 position)
        {

            this.greenBar.CurrentFrame.X = (int)(((float)health/maxHealth)*60.0);

            this.greenBar.SourceRect = new Rectangle((int)0, 0,
                                            (int)this.greenBar.CurrentFrame.X, (int)this.greenBar.FrameDimensions.Y);
            greenBar.Position.X = position.X - 32;
            greenBar.Position.Y = position.Y - 40;
            redBar.Position.X = position.X - 32;
            redBar.Position.Y = position.Y - 40;

            redBar.Draw(spriteBatch);
            greenBar.Draw(spriteBatch);
        }
    }
}