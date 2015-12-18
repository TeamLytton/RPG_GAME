using Diablo_Wannabe.Entities.Characters;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Interfaces;
using Diablo_Wannabe.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Entities.Items
{
    public abstract class Item : IItem
    {
        private const int framesX = 12;
        private const int framesY = 8;
        private const string path = "items/items";

        public bool GotPicked { get; set; }

        public Vector2 Position { get; set; }

        public SpriteSheet Sprite { get; set; }

        protected Item(Vector2 position)
        {
            this.Position = position;
            this.Sprite = new SpriteSheet(framesX, framesY, Position, path);
            this.Sprite.LoadContent(ScreenManager.Manager.Content);
            this.GotPicked = false;
        }

        public virtual void GetPicked(Player player)
        {
            this.GotPicked = true;
            player.Items.Add(this);
        }

        public abstract void Use(Player player);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!GotPicked)
            {
                this.Sprite.Draw(spriteBatch);
            }
        }
    }
}