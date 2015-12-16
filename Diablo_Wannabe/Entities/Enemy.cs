using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Maps;
using Diablo_Wannabe.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Diablo_Wannabe.Entities
{
    public class Enemy : Unit
    {
        public Vector2 Velocity;
        public SpriteSheet[] sprites;
        public bool isMoving;
        public bool isHitting;
        public bool isAlive;
        public int health;
        public TimeSpan chrono;

        public Enemy()
        {
            this.Initialize();
        }

        public override void Move(GameTime gameTime)
        {
                if (this.Position.Y <= Map.Player.Position.Y)
                {
                    this.MoveDown(gameTime);
                }
                if (this.Position.Y > Map.Player.Position.Y)
                {
                    this.MoveUp(gameTime);
                }

                if (Math.Abs(this.Position.Y - Map.Player.Position.Y) < 2)
                {
                    if (this.Position.X <= Map.Player.Position.X)
                    {
                        this.MoveRight(gameTime);
                    }
                    else if (this.Position.X > Map.Player.Position.X)
                    {
                        this.MoveLeft(gameTime);
                    }
                }
        }

        public void TakeDamage(int damage, GameTime gameTime)
        {
            this.health -= damage;
            if (this.health <= 0 && isAlive)
            {
                this.Die(gameTime);
            }
        }

        private void Die(GameTime gameTime)
        {
            this.isAlive = false;
            this.isHitting = false;
            this.isMoving = false;
            this.sprites[2].CurrentFrame.Y = 0;
            this.sprites[2].CurrentFrame.X = 0;
            this.sprites[2].Position = this.Position;
            this.chrono = gameTime.TotalGameTime;
        }

        private void PlayDeathAnimation(GameTime gameTime)
        {
            if ((gameTime.TotalGameTime.Milliseconds - this.chrono.Milliseconds > 80 && this.sprites[2].CurrentFrame.X < 6)
                || (int)this.sprites[2].CurrentFrame.X == 0
                || (int)this.sprites[2].CurrentFrame.X == 1)
            {
                this.chrono = gameTime.TotalGameTime;
                this.sprites[2].CurrentFrame.X++;
            }
        }

        private void PlayHitAnimation(GameTime gameTime)
        {
            if (!isHitting)
            {
                this.chrono = gameTime.TotalGameTime;
                this.sprites[1].Position = this.Position;
                this.sprites[1].CurrentFrame.Y = this.sprites[0].CurrentFrame.Y;
                this.sprites[1].CurrentFrame.X = 0;
            }
            this.isHitting = true;
            if (gameTime.TotalGameTime.TotalMilliseconds - chrono.TotalMilliseconds < 800)
            {
                this.sprites[1].CurrentFrame.X += 60 / gameTime.ElapsedGameTime.Milliseconds * 0.04f;
                if (this.sprites[1].CurrentFrame.X > 6)
                {
                    this.sprites[1].CurrentFrame.X = 0;
                }
            }
            else
            {
                isHitting = false;
            }
        }

        private void MoveUp(GameTime gameTime)
        {
            this.Position.Y -= this.MovementSpeed;
            this.sprites[0].Position = this.Position;
            this.isHitting = false;
            this.sprites[0].CurrentFrame.Y = 0;
            this.sprites[0].CurrentFrame.X += 60 / gameTime.ElapsedGameTime.Milliseconds * 0.04f;
            if (this.sprites[0].CurrentFrame.X > 9 || this.sprites[0].CurrentFrame.X == 0)
            {
                this.sprites[0].CurrentFrame.X = 1;
            }
        }

        private void MoveDown(GameTime gameTime)
        {
            this.Position.Y += this.MovementSpeed;
            this.sprites[0].Position = this.Position;
            this.isHitting = false;
            this.sprites[0].CurrentFrame.Y = 2;
            this.sprites[0].CurrentFrame.X += 60 / gameTime.ElapsedGameTime.Milliseconds * 0.04f;
            if (this.sprites[0].CurrentFrame.X > 9 || this.sprites[0].CurrentFrame.X == 0)
            {
                this.sprites[0].CurrentFrame.X = 1;
            }
        }

        private void MoveLeft(GameTime gameTime)
        {
            this.Position.X -= this.MovementSpeed;
            this.sprites[0].Position = this.Position;
            this.isHitting = false;
            this.sprites[0].CurrentFrame.Y = 1;
            this.sprites[0].CurrentFrame.X += 60 / gameTime.ElapsedGameTime.Milliseconds * 0.04f;
            if (this.sprites[0].CurrentFrame.X > 9 || this.sprites[0].CurrentFrame.X == 0)
            {
                this.sprites[0].CurrentFrame.X = 1;
            }
        }

        private void MoveRight(GameTime gameTime)
        {
            this.Position.X += this.MovementSpeed;
            this.sprites[0].Position = this.Position;
            this.isHitting = false;
            this.sprites[0].CurrentFrame.Y = 3;
            this.sprites[0].CurrentFrame.X += 60 / gameTime.ElapsedGameTime.Milliseconds * 0.04f;
            if (this.sprites[0].CurrentFrame.X > 9 || this.sprites[0].CurrentFrame.X == 0)
            {
                this.sprites[0].CurrentFrame.X = 1;
            }
        }

        public void Initialize()
        {
            this.Position = new Vector2(700, 500);
            this.Velocity = Vector2.Zero;
            this.sprites = new SpriteSheet[3];
            this.sprites[0] = new SpriteSheet(9, 4, this.Position, "Entities/orc-mace-walking");
            this.sprites[1] = new SpriteSheet(8, 4, this.Position, "Entities/orc-mace-hitting");
            this.sprites[2] = new SpriteSheet(6, 1, this.Position, "Entities/orc-mace-death");
            this.LoadContent();
            this.MovementSpeed = 2;
            this.chrono = new TimeSpan();
            this.health = 100;
            this.isAlive = true;
        }

        public override void LoadContent()
        {
            foreach (var sprite in sprites)
            {
                sprite.LoadContent(ScreenManager.Manager.Content);
            }
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (!isAlive)
            {
                PlayDeathAnimation(gameTime);
                sprites[2].Update(gameTime);
                return;
            }

            if (CalculateDistance(this.Position, Map.Player.Position) < 150)
            {
                if (CalculateDistance(this.Position, Map.Player.Position) > 30)
                {
                    this.Move(gameTime);
                }
                else
                {
                    PlayHitAnimation(gameTime);
                }
            }
            if (isHitting)
            {
                sprites[1].Update(gameTime);
            }
            else
            {
                sprites[0].Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isHitting)
            {
                sprites[1].Draw(spriteBatch);
            }
            else if (!isAlive)
            {
                sprites[2].Draw(spriteBatch);
            }
            else
            {
                sprites[0].Draw(spriteBatch);
            }
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
    }
}