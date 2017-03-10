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
        byte MAX_ROSTER_SIZE;

        public Unit[] m_roster { private set; get; }
        public byte m_unitCount { private set; get; }
        public Team m_teamColor { private set; get; }

        public Player(Unit[] roster, byte rosterSize, Team team)
        {
            m_roster = roster;
            m_unitCount = rosterSize;
            m_teamColor = team;
        }

        public Player(byte rosterSize, Team team)
        {
            MAX_ROSTER_SIZE = rosterSize;
            m_unitCount = rosterSize;
            m_roster = new Unit[MAX_ROSTER_SIZE];
            InitializeDefaultRoster();
            m_teamColor = team;
        }

        private void InitializeDefaultRoster()
        {
            for (int j = 0; j < MAX_ROSTER_SIZE; ++j)
            {
                m_roster[j] = new DefaultUnit(Team.DEFAULT_TEAM);
            }
        }
        
        public bool AddUnitToRoster(Unit unit)
        {
            bool unitHasBeenAdded = false;
            for (int j = 0; j < MAX_ROSTER_SIZE; ++j)
            {
                if (m_roster[j].isTheSameUnitAs(new DefaultUnit(m_teamColor)))
                {
                    m_roster[j] = unit;
                    unitHasBeenAdded = true;
                    Console.WriteLine("A(n) {0} has been added to the roster.", unit.GetType());
                    ++m_unitCount;
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
                if (m_roster[j].isTheSameUnitAs(unit))
                {
                    m_roster[j] = new DefaultUnit(m_teamColor);
                    unitHasBeenRemoved = true;
                    Console.WriteLine("A(n) {0} has been removed from the roster.", unit.GetJob());
                    --m_unitCount;
                    break;
                }
            }
            if (!unitHasBeenRemoved)
            {
                Console.WriteLine("The player did not have a {1} in their roster.", unit.GetJob());
            }
            return unitHasBeenRemoved;
        }
        
        public bool CanMoveUnits()
        {
            bool canMoveUnits = false;
            for(int j = 0; j < MAX_ROSTER_SIZE; ++j)
            {
                if(m_roster[j].CanMove() && !m_roster[j].isADefaultUnit())
                {
                    canMoveUnits = true;
                    break;
                }
            }
            return canMoveUnits;
        }

        public void CanNowMoveAllUnits()
        {
            for (int j = 0; j < MAX_ROSTER_SIZE; ++j)
            {
                if (!m_roster[j].CanMove())
                {
                    m_roster[j].isNowAbleToMove();
                }
            }
        }
    }
}
