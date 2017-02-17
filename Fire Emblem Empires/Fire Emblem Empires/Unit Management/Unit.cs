using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires
{
    enum Job
    {
        MERCENARY, 
        SOLDIER,
        FIGHTER,
        HEALER,
        MAGE,
    }

    enum Team
    {
        RED,
        BLUE,
        GREEN,
    }

    abstract class Unit
    {
        // Private data members
        private const int MAX_INVENTORY_SIZE = 4;
        private const int BASE_MOVEMENT_SPEED = 4;

        // Protected data members
        protected byte          m_MaxHealth;
        protected byte          m_CurrentHealth;
        protected byte          m_Attack;
        protected byte          m_Speed;
        protected byte          m_Defense;
        protected byte          m_Resistance;
        protected byte          m_Modifiers;
        protected byte          m_MovementRange;
        protected bool          m_alive = true;
        protected bool          m_canMove = true;
        protected Job           m_Job;
        protected Team          m_Team;
        protected Item[4]       m_inventory;


        // Public access methods
        public byte GetMaxHealth() { return m_MaxHealth; }
        public void SetCurrentHealth(byte health) { m_CurrentHealth = health; }
        public byte GetCurrentHealth() { return m_CurrentHealth; }
        public byte GetAttack() { return m_Attack; }
        public byte GetSpeed() { return m_Speed; }
        public byte GetDefense() { return m_Defense; }
        public byte GetResistance() { return m_Resistance; }
        public byte GetMovementRange() { return m_MovementRange; }
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
            for(int j = 0; j < MAX_INVENTORY_SIZE; ++j)
            {
                if(m_inventory[j].compareTo(Item()) == 0)
                {
                    m_inventory[j] = item;
                    itemHasBeenStored = true;
                    Console.WriteLine("A {0} has been added to the {1}'s inventory.", item.GetName(), m_Job.ToString());
                    break;
                }
            }
            return itemHasBeenStored;
        }
        public bool RemoveItemFromInventory(Item item)
        {
            bool itemHasBeenRemoved = false;
            for(int j = 0; j < MAX_INVENTORY_SIZE; ++j)
            {
                if(m_inventory[j].compareTo(item) == 0)
                {
                    m_inventory[j] = Item();
                    itemHasBeenRemoved = true;
                    break;
                }
            }
            return itemHasBeenRemoved;
        }
        public Item[] GetInventory() { return m_inventory; }

        // Protected internal methods
        public void CalculateMovementSpeed()
        {
            m_MovementRange = (byte)(BASE_MOVEMENT_SPEED + (m_Speed % 3));
        }
    }
}
