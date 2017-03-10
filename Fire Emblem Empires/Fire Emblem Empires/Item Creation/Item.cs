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

    public abstract class Item
    {
        public ItemType type { protected set; get; }
        public byte might { private set; get; }
        public byte durability { protected set; get; }

        public Item() {
            type = ItemType.DEFAULT_ITEM;
            might = 0;
            durability = 0;
        }
        public Item(ItemType itemType, byte itemDurability)
        {
            type = itemType;
            might = setMight(type);
            durability = itemDurability;
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
            return durability == item.durability;
        }

        public Boolean hasTheSameMightAs(Item item)
        {
            return might == item.might;
        }

        public Boolean hasTheSameTypeAs(Item item)
        {
            return type == item.type;
        }

        public Boolean hasTheSameTypeAs(ItemType itemType)
        {
            return type == itemType;
        }

        public void setDurability(byte change)
        {
            durability = change;
        }

        public bool useDurability()
        {
            if (durability > 0)
            {
                durability -= 1;
                return true;
            }
            return false;
        }
    }

    // The following is a crappy implementation of default durability, figure out a better way to do this

    public class DefaultItem : Item
    {
        private static byte DEFAULT_DURABILITY = 0;
        public DefaultItem() : base(ItemType.DEFAULT_ITEM, DEFAULT_DURABILITY) { }
        public DefaultItem(byte durability) : base(ItemType.DEFAULT_ITEM, durability) { }
    }

    public class IronSword : Item
    {
        private static byte DEFAULT_DURABILITY = 0;
        public IronSword() : base(ItemType.IRON_SWORD, DEFAULT_DURABILITY) { }
        public IronSword(byte durability) : base(ItemType.IRON_SWORD, durability) { }
    }

    public class IronLance : Item
    {
        private static byte DEFAULT_DURABILITY = 0;
        public IronLance() : base(ItemType.IRON_LANCE, DEFAULT_DURABILITY) { }
        public IronLance(byte durability) : base(ItemType.IRON_LANCE, durability) { }
    }

    public class IronAxe : Item
    {
        private static byte DEFAULT_DURABILITY = 0;
        public IronAxe() : base(ItemType.IRON_AXE, DEFAULT_DURABILITY) { }
        public IronAxe(byte durability) : base(ItemType.IRON_AXE, durability) { }
    }

    public class Fire : Item
    {
        private static byte DEFAULT_DURABILITY = 0;
        public Fire() : base(ItemType.FIRE, DEFAULT_DURABILITY) { }
        public Fire(byte durability) : base(ItemType.FIRE, durability) { }
    }

    public class Staff : Item
    {
        private static byte DEFAULT_DURABILITY = 0;
        public Staff() : base(ItemType.STAFF, DEFAULT_DURABILITY) { }
        public Staff(byte durability) : base(ItemType.STAFF, durability) { }
    }

    public class Vulnerary : Item
    {
        private static byte DEFAULT_DURABILITY = 3;
        public Vulnerary() : base(ItemType.VULNERARY, DEFAULT_DURABILITY) { }
        public Vulnerary(byte durability) : base(ItemType.VULNERARY, durability) { }
    }

}

