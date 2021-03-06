﻿
using System;
using Diablo_Wannabe.ImageProcessing;
using Diablo_Wannabe.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Maps
{
    public class Tile
    {
        public SpriteSheet TileSprite;
        private int tileFrameX;
        private int tileFrameY;
        public bool isPassable;
        public Rectangle BoundingBox;

        public Tile(int frameX, int frameY, Vector2 position, bool isPassable, string path)
        {
            this.TileFrameX = frameX;
            this.TileFrameY = frameY;
            this.isPassable = isPassable;

            if (!isPassable)
            {
                this.BoundingBox = new Rectangle((int)position.X - 16, (int)position.Y - 16, 32, 32);
            }
            else
            {
                this.BoundingBox = Rectangle.Empty;
            }

            TileSprite = new SpriteSheet(8, 8, position, path)
            {
                CurrentFrame =
                {
                    X = TileFrameX,
                    Y = TileFrameY
                }
            };      
            TileSprite.LoadContent(ScreenManager.Instance.Content);
        }

        public int TileFrameX
        {
            get { return this.tileFrameX; }
            set
            {
                if (value > 9 || value < 0)
                {
                    throw new ArgumentOutOfRangeException("Tile frame X cannot be less than 0 and greater than 9");
                }

                this.tileFrameX = value;
            }
        }

        public int TileFrameY
        {
            get { return this.tileFrameY; }
            set
            {
                if (value > 9 || value < 0)
                {
                    throw new ArgumentOutOfRangeException("Tile frame Y cannot be less than 0 and greater than 9");
                }

                this.tileFrameY = value;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            TileSprite.Draw(spriteBatch);
        }
    }
}
