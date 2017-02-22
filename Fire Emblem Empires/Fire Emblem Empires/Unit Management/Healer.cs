using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires.Unit_Management
{
    class Healer : Unit
    {
        public Healer()
        {
            m_Job = Job.HEALER;
            CalculateLimits();
            InitializeInventory();
        }
    }
}
