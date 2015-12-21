
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        private StringBuilder sb;

        public HealthBar HealthBar;

        public List<IItem> Items { get; set; }

        public Player(int movementSpeed, int weaponRange, int health, int armor, int damage, int attackRate)
        {
            this.Position = new Vector2((int)(ScreenManager.Instance.Dimensions.X / 6), (int)(ScreenManager.Instance.Dimensions.Y / 2));
            this.Sprites = new SpriteSheet[3];
            this.IsAlive = true;
            this.AttackRate = attackRate;
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
                if (!Input.Instance.KeyDown(Keys.Left, Keys.Right))
                {
                    if (Input.Instance.KeyDown(Keys.Up))
                    {
                        this.IsMoving = true;
                        MoveUp(gameTime);
                    }
                    if (Input.Instance.KeyDown(Keys.Down))
                    {
                        this.IsMoving = true;
                        MoveDown(gameTime);
                    }
                }
                if (!Input.Instance.KeyDown(Keys.Up, Keys.Down))
                {
                    if (Input.Instance.KeyDown(Keys.Left))
                    {
                        this.IsMoving = true;
                        MoveLeft(gameTime);
                    }
                    if (Input.Instance.KeyDown(Keys.Right))
                    {
                        this.IsMoving = true;
                        MoveRight(gameTime);
                    }
                }
                if (Input.Instance.KeyPressed(Keys.F))
                {
                    this.PickItem();

                    this.Items.Where(i => i is StatCrystal
                                        && !((StatCrystal)i).HasBeenUsed).ToList().ForEach(i => i.Use(this));
                }
                if (Input.Instance.KeyPressed(Keys.D1))
                {
                    if (this.Items.Any(i => i.GetType().Name == "HealingPotion"))
                    {
                        this.UseItem(this.Items.First(i => i.GetType().Name == "HealingPotion"));
                    }
                }
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
                else
                {
                    this.Health -= 2;
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
            this.Sprites[2].CurrentFrame.Y = 0;
            this.Sprites[2].CurrentFrame.X = 0;
            this.Sprites[2].Position = this.Position;
            this.LastAction = gameTime.TotalGameTime;
        }

        protected virtual void PlayDeathAnimation(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - LastAction.TotalMilliseconds > 100
                && this.Sprites[2].CurrentFrame.X < 6)
            {
                this.LastAction = gameTime.TotalGameTime;
                this.Sprites[2].CurrentFrame.X++;
                this.Sprites[2].Update();
            }
        }

        private void MoveUp(GameTime gameTime)
        {
            this.Sprites[0].CurrentFrame.Y = 0;

            if (this.CheckForCollision(0, (int)-MovementSpeed))
            {
                this.Position.Y -= this.MovementSpeed;
                this.Sprites[0].Position = this.Position;
                this.Sprites[0].CurrentFrame.X += 60/gameTime.ElapsedGameTime.Milliseconds*0.04f;
                if (this.Sprites[0].CurrentFrame.X > 9 || this.Sprites[0].CurrentFrame.X == 0)
                {
                    this.Sprites[0].CurrentFrame.X = 1;
                }
            }
        }

        private void MoveDown(GameTime gameTime)
        {
            this.Sprites[0].CurrentFrame.Y = 2;

            if (this.CheckForCollision(0,(int)this.MovementSpeed))
            {
                this.Position.Y += this.MovementSpeed;
                this.Sprites[0].Position = this.Position;
                this.Sprites[0].CurrentFrame.X += 60/gameTime.ElapsedGameTime.Milliseconds*0.04f;
                if (this.Sprites[0].CurrentFrame.X > 9 || this.Sprites[0].CurrentFrame.X == 0)
                {
                    this.Sprites[0].CurrentFrame.X = 1;
                }
            }
        }

        private void MoveLeft(GameTime gameTime)
        {
            this.Sprites[0].CurrentFrame.Y = 1;

            if (this.CheckForCollision((int)-this.MovementSpeed, 0))
            {
                this.Position.X -= this.MovementSpeed;
                this.Sprites[0].Position = this.Position;
                this.Sprites[0].CurrentFrame.X += 60/gameTime.ElapsedGameTime.Milliseconds*0.04f;
                if (this.Sprites[0].CurrentFrame.X > 9 || this.Sprites[0].CurrentFrame.X == 0)
                {
                    this.Sprites[0].CurrentFrame.X = 1;
                }
            }
        }

        private void MoveRight(GameTime gameTime)
        {
            this.Sprites[0].CurrentFrame.Y = 3;

            if (this.CheckForCollision((int)this.MovementSpeed, 0))
            {
                this.Position.X += this.MovementSpeed;
                this.Sprites[0].Position = this.Position;
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
                sprite.LoadContent(ScreenManager.Instance.Content);
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
                this.IsAlive = false;
                this.BoundingBox = Rectangle.Empty;
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
                Sprites[2].Draw(spriteBatch);
            }

            if (Input.Instance.KeyDown(Keys.S))
            {
                DrawStats(spriteBatch);
            }       
        }

        protected virtual void DrawStats(SpriteBatch spriteBatch)
        {
            sb = new StringBuilder();
            sb.AppendLine("Damage: " + this.Damage);
            sb.AppendLine("AttackRate: " + this.AttackRate);
            sb.AppendLine("Armor: " + this.Armor);
            sb.AppendLine("Health: " + this.Health);
            sb.AppendLine("Max Health: " + this.MaxHealth);
            sb.AppendLine("Weapon Range: " + this.WeaponRange);
            sb.AppendLine("Movement Speed: " + this.MovementSpeed);
            sb.AppendLine("\nItems:");
            if (this.Items.Any())
            {
                this.Items.Where(i => !(i is StatCrystal)).ToList().ForEach(i => sb.AppendFormat("-- {0}\n", i.GetType().Name));     
            }
            else
            {
                sb.AppendLine("N/A");
            }

            spriteBatch.DrawString(Map.sf, sb, Input.Instance.MouseState.Position.ToVector2(), Color.Black);
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

            if (Map.Wife.BoundingBox.Intersects(someRect))
            {
                canPass = false;
            }

            return canPass;
        }
    }
}