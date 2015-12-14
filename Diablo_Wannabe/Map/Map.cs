using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo_Wannabe.Map
{
    public class Map
    {
        public List<Tile> tiles;
        private int posX;
        private int posY;

        public void FillMap()
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

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}