using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires.Unit_Creation
{
    public class Soldier : Unit
    {
        public Soldier(Team team) : base(team)
        {
            m_Job = Job.SOLDIER;
        }

        public Soldier(Team team, byte MaxHealth, byte CurrentHealth, byte Attack, byte Speed, byte Defense, byte Resistance, bool canMove)
            : base(team, MaxHealth, CurrentHealth, Attack, Speed, Defense, Resistance, canMove)
        {
            m_Job = Job.SOLDIER;
        }

        protected override void AssignUnitLimits()
        {
            JOB_MIN_HEALTH      = 18;
            JOB_MAX_HEALTH      = 20;
            JOB_MIN_ATTACK      = 03;
            JOB_MAX_ATTACK      = 05;
            JOB_MIN_SPEED       = 03;
            JOB_MAX_SPEED       = 04;
            JOB_MIN_DEFENSE     = 05;
            JOB_MAX_DEFENSE     = 08;
            JOB_MIN_RESISTANCE  = 01;
            JOB_MAX_RESISTANCE  = 02;
        }
    }
}
