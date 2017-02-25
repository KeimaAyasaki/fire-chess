using Fire_Emblem_Empires.Unit_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires
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
        byte MERCENARY_MIN_HEALTH = 16;
        byte MERCENARY_MAX_HEALTH = 18;
        byte MERCENARY_MIN_ATTACK = 02;
        byte MERCENARY_MAX_ATTACK = 04;
        byte MERCENARY_MIN_SPEED = 05;
        byte MERCENARY_MAX_SPEED = 08;
        byte MERCENARY_MIN_DEFENSE = 03;
        byte MERCENARY_MAX_DEFENSE = 04;
        byte MERCENARY_MIN_RESISTANCE = 00;
        byte MERCENARY_MAX_RESISTANCE = 01;

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

        public byte GetMinHealth(Job job)
        {
            byte minHealth = 255;
            if (job.Equals(Job.MERCENARY))
            { minHealth = MERCENARY_MIN_HEALTH; }
            else if (job.Equals(Job.SOLDIER))
            { minHealth = SOLDIER_MIN_HEALTH; }
            else if (job.Equals(Job.FIGHTER))
            { minHealth = FIGHTER_MIN_HEALTH; }
            else if (job.Equals(Job.HEALER))
            { minHealth = HEALER_MAX_HEALTH; }
            else if (job.Equals(Job.MAGE))
            { minHealth = MAGE_MIN_HEALTH; }
            if (minHealth.Equals(255))
            {
                Console.WriteLine("Error - GetMinHealth(Job) returned an invalid value for the job {0}.", job.ToString());
            }
            return minHealth;
        }

        public byte GetMaxHealth(Job job)
        {
            byte maxHealth = 255;
            if (job.Equals(Job.MERCENARY))
            { maxHealth = MERCENARY_MAX_HEALTH; }
            else if (job.Equals(Job.SOLDIER))
            { maxHealth = SOLDIER_MAX_HEALTH; }
            else if (job.Equals(Job.FIGHTER))
            { maxHealth = FIGHTER_MAX_HEALTH; }
            else if (job.Equals(Job.HEALER))
            { maxHealth = HEALER_MAX_HEALTH; }
            else if (job.Equals(Job.MAGE))
            { maxHealth = MAGE_MAX_HEALTH; }
            if (maxHealth.Equals(255))
            {
                Console.WriteLine("Error - GetMaxHealth(Job) returned an invalid value for the job {0}.", job.ToString());
            }
            return maxHealth;
        }

        public byte GetMinAttack(Job job)
        {
            byte minAttack = 255;
            if (job.Equals(Job.MERCENARY))
            { minAttack = MERCENARY_MIN_ATTACK; }
            else if (job.Equals(Job.SOLDIER))
            { minAttack = SOLDIER_MIN_ATTACK; }
            else if (job.Equals(Job.FIGHTER))
            { minAttack = FIGHTER_MIN_ATTACK; }
            else if (job.Equals(Job.HEALER))
            { minAttack = HEALER_MIN_ATTACK; }
            else if (job.Equals(Job.MAGE))
            { minAttack = MAGE_MIN_ATTACK; }
            if (minAttack.Equals(255))
            {
                Console.WriteLine("Error - GetMinAttack(Job) returned an invalid value for the job {0}.", job.ToString());
            }
            return minAttack;
        }

        public byte GetMaxAttack(Job job)
        {
            byte maxAttack = 255;
            if (job.Equals(Job.MERCENARY))
            { maxAttack = MERCENARY_MAX_ATTACK; }
            else if (job.Equals(Job.SOLDIER))
            { maxAttack = SOLDIER_MAX_ATTACK; }
            else if (job.Equals(Job.FIGHTER))
            { maxAttack = FIGHTER_MAX_ATTACK; }
            else if (job.Equals(Job.HEALER))
            { maxAttack = HEALER_MAX_ATTACK; }
            else if (job.Equals(Job.MAGE))
            { maxAttack = MAGE_MAX_ATTACK; }
            if (maxAttack.Equals(255))
            {
                Console.WriteLine("Error - GetMaxAttack(Job) returned an invalid value for the job {0}.", job.ToString());
            }
            return maxAttack;
        }

        public byte GetMinSpeed(Job job)
        {
            byte minSpeed = 255;
            if (job.Equals(Job.MERCENARY))
            { minSpeed = MERCENARY_MIN_SPEED; }
            else if (job.Equals(Job.SOLDIER))
            { minSpeed = SOLDIER_MIN_SPEED; }
            else if (job.Equals(Job.FIGHTER))
            { minSpeed = FIGHTER_MIN_SPEED; }
            else if (job.Equals(Job.HEALER))
            { minSpeed = HEALER_MIN_SPEED; }
            else if (job.Equals(Job.MAGE))
            { minSpeed = MAGE_MIN_SPEED; }
            if (minSpeed.Equals(255))
            {
                Console.WriteLine("Error - GetMinSpeed(Job) returned an invalid value for the job {0}.", job.ToString());
            }
            return minSpeed;
        }

        public byte GetMaxSpeed(Job job)
        {
            byte maxSpeed = 255;
            if (job.Equals(Job.MERCENARY))
            { maxSpeed = MERCENARY_MAX_SPEED; }
            else if (job.Equals(Job.SOLDIER))
            { maxSpeed = SOLDIER_MAX_SPEED; }
            else if (job.Equals(Job.FIGHTER))
            { maxSpeed = FIGHTER_MAX_SPEED; }
            else if (job.Equals(Job.HEALER))
            { maxSpeed = HEALER_MAX_SPEED; }
            else if (job.Equals(Job.MAGE))
            { maxSpeed = MAGE_MAX_SPEED; }
            if (maxSpeed.Equals(255))
            {
                Console.WriteLine("Error - GetMaxSpeed(Job) returned an invalid value for the job {0}.", job.ToString());
            }
            return maxSpeed;
        }

        public byte GetMinDefense(Job job)
        {
            byte minDefense = 255;
            if (job.Equals(Job.MERCENARY))
            { minDefense = MERCENARY_MIN_DEFENSE; }
            else if (job.Equals(Job.SOLDIER))
            { minDefense = SOLDIER_MIN_DEFENSE; }
            else if (job.Equals(Job.FIGHTER))
            { minDefense = FIGHTER_MIN_DEFENSE; }
            else if (job.Equals(Job.HEALER))
            { minDefense = HEALER_MIN_DEFENSE; }
            else if (job.Equals(Job.MAGE))
            { minDefense = MAGE_MIN_DEFENSE; }
            if (minDefense.Equals(255))
            {
                Console.WriteLine("Error - GetMinDefense(Job) returned an invalid value for the job {0}.", job.ToString());
            }
            return minDefense;
        }

        public byte GetMaxDefense(Job job)
        {
            byte maxDefense = 255;
            if (job.Equals(Job.MERCENARY))
            { maxDefense = MERCENARY_MAX_DEFENSE; }
            else if (job.Equals(Job.SOLDIER))
            { maxDefense = SOLDIER_MAX_DEFENSE; }
            else if (job.Equals(Job.FIGHTER))
            { maxDefense = FIGHTER_MAX_DEFENSE; }
            else if (job.Equals(Job.HEALER))
            { maxDefense = HEALER_MAX_DEFENSE; }
            else if (job.Equals(Job.MAGE))
            { maxDefense = MAGE_MAX_DEFENSE; }
            if (maxDefense.Equals(255))
            {
                Console.WriteLine("Error - GetMaxDefense(Job) returned an invalid value for the job {0}.", job.ToString());
            }
            return maxDefense;
        }

        public byte GetMinResistance(Job job)
        {
            byte minResistance = 255;
            if (job.Equals(Job.MERCENARY))
            { minResistance = MERCENARY_MIN_RESISTANCE; }
            else if (job.Equals(Job.SOLDIER))
            { minResistance = SOLDIER_MIN_RESISTANCE; }
            else if (job.Equals(Job.FIGHTER))
            { minResistance = FIGHTER_MIN_RESISTANCE; }
            else if (job.Equals(Job.HEALER))
            { minResistance = HEALER_MIN_RESISTANCE; }
            else if (job.Equals(Job.MAGE))
            { minResistance = MAGE_MIN_RESISTANCE; }
            if (minResistance.Equals(255))
            {
                Console.WriteLine("Error - GetMinResistance(Job) returned an invalid value for the job {0}.", job.ToString());
            }
            return minResistance;
        }

        public byte GetMaxResistance(Job job)
        {
            byte maxResistance = 255;
            if (job.Equals(Job.MERCENARY))
            { maxResistance = MERCENARY_MAX_RESISTANCE; }
            else if (job.Equals(Job.SOLDIER))
            { maxResistance = SOLDIER_MAX_RESISTANCE; }
            else if (job.Equals(Job.FIGHTER))
            { maxResistance = FIGHTER_MAX_RESISTANCE; }
            else if (job.Equals(Job.HEALER))
            { maxResistance = HEALER_MAX_RESISTANCE; }
            else if (job.Equals(Job.MAGE))
            { maxResistance = MAGE_MAX_RESISTANCE; }
            if (maxResistance.Equals(255))
            {
                Console.WriteLine("Error - GetMaxResistance(Job) returned an invalid value for the job {0}.", job.ToString());
            }
            return maxResistance;
        }

        private void SetHPLimits(byte lowerLimit, byte upperLimit)
        {
            if (lowerLimit > upperLimit)
            {
                Console.WriteLine("Warning - SetHPLimits was given a higher lower limit than upper limit. Substituting each for each other");
                byte temp = lowerLimit;
                lowerLimit = upperLimit;
                upperLimit = lowerLimit;
            }
            MIN_HP = lowerLimit;
            MAX_HP = upperLimit;
        }

        private void SetAttackLimits(byte lowerLimit, byte upperLimit)
        {
            if (lowerLimit > upperLimit)
            {
                Console.WriteLine("Warning - SetAttackLimits was given a higher lower limit than upper limit. Substituting each for each other");
                byte temp = lowerLimit;
                lowerLimit = upperLimit;
                upperLimit = lowerLimit;
            }
            MIN_ATTACK = lowerLimit;
            MAX_ATTACK = upperLimit;
        }

        private void SetSpeedLimits(byte lowerLimit, byte upperLimit)
        {
            if (lowerLimit > upperLimit)
            {
                Console.WriteLine("Warning - SetSpeedLimits was given a higher lower limit than upper limit. Substituting each for each other");
                byte temp = lowerLimit;
                lowerLimit = upperLimit;
                upperLimit = lowerLimit;
            }
            MIN_SPEED = lowerLimit;
            MAX_SPEED = upperLimit;
        }

        private void SetDefenseLimits(byte lowerLimit, byte upperLimit)
        {
            if (lowerLimit > upperLimit)
            {
                Console.WriteLine("Warning - SetDefenseLimits was given a higher lower limit than upper limit. Substituting each for each other");
                byte temp = lowerLimit;
                lowerLimit = upperLimit;
                upperLimit = lowerLimit;
            }

            MIN_DEFENSE = lowerLimit;
            MAX_DEFENSE = upperLimit;
        }

        private void SetResistanceLimits(byte lowerLimit, byte upperLimit)
        {
            if (lowerLimit > upperLimit)
            {
                Console.WriteLine("Warning - SetResistanceLimits was given a higher lower limit than upper limit. Substituting each for each other");
                byte temp = lowerLimit;
                lowerLimit = upperLimit;
                upperLimit = lowerLimit;
            }
            MIN_RESISTANCE = lowerLimit;
            MAX_RESISTANCE = upperLimit;
        }

        private void CalculateModifiers()
        {
            byte modifiers = 0;
            Job job = GetJob();
            modifiers += (byte)(GetMaxHealth(job)     - GetMinHealth(job));
            modifiers += (byte)(GetMaxAttack(job)     - GetMinAttack(job));
            modifiers += (byte)(GetMaxSpeed(job)      - GetMinSpeed(job));
            modifiers += (byte)(GetMaxDefense(job)    - GetMinDefense(job));
            modifiers += (byte)(GetMaxResistance(job) - GetMinResistance(job));
            MODIFIER_COUNT = modifiers;
        }

        private void CreateDefaultUnitInformation()
        {
            Job job = GetJob();
            SetHPLimits(GetMinHealth(job), GetMaxHealth(job));
            SetAttackLimits(GetMinAttack(job), GetMaxAttack(job));
            SetSpeedLimits(GetMinSpeed(job), GetMaxSpeed(job));
            SetDefenseLimits(GetMinDefense(job), GetMaxDefense(job));
            SetResistanceLimits(GetMinResistance(job), GetMaxResistance(job));

            CalculateModifiers();
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

        protected void CalculateLimits()
        {
            CreateDefaultUnitInformation();
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