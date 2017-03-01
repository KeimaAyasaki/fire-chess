using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fire_Emblem_Empires.Unit_Creation;

namespace Fire_Emblem_Empires.Board_Creation
{

    public class Tile
    {
        public Tile()
        {
            terrainType = TileEnumeration.PLAIN;
            occupiedBy = null;
        }

        public Tile(TileEnumeration terrain, Unit unit)
        {
            terrainType = terrain;
            occupiedBy = unit;
        }
        public TileEnumeration terrainType { get; set; }
        public Unit occupiedBy { get; set; }
    }
    public class Board
    {
        public Tile[,] spaces;
        public int numRows { get; set; }
        public int numColumns { get; set; }
        public string name { get; set; }

        public Board(int numRows, int numColumns)
        {
            this.numRows = numRows;
            this.numColumns = numColumns;
            spaces = new Tile[numRows, numColumns];
        }

        public void SetSpace(int row, int column, Tile desiredTile)
        {
            spaces[row, column] = desiredTile;
        }

        public void AddUnitToSpace(int row, int column, Unit unit)
        {
            spaces[row, column].occupiedBy = unit;
        }

        public void RemoveUnitFromSpace(int row, int column)
        {
            if(spaces[row, column].occupiedBy != null)
            {
                spaces[row, column].occupiedBy = null;
            }
        }
        //public TileEnumeration ConvertToTileEnumeration(string terrain)
        //{
        //    switch (terrain)
        //    {
        //        case "P":
        //            return TileEnumeration.PLAIN;
        //        case "M":
        //            return TileEnumeration.MOUNTAIN;
        //        case "W":
        //            return TileEnumeration.WATER;
        //        case "F":
        //            return TileEnumeration.FOREST;
        //        case "T":
        //            return TileEnumeration.TOWN;
        //        default:
        //            return TileEnumeration.NULL;
        //    }
        //}
    }
}
