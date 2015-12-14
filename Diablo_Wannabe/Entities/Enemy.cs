using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Map;
using Diablo_Wannabe.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Diablo_Wannabe.Entities
{
    public class Enemy : Unit
    {
        public Vector2 Velocity;
        public bool IsActive;
        public SpriteSheet EnemySprite;

        public Enemy()
        {
            this.IsActive = false;
            this.Position = new Vector2(720, 400);
            this.EnemySprite = new SpriteSheet(5, 1, this.Position, "Entities/spritesheet");
            EnemySprite.LoadContent(ScreenManager.Manager.Content);
            this.MovementSpeed = 3;
            this.Velocity = Vector2.Zero;

        }

        public override void LoadContent()
        {
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Move(GameTime gameTime, List<Tile> tiles)
        {
            Vector2 direction = ScreenManager.Manager.GameScreen.player.Position - this.Position;
            float distance = CalculateDistance(ScreenManager.Manager.GameScreen.player.Position, this.Position);
            if (distance >= 30)
            {
                this.EnemySprite.RotationAngle = (float)((Math.PI * 0.5f) + Math.Atan2(ScreenManager.Manager.GameScreen.player.Position.Y - this.Position.Y, ScreenManager.Manager.GameScreen.player.Position.X - this.Position.X));
            }
            else
            {
                this.EnemySprite.CurrentFrame.X = 0;
            }

            if (IsActive)
            {
                this.Velocity = Vector2.Zero;

                if (direction != Vector2.Zero)
                {
                    direction.Normalize();
                }

                this.EnemySprite.CurrentFrame.X += 60 / gameTime.ElapsedGameTime.Milliseconds * 0.04f;
                if (this.EnemySprite.CurrentFrame.X > 5 || this.EnemySprite.CurrentFrame.X == 0)
                {
                    this.EnemySprite.CurrentFrame.X = 1;
                }

                if (distance < this.MovementSpeed)
                {
                    this.Velocity += direction * distance;
                }
                else
                {
                    this.Velocity += direction * this.MovementSpeed;
                }

                Debug.WriteLine("E" + this.Position.X + " " + this.Position.Y);

                if (CheckForCollision(tiles))
                {
                    if (Math.Abs((this.Position.X + this.Velocity.X) - ScreenManager.Manager.GameScreen.player.Position.X) > 20 
                        && Math.Abs((this.Position.Y + this.Velocity.Y) - ScreenManager.Manager.GameScreen.player.Position.Y) > 20)
                    {
                        this.Position += this.Velocity;
                        this.EnemySprite.Position = this.Position;
                    }
                    
                }
                else
                {
                    Debug.WriteLine("I az se blusnah");
                }

            }
        }

        public override void Update(GameTime gameTime, List<Tile> tiles)
        {
            if (Math.Abs(ScreenManager.Manager.GameScreen.player.Position.X - this.Position.X) <= 150
                && Math.Abs(ScreenManager.Manager.GameScreen.player.Position.X - this.Position.X) <= 150)
            {
                this.IsActive = true;
                this.Move(gameTime, tiles);
                this.EnemySprite.Update(gameTime, IsActive);
            }
            else
            {
                this.IsActive = false;
                this.EnemySprite.CurrentFrame.X = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            EnemySprite.Draw(spriteBatch);
        }

        private float CalculateDistance(Vector2 A, Vector2 B)
        {
            A = new Vector2(Math.Abs(A.X), Math.Abs(A.Y));
            B = new Vector2(Math.Abs(B.X), Math.Abs(B.Y));

            float xDiff, yDiff, distance;
            xDiff = A.X - B.X;
            yDiff = A.Y - B.Y;

            distance = (float)Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

            return Math.Abs(distance);
        }

        private bool CheckForCollision(List<Tile> tiles)
        {
            return tiles.First(t => t.TileSprite.Position.X <= this.Position.X + 30 + this.Velocity.X
                       && t.TileSprite.Position.Y <= this.Position.Y + 30 + this.Velocity.Y
                       && t.TileSprite.Position.X + 65 > this.Position.X + 30 + this.Velocity.X
                       && t.TileSprite.Position.Y + 65 > this.Position.Y + 30 + this.Velocity.Y).isPassable;
        }
    }
}