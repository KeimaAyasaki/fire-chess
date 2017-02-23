using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires.Unit_Management
{
    class Soldier : Unit
    {
        public Soldier(Team team) : base(team)
        {
            m_Job = Job.SOLDIER;
            CalculateLimits();
            InitializeInventory();
        }
    }
}
