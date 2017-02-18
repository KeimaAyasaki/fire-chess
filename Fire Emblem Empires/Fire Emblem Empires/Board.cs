using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires
{

    class Tile
    {
        public Tile(TileEnumeration terrain, Unit unit)
        {
            terrainType = terrain;
            occupiedBy = unit;
        }
        public TileEnumeration terrainType;
        public Unit occupiedBy;
    }
    class Board
    {
        private Tile[,] spaces;

        public Board(int numRows, int numColumns)
        {
            spaces = new Tile[numRows, numColumns];
            foreach(Tile space in spaces)
            {
                space.terrainType = TileEnumeration.PLAIN;
            }
        }

        public void SetSpace(int row, int column, Tile desiredTile)
        {
            spaces[row, column] = desiredTile;
        }
    }
}
