
using System;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Screens
{
    public class GameScreen : Screen
    {
        private SpriteSheet gameOver;
        private TimeSpan Clearance;

        public override void LoadContent()
        {
            base.LoadContent();
            Map.Initialize();
            Clearance = new TimeSpan();
            this.gameOver = new SpriteSheet(1, 1, Vector2.Zero, "gameover");
            gameOver.LoadContent(ScreenManager.Instance.Content);
            gameOver.Origin = Vector2.Zero;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (Clearance.Ticks == 0)
            {
                Clearance = new TimeSpan();
                Clearance = gameTime.TotalGameTime;
            }
            if (gameTime.TotalGameTime.TotalSeconds - Clearance.TotalSeconds > 20)
            {
                Map.Enemies.RemoveAll(e => !e.IsAlive);
            }
            base.Update(gameTime);
            Map.Player.Update(gameTime);
            Map.Wife.Update(gameTime);
            Map.Enemies.ForEach(e => e.Update(gameTime));
            Map.SpawnEnemies(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Map.Wife.IsAlive)
            {
                Map.Draw(spriteBatch);
            }
            else
            {
                gameOver.Draw(spriteBatch);
            }
            base.Draw(spriteBatch);    
        }
    }
}