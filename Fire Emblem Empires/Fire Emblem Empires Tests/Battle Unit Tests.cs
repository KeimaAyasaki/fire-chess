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
        Unit fb1 = new Fighter(Team.BLUE, 27, 27, 10, 5, 4, 0, true);
        Unit sb1 = new Soldier(Team.BLUE, 24, 24, 7, 4, 8, 1, true);
        Unit mb1 = new Mercenary(Team.BLUE, 21, 15, 6, 8, 4, 1, true);
        Unit hb1 = new Healer(Team.BLUE, 18, 18, 4, 4, 2, 5, true);

        Unit fr1 = new Fighter(Team.RED, 27, 27, 10, 5, 4, 0, true);
        Unit sr1 = new Soldier(Team.RED, 24, 24, 7, 4, 8, 1, true);
        Unit mr1 = new Mercenary(Team.RED, 21, 15, 6, 8, 4, 1, true);
        Unit hr1 = new Healer(Team.RED, 18, 18, 4, 4, 2, 5, true);

        Unit bHighStatFighter   = new Fighter(Team.BLUE, 90, 90, 90, 90, 90, 90, true);
        Unit bLowStatFighter    = new Fighter(Team.BLUE, 5, 5, 5, 5, 5, 5, true);
        Unit bHealerOne         = new Healer(Team.BLUE, 50, 50, 50, 5, 5, 5, true);
        Unit bInjuredSoldierOne = new Soldier(Team.BLUE, 50, 40, 5, 5, 5, 5, true);
        

        [TestMethod]
        public void TestRedUnitDamageCalculation()
        {
            byte expectedByteResult = 2;
            byte actualByteResult = 255;

            bool expectedBoolResult = true;
            bool actualBoolResult = bm.calculateDamage(mb1, mr1, out actualByteResult);
            Assert.AreEqual(expectedByteResult, actualByteResult);
            Assert.AreEqual(expectedBoolResult, actualBoolResult);
        }

        [TestMethod]
        public void TestBlueUnitDamageCalculation()
        {
            byte expectedByteResult = 2;
            byte actualByteResult = 255;

            bool expectedBoolResult = true;
            bool actualBoolResult = bm.calculateDamage(mr1, mb1, out actualByteResult);
            Assert.AreEqual(expectedByteResult, actualByteResult);
            Assert.AreEqual(expectedBoolResult, actualBoolResult); ;
        }

        [TestMethod]
        public void TestBlueUnitHealedAmountCalculation()
        {
            byte expectedByteResult = 6;
            byte actualByteResult = 255;

            bool expectedBoolResult = true;
            bool actualBoolResult = bm.calculateHealing(hb1, mb1, out actualByteResult);
            Assert.AreEqual(expectedByteResult, actualByteResult);
            Assert.AreEqual(expectedBoolResult, actualBoolResult);
        }

        [TestMethod]
        public void TestRedUnitHealedAmountCalculation()
        {
            byte expectedByteResult = 6;
            byte actualByteResult = 255;

            bool expectedBoolResult = true;
            bool actualBoolResult = bm.calculateHealing(hr1, mr1, out actualByteResult);
            Assert.AreEqual(expectedByteResult, actualByteResult);
            Assert.AreEqual(expectedBoolResult, actualBoolResult);
        }

        [TestMethod]
        public void UnitsCannotAttackAllies()
        {
            byte expectedByteResult = 0;
            byte actualByteResult   = 255;

            bool expectedBoolResult = false;
            bool actualBoolResult   = bm.calculateDamage(bHighStatFighter, bLowStatFighter, out actualByteResult);
            Assert.AreEqual(expectedByteResult, actualByteResult);
            Assert.AreEqual(expectedBoolResult, actualBoolResult);
        }

        [TestMethod]
        public void UnitsWhoCannotHealCannotHeal()
        {
            byte expectedByteResult = 0;
            byte actualByteResult = 255;

            bool expectedBoolResult = false;
            bool actualBoolResult = bm.calculateHealing(bHighStatFighter, bLowStatFighter, out actualByteResult);
            Assert.AreEqual(expectedByteResult, actualByteResult);
            Assert.AreEqual(expectedBoolResult, actualBoolResult);
        }

        [TestMethod]
        public void UnitsCannotOverheal()
        {
            byte expectedByteResult = 10;
            byte actualByteResult = 255;

            bool expectedBoolResult = true;
            bool actualBoolResult = bm.calculateHealing(bHealerOne, bInjuredSoldierOne, out actualByteResult);
            Assert.AreEqual(expectedByteResult, actualByteResult);
            Assert.AreEqual(expectedBoolResult, actualBoolResult);
        }
    }
}