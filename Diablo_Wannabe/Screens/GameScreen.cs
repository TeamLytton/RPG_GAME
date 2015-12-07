using System.Xml.Serialization;
using Diablo_Wannabe.Entities;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.XmlSerialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Screens
{
    public class GameScreen : Screen
    {
         public Image testing;

         private Player player;

        public override void LoadContent()
        {
            base.LoadContent();
            testing = new Image();
            testing.Path = "only-background";
            player = new Player();
            var playerLoader = new XmlSerializer<Player> {Type = player.Type};
            player = playerLoader.Load("../../../LoadXml/Player.xml");
            testing.LoadContent();
            player.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            testing.UnloadContent();
            player.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            testing.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            testing.Draw(spriteBatch);
            base.Draw(spriteBatch);    
            player.Draw(spriteBatch);
        }
    }
}