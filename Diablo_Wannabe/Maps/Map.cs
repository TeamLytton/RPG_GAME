using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using Diablo_Wannabe.Entities.Enemies;
using Diablo_Wannabe.Entities.Characters;
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
            Player = new Knight("Entities/player-knight-");
            Player.LoadContent();
            Enemies = new List<Enemy>(); 
            Enemies.Add(new OrcMace(new Vector2(500,300)));
            Enemies.Add(new OrcMace(new Vector2(650,200)));
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
                    foreach (var s in line.Split(' '))
                    {
                        switch (s)
                        {
                            case "G":
                                tiles.Add(new Tile(0, 3, new Vector2(posX, posY), true, "tileset"));
                                tiles[tiles.Count - 1].TileSprite.Position = new Vector2(posX, posY);
                                posX += 32;
                                break;
                            case "GS":
                                tiles.Add(new Tile(2, 3, new Vector2(posX, posY), true, "tileset"));
                                tiles[tiles.Count - 1].TileSprite.Position = new Vector2(posX, posY);
                                posX += 32;
                                break;
                            case "W":
                                tiles.Add(new Tile(3, 2, new Vector2(posX, posY), false, "tileset"));
                                tiles[tiles.Count - 1].TileSprite.Position = new Vector2(posX, posY);
                                posX += 32;
                                break;
                            case "WD":
                                tiles.Add(new Tile(0, 2, new Vector2(posX, posY), false, "tileset"));
                                tiles[tiles.Count - 1].TileSprite.Position = new Vector2(posX, posY);
                                posX += 32;
                                break;
                            case "RD":
                                tiles.Add(new Tile(2, 1, new Vector2(posX, posY), false, "tileset"));
                                tiles[tiles.Count - 1].TileSprite.Position = new Vector2(posX, posY);
                                posX += 32;
                                break;
                            case "R":
                                tiles.Add(new Tile(2, 0, new Vector2(posX, posY), false, "tileset"));
                                tiles[tiles.Count - 1].TileSprite.Position = new Vector2(posX, posY);
                                posX += 32;
                                break;
                            case "S":
                                tiles.Add(new Tile(1, 3, new Vector2(posX, posY), true,"tileset"));
                                tiles[tiles.Count - 1].TileSprite.Position = new Vector2(posX, posY);
                                posX += 32;
                                break;
                            case "SS":
                                tiles.Add(new Tile(3, 3, new Vector2(posX, posY), true, "tileset"));
                                tiles[tiles.Count - 1].TileSprite.Position = new Vector2(posX, posY);
                                posX += 32;
                                break;
                            case "TS":
                                tiles.Add(new Tile(6, 1, new Vector2(posX, posY), true, "tileset"));
                                tiles[tiles.Count - 1].TileSprite.Position = new Vector2(posX, posY);
                                posX += 32;
                                break;
                            case "TG":
                                tiles.Add(new Tile(5, 1, new Vector2(posX, posY), false, "tileset"));
                                tiles[tiles.Count - 1].TileSprite.Position = new Vector2(posX, posY);
                                posX += 32;
                                break;
                            case "WL":
                                tiles.Add(new Tile(5, 0, new Vector2(posX, posY), false, "tileset"));
                                tiles[tiles.Count - 1].TileSprite.Position = new Vector2(posX, posY);
                                posX += 32;
                                break;
                            case "WO":
                                tiles.Add(new Tile(4, 1, new Vector2(posX, posY), true, "tileset"));
                                tiles[tiles.Count - 1].TileSprite.Position = new Vector2(posX, posY);
                                posX += 32;
                                break;
                            case "n":
                                posY += 32;
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