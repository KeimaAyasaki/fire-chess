using Fire_Emblem_Empires.Unit_Creation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires
{
    public class Player
    {
        bool isTurn;
        bool canContinue;
        Unit[] m_roster;
        byte MAX_ROSTER_SIZE;
        byte unitCount = 0;

        public Player(byte rosterSize)
        {
            MAX_ROSTER_SIZE = rosterSize;
            m_roster = new Unit[MAX_ROSTER_SIZE];
            InitializeInventory();
        }

        private void InitializeInventory()
        {
            throw new NotImplementedException();
        }

        public byte GetUnitCount()
        {
            return unitCount;
        }

        public bool AddUnitToRoster(Unit unit)
        {
            bool unitHasBeenAdded = false;
            for (int j = 0; j < MAX_ROSTER_SIZE; ++j)
            {
                Unit testUnit = new Default(unit.GetTeamColor());
                if (m_roster[j].compareTo(testUnit))
                {
                    m_roster[j] = unit;
                    unitHasBeenAdded = true;
                    Console.WriteLine("A(n) {0} has been added to the roster.", unit.GetType());
                    ++unitCount;
                    break;
                }
            }
            if (!unitHasBeenAdded)
            {
                Console.WriteLine("The player's roster is full.");
            }
            return unitHasBeenAdded;
        }
    }
}
