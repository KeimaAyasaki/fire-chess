using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires{

    /*List of Items:
     * Iron Sword - 5 Might
     * Iron Lance - 7 Might
     * Iron Axe - 8 Might
     * Fire - 5 Might
     * Staff - 5 + (Half of Healer’s Atk)
     * Vulnerary - 5 HP
     *  
     */

    public class Item{
        private itemType type;
        private int might;

        //Item Types
        public enum itemType{
            DEFAULT_ITEM,
            IRON_SWORD,
            IRON_LANCE,
            IRON_AXE,
            FIRE,
            STAFF,
            VULNERARY
    };
        public itemType getType()
        {
            return type;
        }
        
         public Item(){
            type = itemType.DEFAULT_ITEM;
            might = 1;
        }
        public Item(itemType type)
        {
            this.type = type;
            might = setMight(type);   
        }
        public String getTypeName()
        {
            return type.ToString();
        }
        public int getMight()
        {
            return might;
        }
        public int setMight(itemType type)
        {
            int value = 0;
            switch (type)
            {
                case itemType.DEFAULT_ITEM:
                    value = 1;
                    break;
                case itemType.IRON_SWORD:
                    value = 5;
                    break;
                case itemType.IRON_LANCE:
                    value = 7;
                    break;
                case itemType.IRON_AXE:
                    value = 8;
                    break;
                case itemType.FIRE:
                    value = 5;
                    break;
                case itemType.STAFF:
                    value = 5;
                    break;
                case itemType.VULNERARY:
                    value = 5;
                    break;
            }
            return value;
        }

        public Boolean compareItem(Item item)
        {
            if (this.getType() == item.getType())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    
    // Iron Sword, Iron Lance, Iron Axe, Fire, Staff, Vulnerary


    public class IronSword : Item
    {
        public IronSword()
        {
            this.setMight(itemType.IRON_SWORD);
        }
    }
    public class IronLance : Item
    {
        public IronLance()
        {
            this.setMight(itemType.IRON_LANCE);
        }
    }
}

