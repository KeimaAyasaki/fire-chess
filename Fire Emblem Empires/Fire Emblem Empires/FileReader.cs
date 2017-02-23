using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Fire_Emblem_Empires
{
    class FileReader
    {
        private StreamReader mapFile;

        private string mapSize;

        private string regexString;

        private List<int> rows = new List<int>();
        private List<int> columns = new List<int>();
        private List<string> terrains = new List<string>();
        private List<string> units = new List<string>();

        public bool Initialize(string filename, out Board map)
        {
            string filepath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filepath = Directory.GetParent(Directory.GetParent(Directory.GetParent(filepath).FullName).FullName).FullName;
            mapFile = new StreamReader(filepath + filename);

            string mapSizeRegex = "^([0-9]*)[X]([0-9]*)$";
            mapSize = mapFile.ReadLine();

            map = new Board(0, 0);
            Match mapDimensions = Regex.Match(mapSize, mapSizeRegex);

            int numRows = 0;
            int numColumns = 0;

            if (!int.TryParse(mapDimensions.Groups[1].ToString(), out numRows))
            {
                Console.WriteLine("The Row Format did not contain a number.");
                return false;
            }
            if(!int.TryParse(mapDimensions.Groups[2].ToString(), out numColumns))
            {
                Console.WriteLine("The Column Format did not contain a number.");
                return false;
            }

            map = new Board(numRows, numColumns);
            map.name = "Chapter1";

            regexString = string.Format("^([A-{0}][0-9]+)\\s([M,W,P,F,T])\\s?([A,E])?$", (char)('A' + numRows));

            string tileLocation = string.Format("([A-{0}])([0-9]*)", (char)('A' + numRows)); 

            string line;
            
            while((line = mapFile.ReadLine()) != null)
            {
                Match tileInformation = Regex.Match(line, regexString);
                string index = tileInformation.Groups[1].ToString();
                Match locationInfo = Regex.Match(index, tileLocation);
                // sets row with 0 base index
                int row = locationInfo.Groups[1].ToString()[0] - 'A';
                rows.Add(row);
                // sets column with 0 base index
                int column;
                if(int.TryParse(locationInfo.Groups[2].ToString(), out column))
                {
                    column -= 1;
                }
                else
                {
                    Console.WriteLine("Unable to convert {0} from {1} to int.", locationInfo.Groups[0].ToString(), locationInfo.Groups[2].ToString());
                    return false;
                }
                columns.Add(column);
                string terrain = tileInformation.Groups[2].ToString();
                terrains.Add(terrain);
                map.SetSpace(row, column, new Tile(map.ConvertToTileEnumeration(terrain), null));
                // will be empty string if no unit on tile
                string unitInfo = tileInformation.Groups[3].ToString();
                units.Add(unitInfo);
            }
            return true;
        }

        //currently does not work if called before Initialize, this will change when Board is implemented
        public bool CreateFile(Board map)
        {
            string filepath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filepath = Directory.GetParent(Directory.GetParent(Directory.GetParent(filepath).FullName).FullName).FullName;
            StreamWriter newMap = new StreamWriter(filepath + string.Format("\\Data\\MapFiles\\{0}T2.fes", map.name, FileMode.OpenOrCreate));
            newMap.WriteLine("{0}X{1}", map.numRows, map.numColumns);
            for(int i = 0; i <  map.numRows; i++)
            {
                for (int j = 0; j < map.numColumns; j++)
                {
                    int row = i;
                    int column = j;
                    Tile currentTile = map.spaces[row, column];
                    TileEnumeration terrain = currentTile.terrainType;
                    Unit unit = currentTile.occupiedBy;
                    if (i == rows.Count - 1)
                    {
                        newMap.Write("{0}{1} {2}{3}", (char)(row + 'A'), column + 1, terrain.ToString().First(), (unit == null) ? "" : " " + unit);
                    }
                    else
                    {
                        newMap.WriteLine("{0}{1} {2}{3}", (char)(row + 'A'), column + 1, terrain.ToString().First(), (unit == null) ? "" : " " + unit);
                    }
                }
            }
            newMap.Close();
            return true;
        }
        public string ConvertUnitToRegexFormat(Unit unit)
        {
            string line ="";
            line += unit.GetTeamColor().ToString().First();
            line += " " + unit.GetJob().ToString().First();
            line += " " + unit.m_MaxHealth;
            line += " " + unit.m_CurrentHealth;
            line += " " + unit.m_Attack;
            line += " " + unit.m_Speed;
            line += " " + unit.m_Defense;
            line += " " + unit.m_Resistance;
            return line;
        }
    }
}
