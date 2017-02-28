using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fire_Emblem_Empires;
using Fire_Emblem_Empires.Unit_Creation;
using Fire_Emblem_Empires.Battle_Management;

namespace Fire_Emblem_Empires_Tests
{
    [TestClass]
    public class EstebanTest
    {
        [TestMethod]
        public void BattleTest()
        {
            BattleManager bm = new BattleManager();
            //Unit(Color, HPMAX, HPCurrent, ATK, SPD, DEF, RES)
            Fighter fb1 = new Fighter(Team.BLUE, 27, 27, 10, 5, 4, 0);
            Soldier sb1 = new Soldier(Team.BLUE, 24, 24, 7, 4, 8, 1);
            Mercenary mb1 = new Mercenary(Team.BLUE, 21, 21, 6, 8, 4, 1);
            Healer hb1 = new Healer(Team.BLUE, 18, 18, 4, 4, 2, 5);

            Fighter fr1 = new Fighter(Team.RED, 27, 27, 10, 5, 4, 0);
            Soldier sr1 = new Soldier(Team.RED, 24, 24, 7, 4, 8, 1);
            Mercenary mr1 = new Mercenary(Team.RED, 21, 21, 6, 8, 4, 1);
            Healer hr1 = new Healer(Team.RED, 18, 18, 4, 4, 2, 5);

            var expectedDamageRed = 2;

            byte actualDamageRed = 0;
            bm.calculateDamage(mb1, mr1, out actualDamageRed);

            Assert.AreEqual(expectedDamageRed, actualDamageRed);

            var expectedDamageBlue = 2;
            byte actualDamageBlue = 0;
            bm.calculateDamage(mr1, mb1, out actualDamageBlue);
            Assert.AreEqual(expectedDamageBlue, actualDamageBlue);

            var expectedHealedAmountBlue = 0;
            byte actualHealedAmountBlue = 0;
            bm.calculateHealing(hb1, mb1, out actualHealedAmountBlue);
            Assert.AreEqual(expectedHealedAmountBlue, actualHealedAmountBlue);
                      
        }
    }
}
