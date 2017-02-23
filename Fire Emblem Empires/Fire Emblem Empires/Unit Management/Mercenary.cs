using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires.Unit_Management
{
    public class Mercenary : Unit
    {

       public Mercenary(Team team) : base(team)
        {
            m_Job = Job.MERCENARY;
            CalculateLimits();
            InitializeInventory();
        }
    }
}
