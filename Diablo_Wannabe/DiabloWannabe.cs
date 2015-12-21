using Diablo_Wannabe.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Diablo_Wannabe
{
    public class DiabloWannabe : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public DiabloWannabe()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
            this.graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
            this.graphics.ApplyChanges();
            this.IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ScreenManager.Instance.GraphicsDevice = this.GraphicsDevice;
            ScreenManager.Instance.SpriteBatch = this.spriteBatch;
            ScreenManager.Instance.LoadContent(this.Content);    
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            ScreenManager.Instance.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Input.Instance.Update();

            if (Input.Instance.KeyPressed(Keys.Escape))
            {
                this.Exit();
            }
            ScreenManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            ScreenManager.Instance.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
