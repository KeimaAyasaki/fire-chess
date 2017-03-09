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
            InitializeRoster();
        }

        private void InitializeRoster()
        {
            for (int j = 0; j < MAX_ROSTER_SIZE; ++j)
            {
                m_roster[j] = new DefaultUnit(Team.DEFAULT_TEAM);
            }
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
                Unit testUnit = new DefaultUnit(unit.GetTeamColor());
                if (m_roster[j].isTheSameUnitAs(testUnit))
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

        public bool RemoveUnitFromRoster(Unit unit)
        {
            bool unitHasBeenRemoved = false;
            for (int j = 0; j < MAX_ROSTER_SIZE; ++j)
            {
                Unit testUnit = new DefaultUnit(unit.GetTeamColor());
                if (m_roster[j].isTheSameUnitAs(unit))
                {
                    m_roster[j] = testUnit;
                    unitHasBeenRemoved = true;
                    Console.WriteLine("A(n) {0} has been removed from the roster.", unit.GetJob());
                    --unitCount;
                    break;
                }
            }
            if (!unitHasBeenRemoved)
            {
                Console.WriteLine("The player did not have a {1} in their roster.", unit.GetJob());
            }
            return unitHasBeenRemoved;
        }

        public bool GetUnitFromRoster(Job unitJob, out Unit unit)
        {
            bool unitHasBeenFound = false;
            unit = new DefaultUnit(Team.DEFAULT_TEAM);
            for (int j = 0; j < MAX_ROSTER_SIZE; ++j)
            {
                if (m_roster[j].GetJob() == unitJob)
                {
                    unit = m_roster[j];
                    unitHasBeenFound = true;
                    break;
                }
            }
            if (unit.isTheSameUnitAs(new DefaultUnit(Team.DEFAULT_TEAM)))
            {
                Console.WriteLine("There was no {0} in the Roster", unitJob);
            }
            return unitHasBeenFound;
        }

        public Unit[] GetRoster()
        {
            return m_roster;
        }
    }
}
