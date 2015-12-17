using Diablo_Wannabe.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Diablo_Wannabe.Entities.Characters
{
    public class Mage : Player, ICaster
    {
        private const int MageDefaultMoveSpeed = 2;
        private const int MageDefaultWeaponRange = 60;
        private const int MageDefaultHealth = 70;
        private const int MageDefaultArmor = 20;
        private const int MageDefaultDamage = 25;

        public bool IsCasting { get; set; }

        public Mage(string path) 
            : base(path, MageDefaultMoveSpeed, MageDefaultWeaponRange, MageDefaultHealth, MageDefaultArmor, MageDefaultDamage)
        {
        }

        public override void Update(GameTime gameTime)
        {
            this.Move(gameTime);
            if (IsHitting)
            {
                Sprites[1].Update(gameTime);
            }
            else if (IsCasting)
            {
                Sprites[2].Update(gameTime);
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
            else if (IsCasting)
            {
                Sprites[2].Draw(spriteBatch);
            }
            else
            {
                Sprites[0].Draw(spriteBatch);
            }
        }

        public override void Move(GameTime gameTime)
        {
            base.Move(gameTime);
            if (Input.Manager.KeyPressed(Keys.A) 
                || IsCasting)
            {
                PlayCastAnimation(gameTime);
            }
        }

        public void PlayCastAnimation(GameTime gameTime)
        {
            if (!IsCasting)
            {
                this.LastAction = gameTime.TotalGameTime;
                this.Sprites[2].Position = this.Position;
                this.Sprites[2].CurrentFrame.Y = this.Sprites[0].CurrentFrame.Y;
                this.Sprites[2].CurrentFrame.X = 0;
            }
            this.IsCasting = true;
            if (gameTime.TotalGameTime.TotalMilliseconds - LastAction.TotalMilliseconds < 1000)
            {
                this.Sprites[2].CurrentFrame.X += 60 / gameTime.ElapsedGameTime.Milliseconds * 0.04f;
                if (this.Sprites[2].CurrentFrame.X > 7)
                {
                    this.Sprites[2].CurrentFrame.X = 0;
                }
            }
            else
            {
                IsCasting = false;
            }
        }
    }
}