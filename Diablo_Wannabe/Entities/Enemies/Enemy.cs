using System;
using System.Diagnostics;
using System.Linq;
using Diablo_Wannabe.Entities.Items;
using Diablo_Wannabe.Entities.StatsBars;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Maps;
using Diablo_Wannabe.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Entities.Enemies
{
    public abstract class Enemy : Unit
    {
        private HealthBar healthBar;
        private Random rnd;

        protected Enemy(Vector2 position, string path, int movementSpeed, int health, int weaponRange, int armor, int damage)
        {
            this.Position = position;
            this.Sprites = new SpriteSheet[3];
            this.Sprites[0] = new SpriteSheet(9, 4, this.Position, path + "walking");
            this.Sprites[1] = new SpriteSheet(8, 4, this.Position, path + "hitting");
            this.Sprites[2] = new SpriteSheet(6, 1, this.Position, path + "death");
            this.MovementSpeed = movementSpeed;
            this.MaxHealth = health;
            this.Health = health;
            this.Armor = armor;
            this.Damage = damage;
            this.WeaponRange = weaponRange;
            this.IsAlive = true;
            this.LastAction = new TimeSpan();
            this.LastTimeDamageTaken = new TimeSpan();
            this.healthBar = new HealthBar(this.Position);
            this.LoadContent();
            this.BoundingBox = new Rectangle((int)this.Position.X - 16, (int)this.Position.Y - 16,
               (int)this.Sprites[0].FrameDimensions.X/3, (int)this.Sprites[0].FrameDimensions.Y/3);
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
            this.DropItem();
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
            if (gameTime.TotalGameTime.TotalMilliseconds - LastAction.TotalMilliseconds > 100
                && (int)this.Sprites[1].CurrentFrame.X != 5)
            {
                this.Sprites[1].CurrentFrame.X += 1;
                if ((int)this.Sprites[1].CurrentFrame.X == 4)
                {
                    CheckForPlayerHit(gameTime);
                }
                LastAction = gameTime.TotalGameTime;
            }
            else if ((int)this.Sprites[1].CurrentFrame.X == 5)
            {
                IsHitting = false;
            }
        }

        private void CheckForPlayerHit(GameTime gameTime)
        {
            if ((int)this.Sprites[1].CurrentFrame.Y == 0)
            {
                    if (Math.Abs(this.Position.X - Map.Player.Position.X) < 40
                        && this.Position.Y > Map.Player.Position.Y
                        && this.Position.Y - WeaponRange < Map.Player.Position.Y)
                    {
                        Map.Player.TakeDamage(this.Damage, gameTime);
                    }
            }
            else if ((int)this.Sprites[1].CurrentFrame.Y == 1)
            {
                    if (Math.Abs(this.Position.Y - Map.Player.Position.Y) < 40
                        && this.Position.X > Map.Player.Position.X
                        && this.Position.X - WeaponRange < Map.Player.Position.X)
                    {
                        Map.Player.TakeDamage(this.Damage, gameTime);
                    }
            }
            else if ((int)this.Sprites[1].CurrentFrame.Y == 2)
            {
                    if (Math.Abs(this.Position.X - Map.Player.Position.X) < 40
                        && this.Position.Y < Map.Player.Position.Y
                        && this.Position.Y + WeaponRange > Map.Player.Position.Y)
                    {
                        Map.Player.TakeDamage(this.Damage, gameTime); ;
                    }
            }
            else if ((int)this.Sprites[1].CurrentFrame.Y == 3)
            {
                    if (Math.Abs(this.Position.Y - Map.Player.Position.Y) < 40
                        && this.Position.X < Map.Player.Position.X
                        && this.Position.X + WeaponRange > Map.Player.Position.X)
                    {
                        Map.Player.TakeDamage(this.Damage, gameTime); ;
                    }
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
                Sprites[2].Update();
                this.BoundingBox = Rectangle.Empty;
                return;
            }

            this.BoundingBox.X = (int)this.Position.X - 16;
            this.BoundingBox.Y = (int)this.Position.Y - 16;
            Debug.WriteLine("ER - " + this.BoundingBox.X + " " + this.BoundingBox.Y);
            Debug.WriteLine("EP - " + this.Position.X + " " + this.Position.Y);

            if (CalculateDistance(this.Position, Map.Player.Position) < 150)
            {
                if (CalculateDistance(this.Position, Map.Player.Position) > 40)
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
                Sprites[1].Update();
            }
            else
            {
                Sprites[0].Update();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            if (IsHitting)
            {
                Sprites[1].Draw(spriteBatch);
                this.healthBar.Draw(spriteBatch, this.Health, this.MaxHealth, this.Position);

            }
            else if (!IsAlive)
            {
                Sprites[2].Draw(spriteBatch);
            }
            else
            {
                Sprites[0].Draw(spriteBatch);
                this.healthBar.Draw(spriteBatch, this.Health, this.MaxHealth, this.Position);

            }
        }

        private bool CheckForCollision(int movementX, int movementY)
        {
            bool canPass = true;

            Rectangle someRect = this.BoundingBox;
            someRect.X += movementX;
            someRect.Y += movementY;

            if (Map.tiles.Any(t => t.BoundingBox.Intersects(someRect)))
            {
                canPass = false;
            }

            if (Map.Enemies.Any(e => e.BoundingBox.Intersects(someRect)
                                && e != this))
            {
                canPass = false;
            }

            if (Map.Player.BoundingBox.Intersects(someRect))
            {
                canPass = false;
            }
            return canPass;
        }

        public void DropItem()
        {
            Random rnd = new Random();

            int id = rnd.Next(0, 4);

            switch (id)
            {
                case 0:
                    Map.DroppedItems.Add(new HealthIncreaseCrystal(this.Position));
                    break;
                case 1:
                    Map.DroppedItems.Add(new ArmorIncreaseCrystal(this.Position));
                    break;
                case 2:
                    Map.DroppedItems.Add(new DamageIncreaseCrystal(this.Position));
                    break;
                case 3:
                    Map.DroppedItems.Add(new HealingPotion(this.Position));
                    break;
            }
        }
    }
}