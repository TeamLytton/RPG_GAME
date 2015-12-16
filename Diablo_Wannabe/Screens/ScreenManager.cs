
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Screens
{
    public class ScreenManager
    {
        private static ScreenManager manager;
        public Vector2 Dimensions { get; private set; }
        public ContentManager Content { get; private set; }

        public GraphicsDevice GraphicsDevice;
        public SpriteBatch SpriteBatch;

        public Screen CurrentScreen;

        private ScreenManager()
        {
            this.Dimensions = new Vector2(800, 600);
            CurrentScreen = new GameScreen();
        }

        public static ScreenManager Manager = manager ?? (manager = new ScreenManager());

        public void LoadContent(ContentManager content)
        {
            this.Content = new ContentManager(content.ServiceProvider, content.RootDirectory);
            CurrentScreen.LoadContent();
        }

        public void UnloadContent()
        {
            this.Content.Unload();
            CurrentScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen.Draw(spriteBatch);
        }
    }
}