using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fire_Emblem_Empires.Unit_Creation;

namespace Fire_Emblem_Empires.Board_Creation
{
    public struct Location
    {
        public byte m_row;
        public byte m_column;
        public Location(byte row, byte column)
        {
            m_row = row;
            m_column = column;
        }
    }
    public class Tile
    {
        public Tile()
        {
            m_terrainType = TileEnumeration.PLAIN;
            m_unit = null;
        }

        public Tile(TileEnumeration terrain, Unit unit)
        {
            m_terrainType = terrain;
            m_unit = unit;
        }
        public bool m_isOccupied
        {
            get
            {
                return m_unit != null;
            }
        }
        public void MoveUnitToTile(Tile other)
        {
            other.m_unit = m_unit;
            m_unit = null;
        }

        public TileEnumeration m_terrainType { get; set; }
        public Unit m_unit { get; set; }
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
            spaces[row, column].m_unit = unit;
        }

        public void RemoveUnitFromSpace(int row, int column)
        {
            if(spaces[row, column].m_unit != null)
            {
                spaces[row, column].m_unit = null;
            }
        }

        public void MoveUnitFromSpaceToSpace(Location currLoc, Location destLoc)
        {
        }
    }
}
