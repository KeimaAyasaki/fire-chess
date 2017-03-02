using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Fire_Emblem_Empires.Item_Creation;

namespace Fire_Emblem_Empires.Unit_Creation
{
    public enum Job
    {
        MERCENARY,
        SOLDIER,
        FIGHTER,
        HEALER,
        MAGE,
    }

    public enum Team
    {
        RED,
        BLUE,
        GREEN,
    }

    public abstract class Unit
    {
        Random random = new Random();
        // Private Data Members
        private const byte MAX_INVENTORY_SIZE = 4;
        private const byte BASE_MOVEMENT_SPEED = 4;

        // The following data members are for soft caps, not affected by the modifiers

        protected byte JOB_MIN_HEALTH    ;
        protected byte JOB_MAX_HEALTH    ;
        protected byte JOB_MIN_ATTACK    ;
        protected byte JOB_MAX_ATTACK    ;
        protected byte JOB_MIN_SPEED     ;
        protected byte JOB_MAX_SPEED     ;
        protected byte JOB_MIN_DEFENSE   ;
        protected byte JOB_MAX_DEFENSE   ;
        protected byte JOB_MIN_RESISTANCE;
        protected byte JOB_MAX_RESISTANCE;

        private byte MAX_HP;
        private byte MIN_HP;
        private byte MAX_ATTACK;
        private byte MIN_ATTACK;
        private byte MAX_SPEED;
        private byte MIN_SPEED;
        private byte MAX_DEFENSE;
        private byte MIN_DEFENSE;
        private byte MAX_RESISTANCE;
        private byte MIN_RESISTANCE;
        private byte MODIFIER_COUNT;
        
        public Unit(Team team)
        {
            m_Team = team;
            Thread.Sleep(100);
            // temp identification
            ++m_id;
        }

        public Unit(Team team, byte MaxHealth, byte CurrentHealth, byte Attack, byte Speed, byte Defense, byte Resistance)
        {
            m_Team = team;
            m_MaxHealth = MaxHealth;
            m_CurrentHealth = CurrentHealth;
            m_Attack = Attack;
            m_Speed = Speed;
            m_Defense = Defense;
            m_Resistance = Resistance;
            // temp identification
            ++m_id;
        }

        // Protected data members
        public byte m_MaxHealth { get; private set; }
        public byte m_CurrentHealth { get; private set; }
        public byte m_Attack { get; private set; }
        public byte m_Speed { get; private set; }
        public byte m_Defense { get; private set; }
        public byte m_Resistance { get; private set; }
        public byte m_MovementRange { get; private set; }
        protected bool m_alive = true;
        protected bool m_canMove = true;
        protected Job m_Job;
        protected Team m_Team;
        protected Item[] m_inventory = new Item[MAX_INVENTORY_SIZE];

        // Temporary for debugging
        public static byte m_id;

        public override string ToString()
        {
            String output = m_Team.ToString() + " " + m_Job.ToString() + " #" + m_id + "\nMax Health\t\t= " + m_MaxHealth + "\nCurrent Health\t= " + m_CurrentHealth + "\nAttack\t\t\t= " + m_Attack
                + "\nSpeed\t\t\t= " + m_Speed + "\nDefense\t\t\t= " + m_Defense + "\nResistance\t\t= " + m_Resistance;
            if(m_inventory.Count() > 0)
            {
                output += "\nInventory:";
                for(int j = 0; j < m_inventory.Count(); ++j)
                {
                    if (!m_inventory[j].compareItem(new Item()))
                    {
                        output += "\n" + m_inventory[j].getTypeName();
                    }
                }
            }
            return output;
        }

        // Public access methods
        public bool IsAlive() { return m_alive; }
        // If the unit was alive and is now dead, returns true;
        // If the unit was dead, returns false;
        // Ensures that the unit in question is dead.
        public bool isNowDead()
        {
            if (!m_alive)
            {
                Console.Write("The {0} is already dead.", m_Job.ToString());
                return false;
            }
            m_alive = false;
            return true;
        }
        public bool CanMove() { return m_canMove; }
        public void isNowUnableToMove()
        {
            m_canMove = false;
        }
        public void isNowAbleToMove()
        {
            m_canMove = true;
        }
        public Job GetJob() { return m_Job; }
        public void SetTeam(Team team) { m_Team = team; }
        public Team GetTeamColor() { return m_Team; }
        public bool AddItemToInventory(Item item)
        {
            bool itemHasBeenStored = false;
            for (int j = 0; j < MAX_INVENTORY_SIZE; ++j)
            {
                if (m_inventory[j].compareItem(new Item()))
                {
                    m_inventory[j] = item;
                    itemHasBeenStored = true;
                    Console.WriteLine("A {0} has been added to the {1}'s inventory.", item.getTypeName(), m_Job.ToString());
                    break;
                }
            }
            if(!itemHasBeenStored)
            {
                Console.WriteLine("The {0}'s inventory is full.", m_Job.ToString());
            }
            return itemHasBeenStored;
        }
        public bool RemoveItemFromInventory(Item item)
        {
            bool itemHasBeenRemoved = false;
            for (int j = 0; j < MAX_INVENTORY_SIZE; ++j)
            {
                if (m_inventory[j].compareItem(item))
                {
                    m_inventory[j] = new Item();
                    itemHasBeenRemoved = true;
                    Console.WriteLine("A {0} has been removed from the {1}'s inventory.", item.getTypeName(), m_Job.ToString());
                    break;
                }
            }
            if(!itemHasBeenRemoved)
            {
                Console.WriteLine("The {0} did not have a {1} in their inventory.", m_Job.ToString(), item.getTypeName());
            }
            return itemHasBeenRemoved;
        }
        public Item[] GetInventory() { return m_inventory; }

