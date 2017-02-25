using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires.Unit_Creation
{
    public class Mage : Unit
    {
        public Mage(Team team) : base(team)
        {
            m_Job = Job.MAGE;
            CalculateLimits();
            InitializeInventory();
        }

        public Mage(Team team, byte MaxHealth, byte CurrentHealth, byte Attack, byte Speed, byte Defense, byte Resistance)
            : base(team, MaxHealth, CurrentHealth, Attack, Speed, Defense, Resistance)
        {
            m_Job = Job.MAGE;
            InitializeInventory();
        }
    }
}
