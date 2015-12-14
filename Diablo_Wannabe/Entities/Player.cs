
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public bool IsActive;
        public SpriteSheet PlayerSprite;

        public Player()
        {
            this.IsActive = false;
            this.Initialize();
            this.PlayerSprite = new SpriteSheet(5, 1, this.Position, "Entities/spritesheet");
            PlayerSprite.LoadContent(ScreenManager.Manager.Content);
            this.MovementSpeed = 3;
            this.Velocity = Vector2.Zero;
        }

        public override void Move(GameTime gameTime, List<Tile> tiles )
        {
            Vector2 direction = new Vector2(Input.Manager.MouseState.Position.X, Input.Manager.MouseState.Position.Y) - this.Position;
            float distance = CalculateDistance(new Vector2(Input.Manager.MouseState.Position.X, Input.Manager.MouseState.Position.Y), this.Position);
            if (distance > 1)
            {
                this.PlayerSprite.RotationAngle = (float)((Math.PI * 0.5f) + Math.Atan2(Input.Manager.MouseState.Position.Y - this.Position.Y, Input.Manager.MouseState.Position.X - this.Position.X));
            }
            else
            {
                this.PlayerSprite.CurrentFrame.X = 0;
            }


            if (Input.Manager.MouseState.LeftButton == ButtonState.Pressed && Input.Manager.PreviousMouseState.LeftButton == ButtonState.Pressed)
                {
                    this.IsActive = true;
                }
                else
                {
                    this.IsActive = false;

                }

                if (IsActive)
                {
                this.Velocity = Vector2.Zero;

                if (direction != Vector2.Zero)
                {
                    direction.Normalize();
                }

                

                this.PlayerSprite.CurrentFrame.X += 60 / gameTime.ElapsedGameTime.Milliseconds * 0.04f;
                if (this.PlayerSprite.CurrentFrame.X > 5 || this.PlayerSprite.CurrentFrame.X == 0)
                {
                    this.PlayerSprite.CurrentFrame.X = 1;
                }

                if (distance < this.MovementSpeed)
                {
                    this.Velocity += direction * distance;
                }
                else
                {
                    this.Velocity += direction * this.MovementSpeed;
                }

                Debug.WriteLine("P" + this.Position.X + " " + this.Position.Y);
                Debug.WriteLine("S" + this.PlayerSprite.Position.X + " " + this.PlayerSprite.Position.Y);

                if (CheckForCollision(tiles))
                {
                this.Position += this.Velocity;
                this.PlayerSprite.Position = this.Position;
                }
                else
                {
                    Debug.WriteLine("Maika ti daeba");
                }
                
            }
        }

        public void Initialize()
        {
            this.Position = new Vector2(ScreenManager.Manager.Dimensions.X / 2, ScreenManager.Manager.Dimensions.Y);
            this.Velocity = Vector2.Zero;
        }

        public override void LoadContent()
        {
            this.PlayerSprite.LoadContent(ScreenManager.Manager.Content);
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime, List<Tile> tiles)
        {
            this.Move(gameTime, tiles);
            this.PlayerSprite.Update(gameTime, IsActive);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            PlayerSprite.Draw(spriteBatch);
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