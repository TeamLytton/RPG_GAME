using System;
using System.Linq;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Interfaces;
using Diablo_Wannabe.Maps;
using Diablo_Wannabe.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Entities.Projectiles
{
    public class Arrow : IProjectile
    {
        private const int DefaultFlySpeed = 9;
        private const int DefaultRadius = 5;

        private Vector2 direction;

        public Arrow(Vector2 position, Vector2 direction, Vector2 mousePos)
        {
            this.FlySpeed = DefaultFlySpeed;
            this.Radius = DefaultRadius;
            this.Position = position;
            this.Sprite = new SpriteSheet(1, 1, Position, "Projectiles/arrow");
            this.Sprite.LoadContent(ScreenManager.Instance.Content);
            this.Sprite.RotationAngle = 
                (float)((Math.PI * 0.5f) + 
                Math.Atan2
                (mousePos.Y - this.Position.Y,                                           
                 mousePos.X - this.Position.X));
            this.Exists = true;
            this.direction = direction;
        }

        public SpriteSheet Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public int FlySpeed { get; set; }
        public int Radius { get; set; }
        public bool Exists { get; set; }

        public void Fly(int damage, GameTime gameTime)
        {
            if (this.Exists)
            {
                if (direction != Vector2.Zero)
                {
                    direction.Normalize();
                }
                this.Velocity = Vector2.Zero;
                this.Velocity += direction * this.FlySpeed;
                this.Position += Velocity;
                this.Sprite.Position = this.Position;

                if (Map.Enemies.Any(e => e.BoundingBox.Contains(this.Position.ToPoint())))
                {
                    Map.Enemies.First(e => e.BoundingBox.Contains(this.Position))
                        .TakeDamage(damage,gameTime);
                    this.Exists = false;
                }

                if (Map.tiles.Any(t => !t.isPassable && t.BoundingBox.Contains(this.Position)))
                {
                    this.Exists = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.Exists)
            {
                this.Sprite.Draw(spriteBatch);
            }
        }
    }
}