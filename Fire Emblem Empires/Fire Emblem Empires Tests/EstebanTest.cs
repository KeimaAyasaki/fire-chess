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
        BattleManager bm = new BattleManager();
        //Unit(Color, HPMAX, HPCurrent, ATK, SPD, DEF, RES)
        Fighter fb1 = new Fighter(Team.BLUE, 27, 27, 10, 5, 4, 0, true);
        Soldier sb1 = new Soldier(Team.BLUE, 24, 24, 7, 4, 8, 1, true);
        Mercenary mb1 = new Mercenary(Team.BLUE, 21, 15, 6, 8, 4, 1, true);
        Healer hb1 = new Healer(Team.BLUE, 18, 18, 4, 4, 2, 5, true);

        Fighter fr1 = new Fighter(Team.RED, 27, 27, 10, 5, 4, 0, true);
        Soldier sr1 = new Soldier(Team.RED, 24, 24, 7, 4, 8, 1, true);
        Mercenary mr1 = new Mercenary(Team.RED, 21, 15, 6, 8, 4, 1, true);
        Healer hr1 = new Healer(Team.RED, 18, 18, 4, 4, 2, 5, true);

        [TestMethod]
        public void TestRedUnitDamageCalculation()
        {
            var expectedDamageRed = 2;
            byte actualDamageRed = 0;
            bm.calculateDamage(mb1, mr1, out actualDamageRed);
            Assert.AreEqual(expectedDamageRed, actualDamageRed);
        }

        [TestMethod]
        public void TestBlueUnitDamageCalculation()
        {
            var expectedDamageBlue = 2;
            byte actualDamageBlue = 0;
            bm.calculateDamage(mr1, mb1, out actualDamageBlue);
            Assert.AreEqual(expectedDamageBlue, actualDamageBlue);
        }

        [TestMethod]
        public void TestBlueUnitHealedAmountCalculation()
        {
            var expectedHealedAmountBlue = 6;
            byte actualHealedAmountBlue = 6;
            bm.calculateHealing(hb1, mb1, out actualHealedAmountBlue);
            Assert.AreEqual(expectedHealedAmountBlue, actualHealedAmountBlue);
        }

        [TestMethod]
        public void TestRedUnitHealedAmountCalculation()
        {
            var expectedHealedAmountRed = 6;
            byte actualHealedAmountRed = 6;
            bm.calculateHealing(hr1, mr1, out actualHealedAmountRed);
            Assert.AreEqual(expectedHealedAmountRed, actualHealedAmountRed);
        }
    }
}