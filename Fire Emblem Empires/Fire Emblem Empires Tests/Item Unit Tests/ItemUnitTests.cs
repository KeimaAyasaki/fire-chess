using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fire_Emblem_Empires.Item_Creation;

namespace Fire_Emblem_Empires_Tests.Item_Unit_Tests
{
    [TestClass]
    public class ItemUnitTests
    {
        Item itemOne        = new DefaultItem(0);
        Item ironSwordOne   = new IronSword(3);
        Item ironLanceOne   = new IronLance(3);
        Item ironAxeOne     = new IronAxe(3);
        Item fireOne        = new Fire(3);
        Item staffOne       = new Staff(3);
        Item vulneraryOne   = new Vulnerary();

        [TestMethod]
        public void IronSwordIsAnIronSword()
        {
            ItemType expectedResult = ItemType.IRON_SWORD;
            ItemType actualResult   = ironSwordOne.type;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void IronLanceIsAnIronLance()
        {
            ItemType expectedResult = ItemType.IRON_LANCE;
            ItemType actualResult   = ironLanceOne.type;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void IronAxeIsAnIronAxe()
        {
            ItemType expectedResult = ItemType.IRON_AXE;
            ItemType actualResult   = ironAxeOne.type;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void FireIsAFire()
        {
            ItemType expectedResult = ItemType.FIRE;
            ItemType actualResult   = fireOne.type;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void StaffIsAStaff()
        {
            ItemType expectedResult = ItemType.STAFF;
            ItemType actualResult   = staffOne.type;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void VulneraryIsAVulnerary()
        {
            ItemType expectedResult = ItemType.VULNERARY;
            ItemType actualResult   = vulneraryOne.type;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void DefaultItemIsADefaultItem()
        {
            ItemType expectedResult = ItemType.DEFAULT_ITEM;
            ItemType actualResult   = itemOne.type;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ItemDurabilityCanBeAssigned()
        {
            byte expectedResult = 0;
            byte actualResult   = itemOne.durability;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ItemWithNoDurabilityCannotBeUsed()
        {
            bool expectedResult = false;
            bool actualResult   = itemOne.useDurability();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ItemCanUseDurability()
        {
            bool expectedMethodResult   = true;
            bool actualMethodResult     = ironSwordOne.useDurability();
            Assert.AreEqual(expectedMethodResult, actualMethodResult);
            byte expectedResult = 2;
            byte actualResult   = ironSwordOne.durability;
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
