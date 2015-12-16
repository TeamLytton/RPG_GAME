using System.Collections.Generic;
using System.IO;
using Diablo_Wannabe.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Maps
{
    public static class Map
    {
        public static List<Tile> tiles;
        private static int posX;
        private static int posY;
        public static Player Player;
        public static List<Enemy> Enemies;

        public static void Initialize()
        {
            FillMap();
            Player = new Player();
            Player.LoadContent();
            Enemies = new List<Enemy>(); 
            Enemies.Add(new Enemy());
            foreach (var enemy in Enemies)
            {
                enemy.LoadContent();
            }
        }

        public static void FillMap()
        {
            tiles = new List<Tile>();
            posX = 0;
            posY = 0;
            using (var reader = new StreamReader("../../../Content/Map.txt"))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    foreach (char t in line)
                    {
                        switch (t)
                        {
                            case 'G':
                                tiles.Add(new Tile(0, 2, new Vector2(posX, posY), true, "tileset"));
                                tiles[tiles.Count - 1].TileSprite.Position = new Vector2(posX, posY);
                                posX += 64;
                                break;
                            case 'R':
                                tiles.Add(new Tile(0, 0, new Vector2(posX, posY), false, "tileset"));
                                tiles[tiles.Count - 1].TileSprite.Position = new Vector2(posX, posY);

                                posX += 64;
                                break;
                            case 'S':
                                tiles.Add(new Tile(0, 1, new Vector2(posX, posY), true,"tileset"));
                                tiles[tiles.Count - 1].TileSprite.Position = new Vector2(posX, posY);

                                posX += 64;
                                break;
                            case 'n':
                                posY += 64;
                                posX = 0;
                                break;
                        }
                    }
                    line = reader.ReadLine();
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in tiles)
            {
                tile.Draw(spriteBatch);

            }
            foreach (var enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }
            Player.Draw(spriteBatch);
        }
    }
}