        // Protected internal methods
        protected void CalculateMovementSpeed()
        {
            m_MovementRange = (byte)(BASE_MOVEMENT_SPEED + (m_Speed % 3));
        }

        public bool hasTheJob(Job job)
        {
            return job.CompareTo(GetJob()) == 0;
        }

        // Find a new place to put these things

        byte SOLDIER_MIN_HEALTH = 18;
        byte SOLDIER_MAX_HEALTH = 20;
        byte SOLDIER_MIN_ATTACK = 03;
        byte SOLDIER_MAX_ATTACK = 05;
        byte SOLDIER_MIN_SPEED = 03;
        byte SOLDIER_MAX_SPEED = 04;
        byte SOLDIER_MIN_DEFENSE = 05;
        byte SOLDIER_MAX_DEFENSE = 08;
        byte SOLDIER_MIN_RESISTANCE = 01;
        byte SOLDIER_MAX_RESISTANCE = 02;

        byte FIGHTER_MIN_HEALTH = 20;
        byte FIGHTER_MAX_HEALTH = 24;
        byte FIGHTER_MIN_ATTACK = 06;
        byte FIGHTER_MAX_ATTACK = 09;
        byte FIGHTER_MIN_SPEED = 02;
        byte FIGHTER_MAX_SPEED = 03;
        byte FIGHTER_MIN_DEFENSE = 02;
        byte FIGHTER_MAX_DEFENSE = 04;
        byte FIGHTER_MIN_RESISTANCE = 00;
        byte FIGHTER_MAX_RESISTANCE = 00;

        byte HEALER_MIN_HEALTH = 16;
        byte HEALER_MAX_HEALTH = 18;
        byte HEALER_MIN_ATTACK = 02;
        byte HEALER_MAX_ATTACK = 03;
        byte HEALER_MIN_SPEED = 02;
        byte HEALER_MAX_SPEED = 04;
        byte HEALER_MIN_DEFENSE = 00;
        byte HEALER_MAX_DEFENSE = 02;
        byte HEALER_MIN_RESISTANCE = 05;
        byte HEALER_MAX_RESISTANCE = 08;

        byte MAGE_MIN_HEALTH = 15;
        byte MAGE_MAX_HEALTH = 16;
        byte MAGE_MIN_ATTACK = 04;
        byte MAGE_MAX_ATTACK = 08;
        byte MAGE_MIN_SPEED = 02;
        byte MAGE_MAX_SPEED = 06;
        byte MAGE_MIN_DEFENSE = 00;
        byte MAGE_MAX_DEFENSE = 02;
        byte MAGE_MIN_RESISTANCE = 04;
        byte MAGE_MAX_RESISTANCE = 06;

        private void CalculateModifiers()
        {
            byte modifiers = 0;
            Job job = GetJob();
            modifiers += (byte)(JOB_MAX_HEALTH - JOB_MIN_HEALTH);
            modifiers += (byte)(GetMaxAttack(job)     - GetMinAttack(job));
            modifiers += (byte)(GetMaxSpeed(job)      - GetMinSpeed(job));
            modifiers += (byte)(GetMaxDefense(job)    - GetMinDefense(job));
            modifiers += (byte)(GetMaxResistance(job) - GetMinResistance(job));
            MODIFIER_COUNT = modifiers;
        }

        private void AssignRandomStatsToUnit()
        {
            m_MaxHealth  = (byte)(random.Next(MIN_HP, MAX_HP));
            m_Attack     = (byte)(random.Next(MIN_ATTACK, MAX_ATTACK));
            m_Speed      = (byte)(random.Next(MIN_SPEED, MAX_SPEED));
            m_Defense    = (byte)(random.Next(MIN_DEFENSE, MAX_DEFENSE));
            m_Resistance = (byte)(random.Next(MIN_RESISTANCE, MAX_RESISTANCE));
        }

        private void AssignAbilityPoints()
        {
            Random random = new Random();
            for (int j = 0; j < MODIFIER_COUNT; ++j)
            {
                byte x = (byte) (random.Next(1, 5));
                if(x == 1)
                {
                    ++m_MaxHealth;
                }else if (x == 2)
                {
                    ++m_Attack;
                }else if (x == 3)
                {
                    ++m_Speed;
                }else if (x == 4)
                {
                    ++m_Defense;
                }else if (x == 5)
                {
                    ++m_Resistance;
                }
            }
            m_CurrentHealth = m_MaxHealth;
        }

        protected void CreateRandomStats()
        {
            AssignRandomStatsToUnit();
            AssignAbilityPoints();
        }

        protected void InitializeInventory()
        {
            for(int j = 0; j < MAX_INVENTORY_SIZE; ++j)
            {
                m_inventory[j] = new Item();
            }
        }
    }
}