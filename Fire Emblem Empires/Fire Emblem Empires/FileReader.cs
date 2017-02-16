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

        public bool Initialize(string filename)
        {
            string filepath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filepath = Directory.GetParent(Directory.GetParent(Directory.GetParent(filepath).FullName).FullName).FullName;
            mapFile = new StreamReader(filepath + filename);

            string mapSizeRegex = "^([0-9]*)[X]([0-9]*)$";
            mapSize = mapFile.ReadLine();

            Match mapDimensions = Regex.Match(mapSize, mapSizeRegex);

            int numRows = 0;
            int numColumns = 0;

            int.TryParse(mapDimensions.Groups[1].ToString(), out numRows);
            int.TryParse(mapDimensions.Groups[2].ToString(), out numColumns);

            regexString = string.Format("^([A-{0}][0-9]+)\\s([M,W,P,F,T])\\s([A,E])?$", (char)('A' + numRows)); 

            string line;
            
            while((line = mapFile.ReadLine()) != null)
            {

            }
            return true;
        }
    }
}
