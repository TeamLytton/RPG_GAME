using System;
using System.Linq;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Maps;
using Diablo_Wannabe.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Entities.Enemies
{
    public abstract class Enemy : Unit
    {

        protected Enemy(Vector2 position, string path, int movementSpeed, int health, int weaponRange, int armor, int damage)
        {
            this.Position = position;
            this.Sprites = new SpriteSheet[3];
            this.Sprites[0] = new SpriteSheet(9, 4, this.Position, path + "walking");
            this.Sprites[1] = new SpriteSheet(8, 4, this.Position, path + "hitting");
            this.Sprites[2] = new SpriteSheet(6, 1, this.Position, path + "death");
            this.MovementSpeed = movementSpeed;
            this.Health = health;
            this.Armor = armor;
            this.Damage = damage;
            this.WeaponRange = weaponRange;
            this.IsAlive = true;
            this.LastAction = new TimeSpan();
            this.LastTimeDamageTaken = new TimeSpan();
            this.LoadContent();
        }

        public new void Move(GameTime gameTime)
        {
                if (this.Position.Y <= Map.Player.Position.Y)
                {
                    if (CheckForCollision(0, (int)this.MovementSpeed))
                    {
                        this.MoveDown(gameTime);
                    }
                }
                if (this.Position.Y > Map.Player.Position.Y)
                {
                    if (CheckForCollision(0, (int) -this.MovementSpeed))
                    {
                        this.MoveUp(gameTime);
                    }
                }

                if (Math.Abs(this.Position.Y - Map.Player.Position.Y) < 2)
                {
                    if (this.Position.X <= Map.Player.Position.X)
                    {
                        if (CheckForCollision((int)this.MovementSpeed, 0))
                        {
                            this.MoveRight(gameTime);
                        }
                    }
                    else if (this.Position.X > Map.Player.Position.X)
                    {
                        if (CheckForCollision((int)-this.MovementSpeed,0))
                        {
                            this.MoveLeft(gameTime);
                        }
                    }
                }
        }

        public void TakeDamage(int damage, GameTime gameTime)
        {
            if (damage - Armor > 0)
            {
                this.Health -= damage - Armor;
            }
            if (this.Health <= 0 && IsAlive)
            {
                this.Die(gameTime);
            }
        }

        protected virtual void Die(GameTime gameTime)
        {
            this.IsAlive = false;
            this.IsHitting = false;
            this.IsMoving = false;
            this.Sprites[2].CurrentFrame.Y = 0;
            this.Sprites[2].CurrentFrame.X = 0;
            this.Sprites[2].Position = this.Position;
            this.LastAction = gameTime.TotalGameTime;
        }

        protected virtual void PlayDeathAnimation(GameTime gameTime)
        {
            if ((gameTime.TotalGameTime.Milliseconds - this.LastAction.Milliseconds > 80 && this.Sprites[2].CurrentFrame.X < 6)
                || (int)this.Sprites[2].CurrentFrame.X == 0
                || (int)this.Sprites[2].CurrentFrame.X == 1)
            {
                this.LastAction = gameTime.TotalGameTime;
                this.Sprites[2].CurrentFrame.X++;
            }
        }

        protected virtual void PlayHitAnimation(GameTime gameTime)
        {
            if (!IsHitting)
            {
                this.LastAction = gameTime.TotalGameTime;
                this.Sprites[1].Position = this.Position;
                this.Sprites[1].CurrentFrame.Y = this.Sprites[0].CurrentFrame.Y;
                this.Sprites[1].CurrentFrame.X = 0;
            }
            this.IsHitting = true;
            if (gameTime.TotalGameTime.TotalMilliseconds - LastAction.TotalMilliseconds < 800)
            {
                this.Sprites[1].CurrentFrame.X += 60 / gameTime.ElapsedGameTime.Milliseconds * 0.04f;
                if (this.Sprites[1].CurrentFrame.X > 6)
                {
                    this.Sprites[1].CurrentFrame.X = 0;
                }
            }
            else
            {
                IsHitting = false;
            }
        }

        protected virtual void MoveUp(GameTime gameTime)
        {
            this.Position.Y -= this.MovementSpeed;
            this.Sprites[0].Position = this.Position;
            this.IsHitting = false;
            this.Sprites[0].CurrentFrame.Y = 0;
            this.Sprites[0].CurrentFrame.X += 60 / gameTime.ElapsedGameTime.Milliseconds * 0.04f;
            if (this.Sprites[0].CurrentFrame.X > 9 || this.Sprites[0].CurrentFrame.X == 0)
            {
                this.Sprites[0].CurrentFrame.X = 1;
            }
        }

        protected virtual void MoveDown(GameTime gameTime)
        {
            this.Position.Y += this.MovementSpeed;
            this.Sprites[0].Position = this.Position;
            this.IsHitting = false;
            this.Sprites[0].CurrentFrame.Y = 2;
            this.Sprites[0].CurrentFrame.X += 60 / gameTime.ElapsedGameTime.Milliseconds * 0.04f;
            if (this.Sprites[0].CurrentFrame.X > 9 || this.Sprites[0].CurrentFrame.X == 0)
            {
                this.Sprites[0].CurrentFrame.X = 1;
            }
        }

        protected virtual void MoveLeft(GameTime gameTime)
        {
            this.Position.X -= this.MovementSpeed;
            this.Sprites[0].Position = this.Position;
            this.IsHitting = false;
            this.Sprites[0].CurrentFrame.Y = 1;
            this.Sprites[0].CurrentFrame.X += 60 / gameTime.ElapsedGameTime.Milliseconds * 0.04f;
            if (this.Sprites[0].CurrentFrame.X > 9 || this.Sprites[0].CurrentFrame.X == 0)
            {
                this.Sprites[0].CurrentFrame.X = 1;
            }
        }

        private void MoveRight(GameTime gameTime)
        {
            this.Position.X += this.MovementSpeed;
            this.Sprites[0].Position = this.Position;
            this.IsHitting = false;
            this.Sprites[0].CurrentFrame.Y = 3;
            this.Sprites[0].CurrentFrame.X += 60 / gameTime.ElapsedGameTime.Milliseconds * 0.04f;
            if (this.Sprites[0].CurrentFrame.X > 9 || this.Sprites[0].CurrentFrame.X == 0)
            {
                this.Sprites[0].CurrentFrame.X = 1;
            }
        }

        public override void LoadContent()
        {
            foreach (var sprite in Sprites)
            {
                sprite.LoadContent(ScreenManager.Manager.Content);
            }
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsAlive)
            {
                PlayDeathAnimation(gameTime);
                Sprites[2].Update(gameTime);
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
            if (IsHitting)
            {
                Sprites[1].Update(gameTime);
            }
            else
            {
                Sprites[0].Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsHitting)
            {
                Sprites[1].Draw(spriteBatch);
            }
            else if (!IsAlive)
            {
                Sprites[2].Draw(spriteBatch);
            }
            else
            {
                Sprites[0].Draw(spriteBatch);
            }
        }

        private bool CheckForCollision(int movementX, int movementY)
        {
            return Map.tiles.Any(t => t.TileSprite.Position.X - 16 <= this.Position.X + movementX
                       && t.TileSprite.Position.Y - 16 <= this.Position.Y + movementY
                       && t.TileSprite.Position.X + 16 > this.Position.X + movementX
                       && t.TileSprite.Position.Y + 16 > this.Position.Y + movementY
                       && t.isPassable);
        }
    }
}