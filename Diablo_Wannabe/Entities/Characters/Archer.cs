using System.Collections.Generic;
using System.Linq;
using Diablo_Wannabe.Entities.Projectiles;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Diablo_Wannabe.Entities.Characters
{
    public class Archer : Player, IShooter
    {
        private const int ArcherDefaultMoveSpeed = 3;
        private const int ArcherDefaultWeaponRange = 500;
        private const int ArcherDefaultHealth = 80;
        private const int ArcherDefaultArmor = 25;
        private const int ArcherDefaultDamage = 70;
        private const string DefaultPath = "Entities/player-archer-";

        private List<IProjectile> arrowsShooted;
        private Vector2 mousePos;

        public Archer() 
            : base(ArcherDefaultMoveSpeed, ArcherDefaultWeaponRange, ArcherDefaultHealth, ArcherDefaultArmor, ArcherDefaultDamage)
        {
            this.Sprites[0] = new SpriteSheet(9, 4, this.Position, DefaultPath + "walking");
            this.Sprites[1] = new SpriteSheet(13, 4, this.Position, DefaultPath + "shooting");
            this.Sprites[2] = new SpriteSheet(6, 1, this.Position, DefaultPath + "death");
            this.LoadContent();

            this.BoundingBox = new Rectangle((int)this.Position.X - 16, (int)this.Position.Y - 16,
            (int)this.Sprites[0].FrameDimensions.X / 2, (int)this.Sprites[0].FrameDimensions.Y / 2);
            this.arrowsShooted = new List<IProjectile>();
            mousePos = new Vector2();
        }

        public override void Move(GameTime gameTime)
        {
            base.Move(gameTime);
            if (Input.Instance.MouseState.LeftButton == ButtonState.Pressed || IsHitting)
            {
                PlayShootingAnimation(gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.arrowsShooted.ForEach(a => a.Fly(this.Damage, gameTime));
            this.arrowsShooted.RemoveAll(a => !a.Exists);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            this.arrowsShooted.ForEach(a => a.Draw(spriteBatch));
        }
        
        public void PlayShootingAnimation(GameTime gameTime)
        {         
            if (!IsHitting)
            {
                this.LastAction = gameTime.TotalGameTime;
                this.Sprites[1].Position = this.Position;
                this.Sprites[1].CurrentFrame.Y = this.Sprites[0].CurrentFrame.Y;
                this.Sprites[1].CurrentFrame.X = 0;
                mousePos = Input.Instance.MouseState.Position.ToVector2();
            }
            this.IsHitting = true;
            if (gameTime.TotalGameTime.TotalMilliseconds - LastAction.TotalMilliseconds > 35
                && (int)this.Sprites[1].CurrentFrame.X != 12)
            {
                this.Sprites[1].CurrentFrame.X += 1;
                if ((int)this.Sprites[1].CurrentFrame.X == 8)
                {
                    ShootArrow(mousePos);
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
            this.arrowsShooted.Add(new Arrow(this.Position, direction, mousePos));
        }
    }
}