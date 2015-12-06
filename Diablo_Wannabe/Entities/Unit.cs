﻿
using Diablo_Wannabe.Interfaces;
using Microsoft.Xna.Framework;

namespace Diablo_Wannabe.Entities
{
    public abstract class Unit : GameObject, IMovable
    {
        public abstract void Move();
        public abstract void Update(GameTime gameTime);
    }
}