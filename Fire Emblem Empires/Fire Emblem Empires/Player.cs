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
        public List<Unit> m_roster { private set; get; }
        public byte m_unitCount { private set; get; }
        public Team m_teamColor { private set; get; }

        public Player(Team team, List<Unit> roster)
        {
            m_roster = roster;
            m_unitCount = (byte)(roster.Count);
            m_teamColor = team;
        }

        public Player(Team team)
        {
            m_unitCount = 0;
            m_roster = new List<Unit>();
            m_teamColor = team;
        }

        public void AddUnitToRoster(Unit unit)
        {
            m_roster.Add(unit);
            Console.WriteLine("A(n) {0} has been added to the roster.", unit.GetType());
            ++m_unitCount;
        }

        public bool RemoveUnitFromRoster(Unit unit)
        {
            bool unitHasBeenRemoved = false;
            for (int j = 0; j < m_unitCount; ++j)
            {
                if (m_roster[j].isTheSameUnitAs(unit))
                {
                    m_roster.RemoveAt(j);
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
            foreach(Unit unit in m_roster)
            {
                if(unit.CanMove())
                {
                    canMoveUnits = true;
                    break;
                }
            }
            return canMoveUnits;
        }

        public void CanNowMoveAllUnits()
        {
            foreach(Unit unit in m_roster)
            {
                unit.isNowAbleToMove();
            }
        }
    }
}
