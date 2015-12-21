using System;
using System.Linq;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Diablo_Wannabe.Entities.Characters
{
    public class Knight : Player
    {
        private const int KnightDefaultMoveSpeed = 3;
        private const int KnightDefaultWeaponRange = 60;
        private const int KnightDefaultHealth = 120;
        private const int KnightDefaultArmor = 60;
        private const int KnightDefaultDamage = 60;
        private const int KnightDefaultAttackRate = 90;
        private const string DefaultPath = "Entities/player-knight-";

        public Knight() 
            : base(KnightDefaultMoveSpeed, KnightDefaultWeaponRange, KnightDefaultHealth, KnightDefaultArmor, KnightDefaultDamage, KnightDefaultAttackRate)
        {
            this.Sprites[0] = new SpriteSheet(9, 4, this.Position, DefaultPath + "walk");
            this.Sprites[1] = new SpriteSheet(8, 4, this.Position, DefaultPath + "hitting");
            this.Sprites[2] = new SpriteSheet(6, 1, this.Position, DefaultPath + "death");
            this.LoadContent();

            this.BoundingBox = new Rectangle((int)this.Position.X - 16, (int)this.Position.Y - 16,
            (int)this.Sprites[0].FrameDimensions.X / 2, (int)this.Sprites[0].FrameDimensions.Y / 2);
        }

        public override void Move(GameTime gameTime)
        {
            base.Move(gameTime);
            if (Input.Instance.MouseState.LeftButton == ButtonState.Pressed ||
                Input.Instance.KeyPressed(Keys.Space) || IsHitting)
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
            if (gameTime.TotalGameTime.TotalMilliseconds - LastAction.TotalMilliseconds > AttackRate
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
                Rectangle rect = this.BoundingBox;
                rect.Height += WeaponRange;
                rect.Y -= WeaponRange;

                Map.Enemies.Where(e => e.BoundingBox.Intersects(rect))
                    .ToList()
                    .ForEach(e => e.TakeDamage(this.Damage, gameTime));
            }
            else if ((int)this.Sprites[1].CurrentFrame.Y == 1)
            {
                Rectangle rect = this.BoundingBox;
                rect.Width += WeaponRange;
                rect.X -= WeaponRange;

                Map.Enemies.Where(e => e.BoundingBox.Intersects(rect))
                    .ToList()
                    .ForEach(e => e.TakeDamage(this.Damage, gameTime));
            }
            else if ((int)this.Sprites[1].CurrentFrame.Y == 2)
            {
                Rectangle rect = this.BoundingBox;
                rect.Height += WeaponRange;

                Map.Enemies.Where(e => e.BoundingBox.Intersects(rect))
                    .ToList()
                    .ForEach(e => e.TakeDamage(this.Damage, gameTime));
            }
            else if ((int)this.Sprites[1].CurrentFrame.Y == 3)
            {
                Rectangle rect = this.BoundingBox;
                rect.Width += this.WeaponRange;

                Map.Enemies.Where(e => e.BoundingBox.Intersects(rect))
                    .ToList()
                    .ForEach(e => e.TakeDamage(this.Damage, gameTime));
            }
        }
    }
}