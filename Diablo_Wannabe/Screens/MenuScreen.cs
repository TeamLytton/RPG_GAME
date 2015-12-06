using System.Xml.Serialization;
using Diablo_Wannabe.ImageProcessing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Screens
{
    public class MenuScreen : Screen
    {
        public Image MenuImage;

        public override void LoadContent()
        {
            base.LoadContent();
            MenuImage.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            MenuImage.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //if for example the mouse cursor X & Y are inside the new game coordinates for example,
            //change the image path dynamically here dont change the image class by making it take array of paths which has no logic
            //or you can see how you can make a list of images and load their paths before-hand, and change the image accordingly
            //to mouse cursor position

            base.Update(gameTime);
            MenuImage.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            MenuImage.Draw(spriteBatch);
        }
    }
}