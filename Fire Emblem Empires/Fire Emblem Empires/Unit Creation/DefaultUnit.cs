using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires.Unit_Creation
{
    public class DefaultUnit : Unit
    {
        public DefaultUnit(Team team) : base(team)
        {
            m_Job = Job.DEFAULT_UNIT;
        }

        public DefaultUnit(Team team, byte MaxHealth, byte CurrentHealth, byte Attack, byte Speed, byte Defense, byte Resistance, bool canMove)
            : base(team, MaxHealth, CurrentHealth, Attack, Speed, Defense, Resistance, canMove)
        {
            m_Job = Job.DEFAULT_UNIT;
        }

        protected override void AssignUnitLimits()
        {
            JOB_MIN_HEALTH = 00;
            JOB_MAX_HEALTH = 00;
            JOB_MIN_ATTACK = 00;
            JOB_MAX_ATTACK = 00;
            JOB_MIN_SPEED = 00;
            JOB_MAX_SPEED = 00;
            JOB_MIN_DEFENSE = 00;
            JOB_MAX_DEFENSE = 00;
            JOB_MIN_RESISTANCE = 00;
            JOB_MAX_RESISTANCE = 00;
        }
    }
}
