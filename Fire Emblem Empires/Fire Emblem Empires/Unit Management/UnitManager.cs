using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires.Unit_Management
{
    class UnitManager
    {
        List<Unit> unitList = new List<Unit>();
        public UnitManager()
        {
            // To add a random unit, must execute Thread.Sleep()

        }

        public override string ToString()
        {
            String output = "";
            foreach(Unit unit in unitList)
            {
                output += unit.ToString() + "\n------------------------------------\n";
            }
            return output;
        }

    }
}
