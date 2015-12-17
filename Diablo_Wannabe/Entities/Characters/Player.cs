
using System;
using System.Linq;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Maps;
using Diablo_Wannabe.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Diablo_Wannabe.Entities.Characters
{
    public class Player : Unit
    {
        public Player(string path, int movementSpeed, int weaponRange, int health, int armor, int damage)
        {
            this.Position = new Vector2(ScreenManager.Manager.Dimensions.X / 2, ScreenManager.Manager.Dimensions.Y - 20);
            this.Sprites = new SpriteSheet[4];
            this.Sprites[0] = new SpriteSheet(9, 4, this.Position, path + "walk");
            this.Sprites[1] = new SpriteSheet(8, 4, this.Position, path + "hitting");
            this.Sprites[2] = new SpriteSheet(7, 4, this.Position, path + "spellcast");
            this.Sprites[3] = new SpriteSheet(6, 1, this.Position, path + "death");
            this.MovementSpeed = movementSpeed;
            this.WeaponRange = weaponRange;
            this.Health = health;
            this.Armor = armor;
            this.Damage = damage;
            this.LastAction = new TimeSpan();
            this.LastTimeDamageTaken = new TimeSpan();
            this.LoadContent();
        }

        public override void Move(GameTime gameTime)
        {
            if (!Input.Manager.KeyDown(Keys.Left, Keys.Right))
            {
                if (Input.Manager.KeyDown(Keys.Up))
                {
                    this.IsMoving = true;
                    MoveUp(gameTime);
                }
                if (Input.Manager.KeyDown(Keys.Down))
                {
                    this.IsMoving = true;
                    MoveDown(gameTime);
                }
            }
            if (!Input.Manager.KeyDown(Keys.Up, Keys.Down))
            {
                if (Input.Manager.KeyDown(Keys.Left))
                {
                    this.IsMoving = true;
                    MoveLeft(gameTime);
                }
                if (Input.Manager.KeyDown(Keys.Right))
                {
                    this.IsMoving = true;
                    MoveRight(gameTime);
                }
            }
            if (Input.Manager.KeyPressed(Keys.Space) || IsHitting)
            {
                PlayHitAnimation(gameTime);
            }

        }

        private void PlayHitAnimation(GameTime gameTime)
        {
            if (!IsHitting)
            {
                this.LastAction = gameTime.TotalGameTime;
                this.Sprites[1].Position = this.Position;
                this.Sprites[1].CurrentFrame.Y = this.Sprites[0].CurrentFrame.Y;
                this.Sprites[1].CurrentFrame.X = 0;
            }
            this.IsHitting = true;
            if (gameTime.TotalGameTime.TotalMilliseconds - LastAction.TotalMilliseconds > 100
                && (int)this.Sprites[1].CurrentFrame.X != 5)
            {
                this.Sprites[1].CurrentFrame.X += 1;
                if ((int)this.Sprites[1].CurrentFrame.X == 4)
                {
                    CheckForEnemyHit(gameTime);
                }
                LastAction = gameTime.TotalGameTime;
            }
            else if ((int)this.Sprites[1].CurrentFrame.X == 5)
            {
                IsHitting = false;
            }
        }

        private void CheckForEnemyHit(GameTime gameTime)
        {
            if ((int)this.Sprites[1].CurrentFrame.Y == 0)
            {
                Map.Enemies.ForEach(e =>
                {
                    if (Math.Abs(this.Position.X - e.Position.X) < 40
                        && this.Position.Y > e.Position.Y
                        && this.Position.Y - WeaponRange < e.Position.Y)
                    {
                        e.TakeDamage(this.Damage, gameTime);;
                    }
                });
            }
            else if ((int)this.Sprites[1].CurrentFrame.Y == 1)
            {
                Map.Enemies.ForEach(e =>
                {
                    if (Math.Abs(this.Position.Y - e.Position.Y) < 40
                        && this.Position.X > e.Position.X
                        && this.Position.X - WeaponRange < e.Position.X)
                    {
                        e.TakeDamage(this.Damage, gameTime);;
                    }
                });
            }
            else if ((int)this.Sprites[1].CurrentFrame.Y == 2)
            {
                Map.Enemies.ForEach(e =>
                {
                    if (Math.Abs(this.Position.X - e.Position.X) < 40
                        && this.Position.Y < e.Position.Y 
                        && this.Position.Y + WeaponRange > e.Position.Y )
                    {
                        e.TakeDamage(this.Damage, gameTime);;

                    }
                });
            }
            else if ((int)this.Sprites[1].CurrentFrame.Y == 3)
            {
                Map.Enemies.ForEach(e =>
                {
                    if (Math.Abs(this.Position.Y - e.Position.Y) < 40
                        && this.Position.X < e.Position.X
                        && this.Position.X + WeaponRange > e.Position.X)
                    {
                        e.TakeDamage(this.Damage, gameTime);;

                    }
                });
            }
        }

        private void MoveUp(GameTime gameTime)
        {
            if (this.CheckForCollision(0, (int) MovementSpeed*(-1)))
            {
                this.Position.Y -= this.MovementSpeed;
                this.Sprites[0].Position = this.Position;
                this.Sprites[0].CurrentFrame.Y = 0;
                this.Sprites[0].CurrentFrame.X += 60/gameTime.ElapsedGameTime.Milliseconds*0.04f;
                if (this.Sprites[0].CurrentFrame.X > 9 || this.Sprites[0].CurrentFrame.X == 0)
                {
                    this.Sprites[0].CurrentFrame.X = 1;
                }
            }
        }

        private void MoveDown(GameTime gameTime)
        {
            if (this.CheckForCollision(0, (int)this.MovementSpeed))
            {
                this.Position.Y += this.MovementSpeed;
                this.Sprites[0].Position = this.Position;
                this.Sprites[0].CurrentFrame.Y = 2;
                this.Sprites[0].CurrentFrame.X += 60/gameTime.ElapsedGameTime.Milliseconds*0.04f;
                if (this.Sprites[0].CurrentFrame.X > 9 || this.Sprites[0].CurrentFrame.X == 0)
                {
                    this.Sprites[0].CurrentFrame.X = 1;
                }
            }
        }

        private void MoveLeft(GameTime gameTime)
        {
            if (this.CheckForCollision((int)-this.MovementSpeed, 0))
            {
                this.Position.X -= this.MovementSpeed;
                this.Sprites[0].Position = this.Position;
                this.Sprites[0].CurrentFrame.Y = 1;
                this.Sprites[0].CurrentFrame.X += 60/gameTime.ElapsedGameTime.Milliseconds*0.04f;
                if (this.Sprites[0].CurrentFrame.X > 9 || this.Sprites[0].CurrentFrame.X == 0)
                {
                    this.Sprites[0].CurrentFrame.X = 1;
                }
            }
        }

        private void MoveRight(GameTime gameTime)
        {
            if (this.CheckForCollision((int)this.MovementSpeed, 0))
            {
                this.Position.X += this.MovementSpeed;
                this.Sprites[0].Position = this.Position;
                this.Sprites[0].CurrentFrame.Y = 3;
                this.Sprites[0].CurrentFrame.X += 60/gameTime.ElapsedGameTime.Milliseconds*0.04f;
                if (this.Sprites[0].CurrentFrame.X > 9 || this.Sprites[0].CurrentFrame.X == 0)
                {
                    this.Sprites[0].CurrentFrame.X = 1;
                }
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
            this.Move(gameTime);
            if (IsHitting)
            {
                Sprites[1].Update(gameTime);
            }
            else
            {
                Sprites[0].Update(gameTime);
            }
        }

        private new void Die()
        {
            throw new NotImplementedException();
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsHitting)
            {
                Sprites[1].Draw(spriteBatch);
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