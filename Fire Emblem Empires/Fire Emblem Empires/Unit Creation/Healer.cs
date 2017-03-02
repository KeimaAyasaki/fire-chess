using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires.Unit_Creation
{
    public class Healer : Unit
    {
        public Healer(Team team) : base(team)
        {
            m_Job = Job.HEALER;
            CreateRandomStats();
            InitializeInventory();
        }

        public Healer(Team team, byte MaxHealth, byte CurrentHealth, byte Attack, byte Speed, byte Defense, byte Resistance)
            : base(team, MaxHealth, CurrentHealth, Attack, Speed, Defense, Resistance)
        {
            m_Job = Job.HEALER;
            InitializeInventory();
        }
    }
}
