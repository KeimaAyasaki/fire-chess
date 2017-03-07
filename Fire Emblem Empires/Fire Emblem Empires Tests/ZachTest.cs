using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Fire_Emblem_Empires.Item_Creation;
using Fire_Emblem_Empires.Item_Management;

namespace Fire_Emblem_Empires_Tests
{
    [TestClass]
    public class ZachTest
    {
        /*List of Items:
     * Iron Sword - 5 Might
     * Iron Lance - 7 Might
     * Iron Axe - 8 Might
     * Fire - 5 Might
     * Staff - 5 + (Half of Healer’s Atk)
     * Vulnerary - 5 HP
     *  
     */
        ItemManager im = new ItemManager();
        IronSword sword = new IronSword();
        IronLance lance = new IronLance();
        IronAxe axe = new IronAxe();
        Fire flames = new Fire();
        Staff staff = new Staff();
        Vulnerary vul = new Vulnerary();
        [TestMethod]
        public void swordTypeTest()
        {
            Assert.AreEqual(Item.itemType.IRON_SWORD, sword.getType());
        }
        [TestMethod]
        public void swordMightTest()
        {
            Assert.AreEqual(sword.getMight(), 5);
        }
        [TestMethod]
        public void lanceTypeTest()
        {
            Assert.AreEqual(Item.itemType.IRON_LANCE, lance.getType());
        }
        [TestMethod]
        public void lanceMightTest()
        {
            Assert.AreEqual(lance.getMight(), 7);
        }
        [TestMethod]
        public void axeTypeTest()
        {
            Assert.AreEqual(Item.itemType.IRON_AXE, axe.getType());
        }
        [TestMethod]
        public void axeMightTest()
        {
            Assert.AreEqual(axe.getMight(), 8);
        }
        [TestMethod]
        public void fireTypeTest()
        {
            Assert.AreEqual(Item.itemType.FIRE, flames.getType());
        }
        [TestMethod]
        public void fireMightTest()
        {
            Assert.AreEqual(flames.getMight(), 5);
        }
        [TestMethod]
        public void staffTypeTest()
        {
            Assert.AreEqual(Item.itemType.STAFF, staff.getType());
        }
        [TestMethod]
        public void staffMightTest()
        {
            Assert.AreEqual(staff.getMight(), 5);
        }
        [TestMethod]
        public void vulneraryTypeTest()
        {
            Assert.AreEqual(Item.itemType.VULNERARY, vul.getType());
        }
        [TestMethod]
        public void vulneraryMightTest()
        {
            Assert.AreEqual(vul.getMight(), 5);
        }
        [TestMethod]
        public void firstDurabilityTest()
        {
            im.useVulnenary(vul);
            Assert.AreEqual(2, vul.getDurability());
        }
        [TestMethod]
        public void secondDurabilityTest()
        {
            im.useVulnenary(vul);
            im.useVulnenary(vul);
            Assert.AreEqual(1, vul.getDurability());
        }
        [TestMethod]
        public void thirdDurabilityTest()
        {
            im.useVulnenary(vul);
            im.useVulnenary(vul);
            im.useVulnenary(vul);
            Assert.AreEqual(0, vul.getDurability());
        }
        [TestMethod]
        public void fourthDurabilityTest()
        {
            im.useVulnenary(vul);
            im.useVulnenary(vul);
            im.useVulnenary(vul);
            im.useVulnenary(vul);
            Assert.AreEqual(0, vul.getDurability());
        }
    }
}
