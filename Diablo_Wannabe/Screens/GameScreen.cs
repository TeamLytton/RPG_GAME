
using Diablo_Wannabe.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Screens
{
    public class GameScreen : Screen
    {

        public override void LoadContent()
        {
            base.LoadContent();
            Map.Initialize();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Map.Enemies.ForEach(e => e.Update(gameTime));
            Map.Player.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Map.Draw(spriteBatch);
            base.Draw(spriteBatch);    
        }
    }
}