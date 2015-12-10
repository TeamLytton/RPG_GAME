using System.Xml.Serialization;
using Diablo_Wannabe.Entities;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Screens
{
    public class GameScreen : Screen
    {
        public Map.Map map;
         private Player player;

        public override void LoadContent()
        {
            base.LoadContent();
            this.map = new Map.Map();
            this.map.FillMap();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);
            base.Draw(spriteBatch);    
        }
    }
}