using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires.Unit_Management
{
    class Fighter : Unit
    {
        public Fighter(Team team) : base(team)
        {
            m_Job = Job.FIGHTER;
            CalculateLimits();
            InitializeInventory();
        }
        
        public Fighter(Team team, byte MaxHealth, byte CurrentHealth, byte Attack, byte Speed, byte Defense, byte Resistance)
            : base(team, MaxHealth, CurrentHealth, Attack, Speed, Defense, Resistance)
        {
            m_Job = Job.FIGHTER;
            InitializeInventory();
        }
    }
}
