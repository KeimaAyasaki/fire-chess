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
        public Location m_Location { get; set; }
    }
    public class Board
    {
        public Tile[,] spaces;
        public byte numRows { get; set; }
        public byte numColumns { get; set; }
        public string name { get; set; }

        public Board(byte numRows, byte numColumns)
        {
            this.numRows = numRows;
            this.numColumns = numColumns;
            spaces = new Tile[numRows, numColumns];
            for(byte j = 0; j < numRows; ++j)
            {
                for(byte k = 0; k < numColumns; ++k)
                {
                    spaces[j, k].m_Location = new Location(j, k);
                }
            }
        }

        public void SetSpace(Location loc, Tile desiredTile)
        {
            spaces[loc.m_row, loc.m_column] = desiredTile;
        }

        public void AddUnitToSpace(Location loc, Unit unit)
        {
            spaces[loc.m_row, loc.m_column].m_unit = unit;
        }

        public void RemoveUnitFromSpace(Location loc)
        {
            if(spaces[loc.m_row, loc.m_column].m_unit != null)
            {
                spaces[loc.m_row, loc.m_column].m_unit = null;
            }
        }

        public bool LocationIsAValidLocation(Location loc)
        {
            return loc.m_row <= numRows && loc.m_column <= numColumns;
        }

        public bool MoveUnitFromSpaceToSpace(Location currLoc, Location destLoc)
        {
            bool moveSuccess = false;
            if(LocationIsAValidLocation(currLoc) || LocationIsAValidLocation(destLoc))
            {
                if(spaces[currLoc.m_row, currLoc.m_column].m_isOccupied && spaces[currLoc.m_row, currLoc.m_column].m_unit.m_MovementRange >= CalculateDistance(currLoc, destLoc))
                {
                    spaces[currLoc.m_row, currLoc.m_column].MoveUnitToTile(spaces[destLoc.m_row, destLoc.m_row]);
                    spaces[destLoc.m_row, destLoc.m_column].m_unit.isNowUnableToMove();
                    moveSuccess = true;
                }
            }
            return moveSuccess;
        }

        public byte CalculateDistance(Location locOne, Location locTwo)
        {
            return (byte)(Math.Abs(locOne.m_row - locTwo.m_row) + Math.Abs(locOne.m_column - locTwo.m_column));
        }

        public byte CalculateDistance(Tile tileOne, Tile tileTwo)
        {
            return CalculateDistance(tileOne.m_Location, tileTwo.m_Location);
        }
    }
}
