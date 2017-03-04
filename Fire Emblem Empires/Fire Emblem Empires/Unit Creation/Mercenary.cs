using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires.Unit_Creation
{
    public class Mercenary : Unit
    {
        public Mercenary(Team team) : base(team)
        {
            m_Job = Job.MERCENARY;
        }

        public Mercenary(Team team, byte MaxHealth, byte CurrentHealth, byte Attack, byte Speed, byte Defense, byte Resistance, bool canMove)
            : base(team, MaxHealth, CurrentHealth, Attack, Speed, Defense, Resistance, canMove)
        {
            m_Job = Job.MERCENARY;
        }

        protected override void AssignUnitLimits()
        {
            JOB_MIN_HEALTH      = 16;
            JOB_MAX_HEALTH      = 18;
            JOB_MIN_ATTACK      = 02;
            JOB_MAX_ATTACK      = 04;
            JOB_MIN_SPEED       = 05;
            JOB_MAX_SPEED       = 08;
            JOB_MIN_DEFENSE     = 03;
            JOB_MAX_DEFENSE     = 04;
            JOB_MIN_RESISTANCE  = 00;
            JOB_MAX_RESISTANCE  = 01;
        }
    }
}
