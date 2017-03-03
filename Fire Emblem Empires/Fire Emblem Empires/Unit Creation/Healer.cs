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
        }

        public Healer(Team team, byte MaxHealth, byte CurrentHealth, byte Attack, byte Speed, byte Defense, byte Resistance)
            : base(team, MaxHealth, CurrentHealth, Attack, Speed, Defense, Resistance)
        {
            m_Job = Job.HEALER;
        }

        protected override void AssignUnitLimits()
        {
            JOB_MIN_HEALTH      = 16;
            JOB_MAX_HEALTH      = 18;
            JOB_MIN_ATTACK      = 02;
            JOB_MAX_ATTACK      = 03;
            JOB_MIN_SPEED       = 02;
            JOB_MAX_SPEED       = 04;
            JOB_MIN_DEFENSE     = 00;
            JOB_MAX_DEFENSE     = 02;
            JOB_MIN_RESISTANCE  = 05;
            JOB_MAX_RESISTANCE  = 08;
        }
    }
}
