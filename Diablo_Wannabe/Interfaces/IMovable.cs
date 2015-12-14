using System.Collections.Generic;
using Diablo_Wannabe.Map;
using Microsoft.Xna.Framework;

namespace Diablo_Wannabe.Interfaces
{
    public interface IMovable
    {
        void Move(GameTime gameTime, List<Tile> tiles);
    }
}