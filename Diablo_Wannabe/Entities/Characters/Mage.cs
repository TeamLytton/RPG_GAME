using Diablo_Wannabe.ImageProcessing;
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
        private const string DefaultPath = "Entities/player-mage-";

        public bool IsCasting { get; set; }

        public Mage() 
            : base(MageDefaultMoveSpeed, MageDefaultWeaponRange, MageDefaultHealth, MageDefaultArmor, MageDefaultDamage)
        {
            this.Sprites[0] = new SpriteSheet(9, 4, this.Position, DefaultPath + "walking");
            this.Sprites[1] = new SpriteSheet(7, 4, this.Position, DefaultPath + "casting");
            this.Sprites[2] = new SpriteSheet(6, 1, this.Position, DefaultPath + "death");
            this.LoadContent();

            this.BoundingBox = new Rectangle((int)this.Position.X - 16, (int)this.Position.Y - 16,
            (int)this.Sprites[0].FrameDimensions.X / 2, (int)this.Sprites[0].FrameDimensions.Y / 2);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Move(GameTime gameTime)
        {
            base.Move(gameTime);
            if (Input.Instance.KeyPressed(Keys.A) 
                || IsCasting)
            {
                PlayCastAnimation(gameTime);
            }
        }

        public void PlayCastAnimation(GameTime gameTime)
        {
        }
    }
}