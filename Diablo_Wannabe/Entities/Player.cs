

using System;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Diablo_Wannabe.Entities
{
    public class Player : Unit
    {
        public Vector2 Velocity;
        public SpriteSheet playerSprite;
        public bool isActive;
        public Type Type;

        public Player()
        {
            this.Type = this.GetType();
            this.isActive = false;
            this.Initialize();
        }

        public override void Move(GameTime gameTime)
        {   
            if (Input.Manager.KeyDown(Keys.Down))
            {
                this.isActive = true;
                this.playerSprite.Position.Y++;
                this.playerSprite.CurrentFrame.X += gameTime.ElapsedGameTime.Milliseconds * 0.02f;
                if (playerSprite.CurrentFrame.X > 7)
                {
                    playerSprite.CurrentFrame.X = 0;
                }
            }
        }

        public void Initialize()
        {
            this.Velocity = Vector2.Zero;
        }

        public override void LoadContent()
        {
            playerSprite.LoadContent(ScreenManager.Manager.Content);
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            this.Move(gameTime);
            playerSprite.Update(gameTime, isActive);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.playerSprite.Draw(spriteBatch);
        }
    }
}