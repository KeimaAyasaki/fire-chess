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
            Assert.AreSame(sword.GetType(), Item.itemType.IRON_SWORD);
        }
        [TestMethod]
        public void swordMightTest()
        {
            Assert.AreEqual(sword.getMight(), 5);
        }
        [TestMethod]
        public void lanceTypeTest()
        {
            Assert.AreSame(lance.GetType(), Item.itemType.IRON_LANCE);
        }
        [TestMethod]
        public void lanceMightTest()
        {
            Assert.AreEqual(lance.getMight(), 7);
        }
        [TestMethod]
        public void axeTypeTest()
        {
            Assert.AreSame(axe.GetType(), Item.itemType.IRON_AXE);
        }
        [TestMethod]
        public void axeMightTest()
        {
            Assert.AreEqual(axe.getMight(), 8);
        }
        [TestMethod]
        public void fireTypeTest()
        {
            Assert.AreSame(flames.GetType(), Item.itemType.FIRE);
        }
        [TestMethod]
        public void fireMightTest()
        {
            Assert.AreEqual(flames.getMight(), 5);
        }
        [TestMethod]
        public void staffTypeTest()
        {
            Assert.AreSame(staff.GetType(), Item.itemType.STAFF);
        }
        [TestMethod]
        public void staffMightTest()
        {
            Assert.AreEqual(staff.getMight(), 5);
        }
        [TestMethod]
        public void vulneraryTypeTest()
        {
            Assert.AreSame(vul.GetType(), Item.itemType.VULNERARY);
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
            Assert.AreSame(vul.getDurability(), 2);
        }
        [TestMethod]
        public void secondDurabilityTest()
        {
            im.useVulnenary(vul);
            Assert.AreSame(vul.getDurability(), 1);
        }
        [TestMethod]
        public void thirdDurabilityTest()
        {
            im.useVulnenary(vul);
            Assert.AreSame(vul.getDurability(), 0);
        }
        [TestMethod]
        public void fourthDurabilityTest()
        {
            im.useVulnenary(vul);
            Assert.AreSame(vul.getDurability(), 0);
        }
    }
}
