﻿
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Map;
using Diablo_Wannabe.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Diablo_Wannabe.Entities
{
    public class Player : Unit
    {
        public Vector2 Velocity;
        public SpriteSheet[] sprites;
        public bool isMoving;
        public bool isHitting;
        public bool isCasting;
        public TimeSpan chrono;

        public Player()
        { 
            this.Initialize();      
        }

        public override void Move(GameTime gameTime, List<Tile> tiles)
        {
            if (!Input.Manager.KeyDown(Keys.Left, Keys.Right))
            {
                if (Input.Manager.KeyDown(Keys.Up))
                {
                    this.isMoving = true;
                    MoveUp(gameTime);
                }
                if (Input.Manager.KeyDown(Keys.Down))
                {
                    this.isMoving = true;
                    MoveDown(gameTime);
                }
            }
            if (!Input.Manager.KeyDown(Keys.Up, Keys.Down))
            {
                if (Input.Manager.KeyDown(Keys.Left))
                {
                    this.isMoving = true;
                    MoveLeft(gameTime);
                }
                if (Input.Manager.KeyDown(Keys.Right))
                {
                    this.isMoving = true;
                    MoveRight(gameTime);
                }
            }
            if (Input.Manager.KeyDown(Keys.Space) || isHitting)
            {
                PlayHitAnimation(gameTime);
            }
            if (Input.Manager.KeyPressed(Keys.A) || isCasting)
            {
                PlayCastAnimation(gameTime);
            }
        }

        private void PlayCastAnimation(GameTime gameTime)
        {
            if (!isCasting)
            {
                this.chrono = gameTime.TotalGameTime;
                this.sprites[2].Position = this.Position;
                this.sprites[2].CurrentFrame.Y = this.sprites[0].CurrentFrame.Y;
                this.sprites[2].CurrentFrame.X = 0;
            }
            this.isCasting = true;
            if (gameTime.TotalGameTime.TotalMilliseconds - chrono.TotalMilliseconds < 1000)
            {
                this.sprites[2].CurrentFrame.X += 60/gameTime.ElapsedGameTime.Milliseconds*0.04f;
                if (this.sprites[2].CurrentFrame.X > 7)
                {
                    this.sprites[2].CurrentFrame.X = 0;
                }
            }
            else
            {
                isCasting = false;
            }
        }

        private void PlayHitAnimation(GameTime gameTime)
        {
            if (!isHitting)
            {
                this.chrono = gameTime.TotalGameTime;
                this.sprites[1].Position = this.Position;
                this.sprites[1].CurrentFrame.Y = this.sprites[0].CurrentFrame.Y;
                this.sprites[1].CurrentFrame.X = 0;
            }
            this.isHitting = true;
            if (gameTime.TotalGameTime.TotalMilliseconds - chrono.TotalMilliseconds < 800)
            {
                this.sprites[1].CurrentFrame.X += 60/gameTime.ElapsedGameTime.Milliseconds*0.04f;
                if (this.sprites[1].CurrentFrame.X > 6)
                {
                    this.sprites[1].CurrentFrame.X = 0;
                }
            }
            else
            {
                isHitting = false;
            }
        }

        private void MoveUp(GameTime gameTime)
        {
            this.Position.Y -= this.MovementSpeed;
            this.sprites[0].Position = this.Position;
            this.isHitting = false;
            this.isCasting = false;
            this.sprites[0].CurrentFrame.Y = 0;
            this.sprites[0].CurrentFrame.X += 60/gameTime.ElapsedGameTime.Milliseconds*0.04f;
            if (this.sprites[0].CurrentFrame.X > 9 || this.sprites[0].CurrentFrame.X == 0)
            {
                this.sprites[0].CurrentFrame.X = 1;
            }
        }

        private void MoveDown(GameTime gameTime)
        {
            this.Position.Y += this.MovementSpeed;
            this.sprites[0].Position = this.Position;
            this.isHitting = false;
            this.isCasting = false;
            this.sprites[0].CurrentFrame.Y = 2;
            this.sprites[0].CurrentFrame.X += 60/gameTime.ElapsedGameTime.Milliseconds*0.04f;
            if (this.sprites[0].CurrentFrame.X > 9 || this.sprites[0].CurrentFrame.X == 0)
            {
                this.sprites[0].CurrentFrame.X = 1;
            }
        }

        private void MoveLeft(GameTime gameTime)
        {
            this.Position.X -= this.MovementSpeed;
            this.sprites[0].Position = this.Position;
            this.isHitting = false;
            this.isCasting = false;
            this.sprites[0].CurrentFrame.Y = 1;
            this.sprites[0].CurrentFrame.X += 60/gameTime.ElapsedGameTime.Milliseconds*0.04f;
            if (this.sprites[0].CurrentFrame.X > 9 || this.sprites[0].CurrentFrame.X == 0)
            {
                this.sprites[0].CurrentFrame.X = 1;
            }
        }

        private void MoveRight(GameTime gameTime)
        {
            this.Position.X += this.MovementSpeed;
            this.sprites[0].Position = this.Position;
            this.isHitting = false;
            this.isCasting = false;
            this.sprites[0].CurrentFrame.Y = 3;
            this.sprites[0].CurrentFrame.X += 60/gameTime.ElapsedGameTime.Milliseconds*0.04f;
            if (this.sprites[0].CurrentFrame.X > 9 || this.sprites[0].CurrentFrame.X == 0)
            {
                this.sprites[0].CurrentFrame.X = 1;
            }
        }

        public void Initialize()
        {
            this.Position = new Vector2(ScreenManager.Manager.Dimensions.X / 2, ScreenManager.Manager.Dimensions.Y);
            this.Velocity = Vector2.Zero;
            this.sprites = new SpriteSheet[3];
            this.sprites[0] = new SpriteSheet(9, 4, this.Position, "Entities/player-knight-walk");
            this.sprites[1] = new SpriteSheet(8, 4, this.Position, "Entities/player-knight-hitting");
            this.sprites[2] = new SpriteSheet(7, 4, this.Position, "Entities/player-knight-spellcast");
            this.LoadContent();
            this.MovementSpeed = 3;
            this.chrono = new TimeSpan();
        }

        public override void LoadContent()
        {
            foreach (var sprite in sprites)
            {
                sprite.LoadContent(ScreenManager.Manager.Content);
            }
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime, List<Tile> tiles)
        {
            this.Move(gameTime, tiles);
            if (isHitting)
            {
                sprites[1].Update(gameTime);
            }
            else if (isCasting)
            {
                sprites[2].Update(gameTime);
            }
            else
            {
                sprites[0].Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isHitting)
            {
                sprites[1].Draw(spriteBatch);
            }
            else if (isCasting)
            {
                sprites[2].Draw(spriteBatch);
            }
            else
            {
                sprites[0].Draw(spriteBatch);
            }
        }

        private float CalculateDistance(Vector2 A, Vector2 B)
        {
            A = new Vector2(Math.Abs(A.X), Math.Abs(A.Y));
            B = new Vector2(Math.Abs(B.X), Math.Abs(B.Y));

            float xDiff, yDiff, distance;
            xDiff = A.X - B.X;
            yDiff = A.Y - B.Y;

            distance = (float)Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));

            return Math.Abs(distance);
        }

        private bool CheckForCollision(List<Tile>  tiles)
        {
            return tiles.First(t=>t.TileSprite.Position.X <= this.Position.X + 30 + this.Velocity.X
                       && t.TileSprite.Position.Y <= this.Position.Y + 30 + this.Velocity.Y
                       && t.TileSprite.Position.X + 65 > this.Position.X + 30 + this.Velocity.X
                       && t.TileSprite.Position.Y + 65 > this.Position.Y + 30 + this.Velocity.Y).isPassable;
        }
    }
}