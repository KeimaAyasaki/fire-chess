using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires.Item_Creation
{
     
    public enum ItemType
    {
        DEFAULT_ITEM,
        IRON_SWORD,
        IRON_LANCE,
        IRON_AXE,
        FIRE,
        STAFF,
        VULNERARY
    };

    public class Item
    {
        protected ItemType type;
        public byte might { private set; get; }
        protected byte durability = 0;

        public ItemType getType()
        {
            return type;
        }
        
         public Item(){
            type = ItemType.DEFAULT_ITEM;
            might = 1;
        }
        public Item(ItemType type)
        {
            this.type = type;
            might = setMight(type);   
        }
        public String getTypeName()
        {
            return type.ToString();
        }
        public byte setMight(ItemType type)
        {
            byte value = 0;
            switch (type)
            {
                case ItemType.DEFAULT_ITEM:
                    value = 1;
                    break;
                case ItemType.IRON_SWORD:
                    value = 5;
                    break;
                case ItemType.IRON_LANCE:
                    value = 7;
                    break;
                case ItemType.IRON_AXE:
                    value = 8;
                    break;
                case ItemType.FIRE:
                    value = 5;
                    break;
                case ItemType.STAFF:
                    value = 5;
                    break;
                case ItemType.VULNERARY:
                    value = 5;
                    break;
            }
            return value;
        }

        public Boolean isTheSameItemAs(Item item)
        {
            return (hasTheSameTypeAs(item) && hasTheSameMightAs(item) && hasTheSameDurabilityAs(item));
        }

        public Boolean hasTheSameDurabilityAs(Item item)
        {
            return getDurability() == getDurability();
        }

        public Boolean hasTheSameMightAs(Item item)
        {
            return might == item.might;
        }

        public Boolean hasTheSameTypeAs(Item item)
        {
            return getType() == item.getType();
        }

        public Boolean hasTheSameTypeAs(ItemType itemType)
        {
            return getType() == itemType;
        }

        // Unreferenced
        //public Boolean isDefaultItem(Item item)
        //{
        //    return item.getType() == ItemType.DEFAULT_ITEM;
        //}

        public byte getDurability()
        {
            return durability;
        }

        public void setDurability(byte change)
        {
            durability = change;
        }
    }
    // Iron Sword, Iron Lance, Iron Axe, Fire, Staff, Vulnerary
    public class IronSword : Item
    {
        public IronSword() : base(ItemType.IRON_SWORD) { }
    }

    public class IronLance : Item
    {
        public IronLance() : base(ItemType.IRON_LANCE) { }
    }

    public class IronAxe : Item
    {
        public IronAxe() : base(ItemType.IRON_AXE) { }
    }

    public class Fire : Item
    {
        public Fire() : base(ItemType.FIRE) { }
    }

    public class Staff : Item
    {
        public Staff() : base(ItemType.STAFF) { }
    }

    public class Vulnerary : Item
    {
        public Vulnerary() : base(ItemType.VULNERARY)
        {
            durability = 3;
        }
    }
}

