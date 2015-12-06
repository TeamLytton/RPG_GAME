

using System;
using System.Xml.Serialization;
using Diablo_Wannabe.ImageProcessing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Entities
{
    public class Player : Unit
    {
        public Vector2 Velocity;

        [XmlIgnore]
        public Type Type;

        public Player()
        {
            this.Type = this.GetType();
            this.Initialize();
        }

        public override void Move()
        {
            //throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            this.Velocity = Vector2.Zero;
        }

        public override void LoadContent()
        {
            Image.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            this.Move();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.Image.Draw(spriteBatch);
        }
    }
}