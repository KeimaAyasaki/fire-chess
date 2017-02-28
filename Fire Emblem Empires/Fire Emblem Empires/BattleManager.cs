using Fire_Emblem_Empires.Unit_Creation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fire_Emblem_Empires.Unit_Creation;

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
                    if ((atkUnit.m_Attack - defUnit.m_Defense) < 0)
                    {
                        damage = 0;
                    }
                    else
                    {
                        damage = (byte)(atkUnit.m_Attack - defUnit.m_Defense);
                    }
                    if (atkUnit.GetJob() == Job.MERCENARY && defUnit.GetJob() == Job.FIGHTER || atkUnit.GetJob() == Job.FIGHTER && defUnit.GetJob() == Job.SOLDIER || atkUnit.GetJob() == Job.SOLDIER && defUnit.GetJob() == Job.MERCENARY)
                    {
                        damage += 2;
                    }
                    else if (atkUnit.GetJob() == Job.MERCENARY && defUnit.GetJob() == Job.SOLDIER || atkUnit.GetJob() == Job.SOLDIER && defUnit.GetJob() == Job.FIGHTER || atkUnit.GetJob() == Job.FIGHTER && defUnit.GetJob() == Job.MERCENARY)
                    {
                        if((damage - 2) < 0)
                        {
                            damage = 0;
                        }
                        else
                        {
                            damage -= 2;
                        }
                    }
                    if (atkUnit.m_Speed >= (defUnit.m_Speed * 2)){ damage *= 2; }
                    if (damage < 0){ damage = 0; }
                    damageWasCalculated = true;
                }
                else
                {
                    if ((atkUnit.m_Attack - defUnit.m_Resistance) < 0)
                    {
                        damage = 0;
                    }
                    else
                    {
                        damage = (byte)(atkUnit.m_Attack - defUnit.m_Resistance);
                    }
                    if (atkUnit.m_Speed >= (defUnit.m_Speed * 2)){ damage *= 2; }
                    if (damage < 0){ damage = 0; }
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
            if(healingUnit.GetJob() == Job.HEALER)
            {
                if (healingUnit.GetTeamColor() == healedUnit.GetTeamColor())
                {
                    amountHealed = (byte)(5 + (healingUnit.m_Attack / 2));
                    if (amountHealed + healedUnit.m_CurrentHealth > healedUnit.m_MaxHealth)
                    {
                        amountHealed = 0;
                    }
                    healingWasCalculated = true;
                }
                else
                {
                    healingWasCalculated = false;
                    amountHealed = 0;
                }
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
