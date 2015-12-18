using System;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Screens
{
    public class Screen
    {
        protected ContentManager content;


        [XmlIgnore]
        public Type Type;

        public Screen()
        {
            this.Type = this.GetType();
        }

        public virtual void LoadContent()
        {
            this.content =
                new ContentManager(
                ScreenManager.Instance.Content.ServiceProvider,
                ScreenManager.Instance.Content.RootDirectory);
        }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}