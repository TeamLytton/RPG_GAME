
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Diablo_Wannabe.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.ImageProcessing
{
    public class Image
    {
        private ContentManager content;
        private RenderTarget2D renderTarget;
        private Vector2 origin;
        private SpriteFont spriteFont;

        public float Alpha;
        public string Text, FontName;
        public string Effect;
        public bool IsActive;

        public string Path;

        public Vector2 Position, Scale;

        [XmlIgnore]
        public Texture2D Texture;
        public Rectangle SourceRect;



        public Image()
        {
            this.Path = string.Empty;
            this.Text = string.Empty;
            this.Effect = string.Empty;
            this.FontName = "fonts/default_font";
            this.Position = Vector2.Zero;
            this.Scale = Vector2.One;
            this.SourceRect = Rectangle.Empty;
            this.Alpha = 1.0f;
        }

        public void LoadContent()
        {
            content =
                new ContentManager
                (ScreenManager.Instance.Content.ServiceProvider,
                 ScreenManager.Instance.Content.RootDirectory);

            if (Path != string.Empty)
            {
                Texture = content.Load<Texture2D>(Path);
            }

            spriteFont = content.Load<SpriteFont>(FontName);

            Vector2 dimensions = Vector2.Zero;

            if (Texture != null)
            {
                dimensions.X += Texture.Width;
                dimensions.X += spriteFont.MeasureString(Text).X;
            }

            dimensions.Y = Texture != null ? Math.Max(Texture.Height, spriteFont.MeasureString(Text).Y) : spriteFont.MeasureString(Text).Y;

            if (SourceRect == Rectangle.Empty)
            {
                SourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);
            }

            renderTarget = new RenderTarget2D
                (ScreenManager.Instance.GraphicsDevice,
                (int)dimensions.X,
                (int)dimensions.Y);

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();
            if (Texture != null)
            {
                ScreenManager.Instance.SpriteBatch.Draw(Texture, Vector2.Zero, Color.White);
            }
            ScreenManager.Instance.SpriteBatch.DrawString(spriteFont, Text, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.End();

            Texture = renderTarget;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);
        }

        public void UnloadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.origin = new Vector2(this.SourceRect.Width / 2, this.SourceRect.Height / 2);
            spriteBatch.Draw(Texture,
                Position + origin,
                SourceRect,
                Color.White * Alpha,
                0.0f,
                origin,
                Scale,
                SpriteEffects.None,
                0.0f);
        }
    }
}