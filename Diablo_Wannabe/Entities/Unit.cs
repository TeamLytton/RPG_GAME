
using System;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Interfaces;
using Microsoft.Xna.Framework;

namespace Diablo_Wannabe.Entities
{
    public abstract class Unit : GameObject, IMovable, IUnit
    {
        private int health;
        private int maxHealth;

        public float MovementSpeed;

        public bool IsMoving;
        public bool IsHitting;
        public bool IsAlive;

        public int MaxHealth
        {
            get { return this.maxHealth; }
            set { this.maxHealth = value; }

        }

        public int Health
        {
            get { return health; }
            set
            {
                this.health = value > maxHealth ? maxHealth : value;
            }
        }
        public int Armor { get; set; }
        public int Damage { get; set; }
        public int WeaponRange { get; set; }

        public TimeSpan LastAction;
        public TimeSpan LastTimeDamageTaken;

        public SpriteSheet[] Sprites;
        public Rectangle BoundingBox;

        public virtual void Move(GameTime gameTime)
        {
        }


        public virtual void Update(GameTime gameTime)
        {
        }


        protected virtual void Die()
        {
        }

        protected float CalculateDistance(Vector2 A, Vector2 B)
        {
            A = new Vector2(Math.Abs(A.X), Math.Abs(A.Y));
            B = new Vector2(Math.Abs(B.X), Math.Abs(B.Y));

            float xDiff, yDiff, distance;
            xDiff = A.X - B.X;
            yDiff = A.Y - B.Y;

            distance = (float)Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

            return Math.Abs(distance);
        }
    }
}