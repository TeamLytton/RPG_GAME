
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
        private Dictionary<string, ImageEffect> allEffects;

        public float Alpha;
        public string Text, FontName;
        public string Effect;
        public bool IsActive;
        public FadeEffect FadeEffect;

        [XmlElement("Path")]
        public string Path;

        public Vector2 Position, Scale;

        [XmlIgnore]
        public Texture2D Texture;
        public Rectangle SourceRect;

        private void SetEffect<T>(ref T effect)
        {
            if (effect == null)
            {
                effect = (T)Activator.CreateInstance(typeof(T));
            }
            else
            {
                (effect as ImageEffect).IsActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }

            allEffects.Add(effect.GetType().ToString().Replace("Diablo_Wannabe.", ""), (effect as ImageEffect));
        }

        public void ActivateEffect(string effect)
        {
            if (!allEffects.ContainsKey(effect))
            {
                return;
            }

            allEffects[effect].IsActive = true;
            var obj = this;
            allEffects[effect].LoadContent(ref obj);
        }

        public void StopEffect(string effect)
        {
            if (!allEffects.ContainsKey(effect))
            {
                return;
            }

            allEffects[effect].IsActive = false;
            allEffects[effect].UnloadContent();
        }

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
            this.allEffects = new Dictionary<string, ImageEffect>();
        }

        public void LoadContent()
        {
            content =
                new ContentManager
                (ScreenManager.Manager.Content.ServiceProvider,
                 ScreenManager.Manager.Content.RootDirectory);

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
                (ScreenManager.Manager.GraphicsDevice,
                (int)dimensions.X,
                (int)dimensions.Y);

            ScreenManager.Manager.GraphicsDevice.SetRenderTarget(renderTarget);
            ScreenManager.Manager.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Manager.SpriteBatch.Begin();
            if (Texture != null)
            {
                ScreenManager.Manager.SpriteBatch.Draw(Texture, Vector2.Zero, Color.White);
            }
            ScreenManager.Manager.SpriteBatch.DrawString(spriteFont, Text, Vector2.Zero, Color.White);
            ScreenManager.Manager.SpriteBatch.End();

            Texture = renderTarget;

            ScreenManager.Manager.GraphicsDevice.SetRenderTarget(null);
            SetEffect<FadeEffect>(ref FadeEffect);
            if (Effect != string.Empty)
            {
                string[] split = Effect.Split(':');
                foreach (var item in split)
                {
                    ActivateEffect(item);
                }
            }
        }

        public void UnloadContent()
        {
            content.Unload();
            foreach (var effect in allEffects)
            {
                effect.Value.UnloadContent();
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var effect in allEffects)
            {
                effect.Value.Update(gameTime);
            }
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