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
            base.Update(gameTime);
            MenuImage.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            MenuImage.Draw(spriteBatch);
        }
    }
}