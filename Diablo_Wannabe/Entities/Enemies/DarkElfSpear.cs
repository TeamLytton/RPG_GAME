using Diablo_Wannabe.ImageProcessing;
using Microsoft.Xna.Framework;

namespace Diablo_Wannabe.Entities.Enemies
{
    public class DarkElfSpear : Enemy
    {
        private const string DefaultPath = "Entities/darkelf-spear-";
        private const int DefaultMovementSpeed = 3;
        private const int DefaultHealth = 70;
        private const int DefaultWeaponRange = 70;
        private const int DefaultArmor = 10;
        private const int DefaultDamage = 30;

        public DarkElfSpear(Vector2 position) 
            : base(position, DefaultPath, DefaultMovementSpeed, DefaultHealth, DefaultWeaponRange, DefaultArmor, DefaultDamage)
        {
            this.Sprites[0] = new SpriteSheet(9, 4, this.Position, path + "walking");
            this.Sprites[1] = new SpriteSheet(8, 4, this.Position, path + "hitting");
            this.Sprites[2] = new SpriteSheet(6, 1, this.Position, path + "death");

            this.LoadContent();

            this.BoundingBox = new Rectangle((int)this.Position.X - 16, (int)this.Position.Y - 16,
            (int)this.Sprites[0].FrameDimensions.X / 2, (int)this.Sprites[0].FrameDimensions.Y / 2);
        }
    }
}