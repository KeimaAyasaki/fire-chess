using Fire_Emblem_Empires.Unit_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires
{
    public class BattleManager
    {
        //Calculate Damage
        public bool calculateDamage(Unit atkUnit, Unit defUnit, out byte damage)
        {
            bool damageWasCalculated;
            if(atkUnit.GetTeamColor() != defUnit.GetTeamColor())
            {
                if (atkUnit.GetJob() == Job.MERCENARY || atkUnit.GetJob() == Job.FIGHTER || atkUnit.GetJob() == Job.SOLDIER)
                {
                    damage = (byte)(atkUnit.GetAttack() - defUnit.GetDefense());
                    if (atkUnit.GetJob() == Job.MERCENARY && defUnit.GetJob() == Job.FIGHTER || atkUnit.GetJob() == Job.FIGHTER && defUnit.GetJob() == Job.SOLDIER || atkUnit.GetJob() == Job.SOLDIER && defUnit.GetJob() == Job.MERCENARY)
                    {
                        damage += 2;
                    }
                    else if (atkUnit.GetJob() == Job.MERCENARY && defUnit.GetJob() == Job.SOLDIER || atkUnit.GetJob() == Job.SOLDIER && defUnit.GetJob() == Job.FIGHTER || atkUnit.GetJob() == Job.FIGHTER && defUnit.GetJob() == Job.MERCENARY)
                    {
                        damage -= 2;
                    }
                    if (atkUnit.GetSpeed() >= (defUnit.GetSpeed() * 2))
                    {
                        damage *= 2;
                    }
                    if (damage < 0)
                    {
                        damage = 0;
                    }
                    damageWasCalculated = true;
                }
                else
                {
                    damage = (byte)(atkUnit.GetAttack() - defUnit.GetResistance());
                    if (atkUnit.GetSpeed() >= (defUnit.GetSpeed() * 2))
                    {
                        damage *= 2;
                    }
                    if (damage < 0)
                    {
                        damage = 0;
                    }
                    damageWasCalculated = true;
                }
            }
            else
            {
                damageWasCalculated = false;
                damage = 0;
            }
            return damageWasCalculated;
        }

        //Calculate Healing
        public bool calculateHealing(Unit healingUnit, Unit healedUnit, out byte amountHealed)
        {
            bool healingWasCalculated;
            if(healingUnit.GetTeamColor() == healedUnit.GetTeamColor())
            {
                amountHealed = (byte)(5 + (healingUnit.GetAttack() / 2));
                healingWasCalculated = true;
            }
            else
            {
                healingWasCalculated = false;
                amountHealed = 0;
            }
            return healingWasCalculated;
        }
    }
}
