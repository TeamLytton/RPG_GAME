﻿
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Diablo_Wannabe.Entities.Items;
using Diablo_Wannabe.Entities.StatsBars;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Interfaces;
using Diablo_Wannabe.Maps;
using Diablo_Wannabe.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Diablo_Wannabe.Entities.Characters
{
    public class Player : Unit
    {
        public HealthBar HealthBar;

        public List<IItem> Items { get; set; }

        public Player(string path, int movementSpeed, int weaponRange, int health, int armor, int damage)
        {
            this.Position = new Vector2(ScreenManager.Manager.Dimensions.X / 2, ScreenManager.Manager.Dimensions.Y - 20);
            this.Sprites = new SpriteSheet[4];
            this.Sprites[0] = new SpriteSheet(9, 4, this.Position, path + "walk");
            this.Sprites[1] = new SpriteSheet(8, 4, this.Position, path + "hitting");
            this.Sprites[2] = new SpriteSheet(7, 4, this.Position, path + "spellcast");
            this.Sprites[3] = new SpriteSheet(6, 1, this.Position, path + "death");
            this.IsAlive = true;
            this.MovementSpeed = movementSpeed;
            this.WeaponRange = weaponRange;
            this.MaxHealth = health;
            this.Health = health;
            this.Armor = armor;
            this.Damage = damage;
            this.LastAction = new TimeSpan();
            this.LastTimeDamageTaken = new TimeSpan();
            this.HealthBar = new HealthBar(new Vector2(this.Position.X, this.Position.Y - 40));         
            this.Items = new List<IItem>();  
            this.LoadContent();
            this.BoundingBox = new Rectangle((int)this.Position.X - 16, (int)this.Position.Y - 16,
                (int)this.Sprites[0].FrameDimensions.X/2, (int)this.Sprites[0].FrameDimensions.Y/2);
        }

        public void UseItem(IItem item)
        {
            item.Use(this);
            this.Items.Remove(item);
        }

        public void PickItem()
        {
            Map.DroppedItems
                .Where(d => this.CalculateDistance(this.Position, d.Position) < 30
                            && d.GotPicked == false)
                .ToList().ForEach(i => i.GetPicked(this));
        }

        public override void Move(GameTime gameTime)
        {
            if (!IsHitting)
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
                if (Input.Manager.KeyPressed(Keys.F))
                {
                    this.PickItem();

                    this.Items.Where(i => i is StatCrystal
                                        && !((StatCrystal)i).HasBeenUsed).ToList().ForEach(i => i.Use(this));
                }
                if (Input.Manager.KeyPressed(Keys.D1))
                {
                    if (this.Items.Any(i => i.GetType().Name == "HealingPotion"))
                    {
                        this.UseItem(this.Items.First(i => i.GetType().Name == "HealingPotion"));
                    }
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

        public void TakeDamage(int damage, GameTime gameTime)
        {
            if (this.IsAlive)
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
        }

        protected virtual void Die(GameTime gameTime)
        {
            this.IsAlive = false;
            this.IsHitting = false;
            this.IsMoving = false;
            this.Sprites[3].CurrentFrame.Y = 0;
            this.Sprites[3].CurrentFrame.X = 0;
            this.Sprites[3].Position = this.Position;
            this.LastAction = gameTime.TotalGameTime;
        }

        protected virtual void PlayDeathAnimation(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - LastAction.TotalMilliseconds > 100
                && this.Sprites[3].CurrentFrame.X < 6)
            {
                this.LastAction = gameTime.TotalGameTime;
                this.Sprites[3].CurrentFrame.X++;
                this.Sprites[3].Update();
            }
        }

        private void MoveUp(GameTime gameTime)
        {
            if (this.CheckForCollision(0, (int)-MovementSpeed))
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
            if (this.CheckForCollision(0,(int)this.MovementSpeed))
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
            if (IsAlive)
            {
                this.Move(gameTime);
                this.BoundingBox.X = (int)this.Position.X - 16;
                this.BoundingBox.Y = (int)this.Position.Y - 16;
                if (IsHitting)
                {
                    Sprites[1].Update();
                }
                else if (IsMoving)
                {
                    Sprites[0].Update();

                }

                Debug.WriteLine("PR - " + this.BoundingBox.X + " " + this.BoundingBox.Y);
                Debug.WriteLine("PP - " + this.Position.X + " " + this.Position.Y);
            }
            else
            {
                PlayDeathAnimation(gameTime);
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                this.HealthBar.Draw(spriteBatch, this.Health, this.MaxHealth, this.Position);

                if (IsHitting)
                {
                    Sprites[1].Draw(spriteBatch);
                }
                else
                {
                    Sprites[0].Draw(spriteBatch);
                }
            }
            else
            {
                Sprites[3].Draw(spriteBatch);
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

            if (Map.Enemies.Any(e => e.BoundingBox.Intersects(someRect)))
            {
                canPass = false;
            }
            return canPass;
        }
    }
}