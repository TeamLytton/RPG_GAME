﻿
using Diablo_Wannabe.Entities.StatsBars;
using Diablo_Wannabe.ImageProcessing;
using Microsoft.Xna.Framework;

namespace Diablo_Wannabe.Entities.Enemies
{
    public class OrcMace : Enemy
    {
        private const string DefaultPath = "Entities/orc-mace-";
        private const int DefaultMovementSpeed = 1;
        private const int DefaultHealth = 100;
        private const int DefaultWeaponRange = 45;
        private const int DefaultArmor = 20;
        private const int DefaultDamage = 50;
        private const int DefaultAttackRate = 100;


        public OrcMace(Vector2 position) 
            : base(position, DefaultPath, DefaultMovementSpeed, DefaultHealth, DefaultWeaponRange, DefaultArmor, DefaultDamage, DefaultAttackRate)
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