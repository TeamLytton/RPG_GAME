using System.Collections.Generic;
using System.Diagnostics;
using Diablo_Wannabe.Entities.Projectiles;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Interfaces;
using Diablo_Wannabe.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Entities.Enemies
{
    public class SkeletonArcher : Enemy, IShooter
    {
        private const string SkeletonArcherDefaultPath = "Entities/skeleton-bow-";
        private const int DefaultMoveSpeed = 1;
        private const int DefaultHealth = 60;
        private const int DefaultWeaponRange = 170;
        private const int DefaultArmor = 0;
        private const int DefaultDamage = 15;

        private List<IProjectile> arrowsShot;
        private Vector2 playerPos;

        public SkeletonArcher(Vector2 position) 
            : base(position, SkeletonArcherDefaultPath, DefaultMoveSpeed, DefaultHealth, DefaultWeaponRange, DefaultArmor, DefaultDamage)
        {
            this.arrowsShot = new List<IProjectile>();
            this.playerPos = new Vector2();

            this.Sprites[0] = new SpriteSheet(9, 4, this.Position, path + "walking");
            this.Sprites[1] = new SpriteSheet(13, 4, this.Position, path + "shooting");
            this.Sprites[2] = new SpriteSheet(6, 1, this.Position, path + "death");

            this.LoadContent();

            this.BoundingBox = new Rectangle((int)this.Position.X - 16, (int)this.Position.Y - 16,
            (int)this.Sprites[0].FrameDimensions.X / 2, (int)this.Sprites[0].FrameDimensions.Y / 2);
        }

        public void PlayShootingAnimation(GameTime gameTime)
        {
            if (!IsHitting)
            {
                this.LastAction = gameTime.TotalGameTime;
                this.Sprites[1].Position = this.Position;
                this.Sprites[1].CurrentFrame.Y = this.Sprites[0].CurrentFrame.Y;
                this.Sprites[1].CurrentFrame.X = 0;
                playerPos = Map.Player.Position;
            }
            this.IsHitting = true;
            if (gameTime.TotalGameTime.TotalMilliseconds - LastAction.TotalMilliseconds > 80
                && (int)this.Sprites[1].CurrentFrame.X != 12)
            {
                this.Sprites[1].CurrentFrame.X += 1;
                if ((int)this.Sprites[1].CurrentFrame.X == 8)
                {
                    ShootArrow(playerPos);
                }
                LastAction = gameTime.TotalGameTime;
            }
            else if ((int)this.Sprites[1].CurrentFrame.X == 12)
            {
                IsHitting = false;
            }
        }

        private void ShootArrow(Vector2 mousePos)
        {
            Vector2 direction = mousePos - this.Position;
            this.arrowsShot.Add(new SkeletonArrow(this.Position, direction, mousePos));
        }

        public override void Update(GameTime gameTime)
        {
            if (IsAlive)
            {
                this.BoundingBox.X = (int)this.Position.X - 16;
                this.BoundingBox.Y = (int)this.Position.Y - 16;
                Debug.WriteLine("ER - " + this.BoundingBox.X + " " + this.BoundingBox.Y);
                Debug.WriteLine("EP - " + this.Position.X + " " + this.Position.Y);

                Sprites[0].Update();


                if (CalculateDistance(this.Position, Map.Player.Position) < WeaponRange + 100
                    && Map.Player.IsAlive)
                {
                    if (CalculateDistance(this.Position, Map.Player.Position) > WeaponRange)
                    {
                        this.Move(gameTime);
                    }
                    else
                    {
                        PlayShootingAnimation(gameTime);
                    }

                    if (IsHitting)
                    {
                        Sprites[1].Update();
                    }
                }
                else
                {
                    this.MoveTowardsWife(gameTime);
                }
            }
            else
            {
                PlayDeathAnimation(gameTime);
                Sprites[2].Update();
                this.BoundingBox = Rectangle.Empty;
            }
            this.arrowsShot.ForEach(a => a.Fly(this.Damage, gameTime));
            this.arrowsShot.RemoveAll(a => !a.Exists);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            this.arrowsShot.ForEach(a => a.Draw(spriteBatch));
        }
    }
}