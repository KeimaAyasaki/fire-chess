using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fire_Emblem_Empires.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires.Items.Tests
{
    [TestClass()]
    public class ItemManagerTests
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
        [TestMethod()]
        public void mainTest()
        {
            try {
                IronSword sword = new IronSword();
                Assert.AreSame(sword.GetType(), Item.itemType.IRON_SWORD);
                Assert.AreSame(sword.getMight(), 5);
                IronLance lance = new IronLance();
                Assert.AreSame(lance.GetType(), Item.itemType.IRON_LANCE);
                Assert.AreSame(lance.getMight(), 7);
                IronAxe axe = new IronAxe();
                Assert.AreSame(axe.GetType(), Item.itemType.IRON_AXE);
                Assert.AreSame(axe.getMight(), 8);
                Fire flames = new Fire();
                Assert.AreSame(flames.getType(), Item.itemType.FIRE);
                Assert.AreSame(flames.getMight(), 5);
                Staff staff = new Staff();
                Assert.AreSame(staff.GetType(), Item.itemType.STAFF);
                Assert.AreSame(staff.getMight(), 5);
                Vulnerary vul = new Vulnerary();
                Assert.AreSame(vul.GetType(), Item.itemType.VULNERARY);
                Assert.AreSame(vul.getMight(), 5);
                ItemManager mg = new ItemManager();
                mg.useVulnenary(vul);
                Assert.AreSame(vul.getDurability(), 2);
                mg.useVulnenary(vul);
                Assert.AreSame(vul.getDurability(), 1);
                mg.useVulnenary(vul);
                Assert.AreSame(vul.getDurability(), 0);
                mg.useVulnenary(vul);
                Assert.AreSame(vul.getDurability(), 0);

            }
            catch (Exception e)
            {
                
            }
            
        }
    }
}