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
        }

        public Mage(Team team, byte MaxHealth, byte CurrentHealth, byte Attack, byte Speed, byte Defense, byte Resistance)
            : base(team, MaxHealth, CurrentHealth, Attack, Speed, Defense, Resistance)
        {
            m_Job = Job.MAGE;
        }

        protected override void AssignUnitLimits()
        {
            JOB_MIN_HEALTH      = 15;
            JOB_MAX_HEALTH      = 16;
            JOB_MIN_ATTACK      = 04;
            JOB_MAX_ATTACK      = 08;
            JOB_MIN_SPEED       = 02;
            JOB_MAX_SPEED       = 06;
            JOB_MIN_DEFENSE     = 00;
            JOB_MAX_DEFENSE     = 02;
            JOB_MIN_RESISTANCE  = 04;
            JOB_MAX_RESISTANCE  = 06;
        }
    }
}
