using System;
using System.Collections.Generic;
using Diablo_Wannabe.Interfaces;
using Diablo_Wannabe.Maps;
using Microsoft.Xna.Framework;

namespace Diablo_Wannabe.Entities
{
    public abstract class Unit : GameObject, IMovable
    {
        public bool IsActive;
        public float MovementSpeed;
        public Vector2 Velocity;

        public abstract void Move(GameTime gameTime );
        public abstract void Update(GameTime gameTime );
    }
}