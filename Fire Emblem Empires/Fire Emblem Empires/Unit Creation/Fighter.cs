using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires.Unit_Creation
{
    public class Fighter : Unit
    {
        public Fighter(Team team) : base(team)
        {
            m_Job = Job.FIGHTER;
        }
        
        public Fighter(Team team, byte MaxHealth, byte CurrentHealth, byte Attack, byte Speed, byte Defense, byte Resistance)
            : base(team, MaxHealth, CurrentHealth, Attack, Speed, Defense, Resistance)
        {
            m_Job = Job.FIGHTER;
        }

        protected override void AssignUnitLimits()
        {
            JOB_MIN_HEALTH      = 20;
            JOB_MAX_HEALTH      = 24;
            JOB_MIN_ATTACK      = 06;
            JOB_MAX_ATTACK      = 09;
            JOB_MIN_SPEED       = 02;
            JOB_MAX_SPEED       = 03;
            JOB_MIN_DEFENSE     = 02;
            JOB_MAX_DEFENSE     = 04;
            JOB_MIN_RESISTANCE  = 00;
            JOB_MAX_RESISTANCE  = 00;
        }
    }
}
