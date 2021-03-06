﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fire_Emblem_Empires.Item_Creation;

namespace Fire_Emblem_Empires.Item_Management
{
    public class Inventory
    {
        Item[] m_inventory;
        byte MAX_INVENTORY_SIZE;
        public byte itemCount { private set; get; }

        public Inventory(byte inventorySize)
        {
            MAX_INVENTORY_SIZE = inventorySize;
            m_inventory = new Item[MAX_INVENTORY_SIZE];
            InitializeInventory();
        }

        public override string ToString()
        {
            string output = "";
            if (m_inventory.Count() > 0)
            {
                output += "\nInventory:";
                for (int j = 0; j < m_inventory.Count(); ++j)
                {
                    if (!m_inventory[j].hasTheSameTypeAs(new DefaultItem()))
                    {
                        output += "\n" + m_inventory[j].getTypeName();
                    }
                }
            }
            return output;
        }

        // Does not check if the items are out of order, but essentially the same
        public bool containsTheSameItemsAs(Inventory inventory)
        {
            if (inventory.itemCount != itemCount)
            {
                return false;
            }

            for(int j = 0; j < itemCount; ++j)
            {
                if(!m_inventory[j].isTheSameItemAs(inventory.GetInventory()[j]))
                {
                    return false;
                }
            }
            return true;
        }

        public bool AddItemToInventory(Item item)
        {
            bool itemHasBeenStored = false;
            for (int j = 0; j < MAX_INVENTORY_SIZE; ++j)
            {
                if (m_inventory[j].hasTheSameTypeAs(new DefaultItem()))
                {
                    m_inventory[j] = item;
                    itemHasBeenStored = true;
                    Console.WriteLine("A(n) {0} has been added to the inventory.", item.getTypeName());
                    ++itemCount;
                    break;
                }
            }
            if (!itemHasBeenStored)
            {
                Console.WriteLine("The unit's inventory is full.");
            }
            return itemHasBeenStored;
        }

        public bool RemoveItemFromInventory(Item item)
        {
            bool itemHasBeenRemoved = false;
            for (int j = 0; j < MAX_INVENTORY_SIZE; ++j)
            {
                if (m_inventory[j].hasTheSameTypeAs(item))
                {
                    m_inventory[j] = new DefaultItem();
                    itemHasBeenRemoved = true;
                    Console.WriteLine("A(n) {0} has been removed from the inventory.", item.getTypeName());
                    --itemCount;
                    break;
                }
            }
            if (!itemHasBeenRemoved)
            {
                Console.WriteLine("The unit did not have a {1} in their inventory.", item.getTypeName());
            }
            return itemHasBeenRemoved;
        }

        public bool GetItemFromInventory(ItemType itemType, out Item item)
        {
            bool itemHasBeenFound = false;
            item = new DefaultItem();
            for (int j = 0; j < MAX_INVENTORY_SIZE; ++j)
            {
                if(m_inventory[j].hasTheSameTypeAs(itemType))
                {
                    item = m_inventory[j];
                    itemHasBeenFound = true;
                    break;
                }
            }
            return itemHasBeenFound;
        }

        public Item[] GetInventory()
        {
            return m_inventory;
        }

        private void InitializeInventory()
        {
            for (int j = 0; j < MAX_INVENTORY_SIZE; ++j)
            {
                m_inventory[j] = new DefaultItem();
            }
        }

        private Boolean checkDurability(Item item)
        {
            return item.durability > 0;
        }

        public bool useDurability(ref Item item)
        {
            bool durabilityHasBeenUsed = false;
            if (checkDurability(item))
            {
                durabilityHasBeenUsed = item.useDurability();
            }
            return durabilityHasBeenUsed;
        }
    }
}
