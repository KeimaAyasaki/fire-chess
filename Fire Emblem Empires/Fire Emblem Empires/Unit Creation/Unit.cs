using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Fire_Emblem_Empires.Item_Management;

namespace Fire_Emblem_Empires.Unit_Creation
{
    public enum Job
    {
        MERCENARY,
        SOLDIER,
        FIGHTER,
        HEALER,
        MAGE,
        DEFAULT_UNIT
    }

    public enum Team
    {
        RED,
        BLUE,
        GREEN,
    }

    public abstract class Unit
    {
        static Random random = new Random();
        // Private Data Members
        private const byte MAX_INVENTORY_SIZE   = 4;
        private const byte BASE_MOVEMENT_SPEED  = 4;

        // The following data members are for soft caps, not affected by the modifiers
        // This gets set only during unit creation, when the caps are determined by the AssignUnitLimits() method
        protected byte JOB_MIN_HEALTH       = 0;
        protected byte JOB_MAX_HEALTH       = 0;
        protected byte JOB_MIN_ATTACK       = 0;
        protected byte JOB_MAX_ATTACK       = 0;
        protected byte JOB_MIN_SPEED        = 0;
        protected byte JOB_MAX_SPEED        = 0;
        protected byte JOB_MIN_DEFENSE      = 0;
        protected byte JOB_MAX_DEFENSE      = 0;
        protected byte JOB_MIN_RESISTANCE   = 0;
        protected byte JOB_MAX_RESISTANCE   = 0;
        // The total difference in stats calculated by taking the sum of differences between the upper and lower limits
        private byte MODIFIER_COUNT;


        // Public data members for stats, unable to be modified but able to be accessed
        public byte m_MaxHealth { get; private set; }
        public byte m_CurrentHealth { get; private set; }
        public byte m_Attack { get; private set; }
        public byte m_Speed { get; private set; }
        public byte m_Defense { get; private set; }
        public byte m_Resistance { get; private set; }
        public byte m_MovementRange { get; private set; }

        // Protected data members to keep track of unit statuses and properties
        protected bool          m_alive     = true;
        protected bool          m_canMove   = true;
        protected Job           m_Job;
        protected Team          m_Team;
        protected Inventory     m_inventory = new Inventory(MAX_INVENTORY_SIZE);

        // Unique Identifier for debugging
        public static byte m_id;
        public void AssignAnID() { ++m_id; }

        public Inventory OpenBag()
        {
            return m_inventory;
        }

        // Public constructor that returns a unit with the given stats on a specified team
        public Unit(Team team)
        {
            // Assigns the unit to the team
            m_Team = team;
            
            // Creates unit stats
            AssignUnitLimits();
            CreateRandomStats();
            CalculateMovementSpeed();
            
            // Debug information
            AssignAnID();
        }

        // Public overloaded constructor for a unit of specified parameters
        // Items have not been implemented as of now
        public Unit(Team team, byte MaxHealth, byte CurrentHealth, byte Attack, byte Speed, byte Defense, byte Resistance, bool canMove)
        {
            // Assign unit stats
            m_Team          = team;

            if (MaxHealth < CurrentHealth)
            {
                Console.WriteLine("Warning! A {0} was constructed with a max health of {1} and a current health of {2}!", MaxHealth, CurrentHealth);
            }

            m_MaxHealth     = MaxHealth;
            m_CurrentHealth = CurrentHealth;
            m_Attack        = Attack;
            m_Speed         = Speed;
            m_Defense       = Defense;
            m_Resistance    = Resistance;
            m_canMove       = canMove;

            // Calculate unit stats based on stats
            CalculateMovementSpeed();

            // Debug information
            AssignAnID();
        }

        // Displays all unit stats and non-default inventory items.
        public override string ToString()
        {
            String output = m_Team.ToString() + " " + m_Job.ToString() + " #" + m_id + "\nMax Health\t\t= " + m_MaxHealth + "\nCurrent Health\t\t= " + m_CurrentHealth + "\nAttack\t\t\t= " + m_Attack
                + "\nSpeed\t\t\t= " + m_Speed + "\nDefense\t\t\t= " + m_Defense + "\nResistance\t\t= " + m_Resistance + "\n" + m_inventory.ToString();
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
        public bool CanMove()
        {
            return m_canMove;
        }
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
        
        // Protected internal methods
        protected void CalculateMovementSpeed()
        {
            m_MovementRange = (byte)(BASE_MOVEMENT_SPEED + (m_Speed / 3));
        }

        public bool hasTheJob(Job job)
        {
            return job.CompareTo(GetJob()) == 0;
        }

       protected abstract void AssignUnitLimits();

        private void CalculateModifiers()
        {
            byte modifiers = 0;
            Job job = GetJob();
            modifiers += (byte)(JOB_MAX_HEALTH - JOB_MIN_HEALTH);
            modifiers += (byte)(JOB_MAX_ATTACK - JOB_MIN_ATTACK);
            modifiers += (byte)(JOB_MAX_SPEED - JOB_MIN_SPEED);
            modifiers += (byte)(JOB_MAX_DEFENSE - JOB_MIN_DEFENSE);
            modifiers += (byte)(JOB_MAX_RESISTANCE - JOB_MIN_RESISTANCE);
            MODIFIER_COUNT = modifiers;
        }

        private void AssignRandomStatsToUnit()
        {
            m_MaxHealth  = (byte)(random.Next(JOB_MIN_HEALTH,       JOB_MAX_HEALTH));
            m_Attack     = (byte)(random.Next(JOB_MIN_ATTACK,       JOB_MAX_ATTACK));
            m_Speed      = (byte)(random.Next(JOB_MIN_SPEED,        JOB_MAX_SPEED));
            m_Defense    = (byte)(random.Next(JOB_MIN_DEFENSE,      JOB_MAX_DEFENSE));
            m_Resistance = (byte)(random.Next(JOB_MIN_RESISTANCE,   JOB_MAX_RESISTANCE));
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
    }
}