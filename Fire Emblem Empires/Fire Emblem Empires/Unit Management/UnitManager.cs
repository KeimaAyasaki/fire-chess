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
            unitList.Add(new Soldier());
            Thread.Sleep(100);
            unitList.Add(new Mage());
            Thread.Sleep(100);
            unitList.Add(new Fighter());
            Thread.Sleep(100);
            unitList.Add(new Fighter());
            Thread.Sleep(100);
            unitList.Add(new Healer());
            Thread.Sleep(100);
            unitList.Add(new Mercenary());
            Thread.Sleep(100);
            unitList.Add(new Mercenary());
            Thread.Sleep(100);
            unitList.Add(new Mercenary());

            unitList[0].AddItemToInventory(new IronLance());
            unitList[0].AddItemToInventory(new IronSword());
            unitList[0].RemoveItemFromInventory(new IronSword());

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
