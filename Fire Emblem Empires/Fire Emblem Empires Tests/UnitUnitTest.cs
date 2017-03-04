using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fire_Emblem_Empires.Unit_Creation;
using Fire_Emblem_Empires.Unit_Management;
using Fire_Emblem_Empires.Item_Management;


namespace Fire_Emblem_Empires_Tests
{
    [TestClass]
    public class UnitUnitTest
    {
        // Some units to test
        Unit mercenaryOne = new Mercenary(Team.BLUE, 20, 16, 9, 8, 7, 6, true);

        [TestMethod]
        public void OverloadedUnitHasCorrectMaxHealth()
        {
            byte expectedResult = 20;
            byte actualResult   = mercenaryOne.m_MaxHealth;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OverloadedUnitHasCorrectCurrentHealth()
        {
            byte expectedResult = 16;
            byte actualResult   = mercenaryOne.m_CurrentHealth;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OverloadedUnitHasCorrectAttack()
        {
            byte expectedResult = 9;
            byte actualResult   = mercenaryOne.m_Attack;

            Assert.AreEqual(expectedResult, actualResult);
        }
        
        [TestMethod]
        public void OverloadedUnitHasCorrectSpeed()
        {
            byte expectedResult = 8;
            byte actualResult   = mercenaryOne.m_Speed;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OverloadedUnitHasCorrectDefense()
        {
            byte expectedResult = 7;
            byte actualResult   = mercenaryOne.m_Defense;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OverloadedUnitHasCorrectResistance()
        {
            byte expectedResult = 6;
            byte actualResult   = mercenaryOne.m_Resistance;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OverloadedUnitHasCorrectJob()
        {
            Job expectedResult  = Job.MERCENARY;
            Job actualResult    = mercenaryOne.GetJob();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OverloadedUnitHasCorrectTeam()
        {
            Team expectedResult = Team.BLUE;
            Team actualResult   = mercenaryOne.GetTeamColor();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OverloadedUnitHasCorrectMovementAssignment()
        {
            bool expectedResult = true;
            bool actualResult   = mercenaryOne.CanMove();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OverloadedUnitIsAlive()
        {
            bool expectedResult = true;
            bool actualResult   = mercenaryOne.IsAlive();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void UnitCanNoLongerMove()
        {
            bool expectedResult = false;
        }
    }
}
