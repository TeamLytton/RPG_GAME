
using System;
using Diablo_Wannabe.Entities.StatsBars;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Entities
{
    public class Wife : Unit
    {
        private const string DefaultWifePath = "Entities/wife";
        private HealthBar healthBar;
        public Wife()
        {
            this.Position = new Vector2(ScreenManager.Instance.Dimensions.X/10, ScreenManager.Instance.Dimensions.Y/2);
            this.Sprites = new SpriteSheet[1];
            this.Sprites[0] = new SpriteSheet(1, 2, Position, DefaultWifePath);
            this.Sprites[0].LoadContent(ScreenManager.Instance.Content);
            this.LastAction = new TimeSpan();
            this.MaxHealth = 2000;
            this.Health = 2000;
            this.IsAlive = true;
            this.Armor = 0;
            this.healthBar = new HealthBar(this.Position);
            this.BoundingBox = new Rectangle((int)this.Position.X - 16, (int)this.Position.Y - 16,
            (int)this.Sprites[0].FrameDimensions.X / 2, (int)this.Sprites[0].FrameDimensions.Y / 2);
        }

        public void TakeDamage(int damage)
        {
            if (damage - this.Armor > 0)
            {
                this.Health -= damage - Armor;
            }
            else
            {
                this.Health -= 1;
            }
            if (this.Health <= 0)
            {
                this.IsAlive = false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (LastAction.Ticks == 0)
            {
                this.LastAction = gameTime.TotalGameTime;
            }
            if ((int)gameTime.TotalGameTime.TotalSeconds - (int)LastAction.TotalSeconds == 10)
            {
                this.LastAction = gameTime.TotalGameTime;
                this.Sprites[0].CurrentFrame.Y++;
                if (this.Sprites[0].CurrentFrame.Y > 1)
                {
                    this.Sprites[0].CurrentFrame.Y = 0;
                }
                this.Sprites[0].Update();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.healthBar.Draw(spriteBatch, this.Health, this.MaxHealth, this.Position);
            this.Sprites[0].Draw(spriteBatch);
        }
    }
}