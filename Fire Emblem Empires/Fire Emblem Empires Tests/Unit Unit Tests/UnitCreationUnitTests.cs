using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Fire_Emblem_Empires.Unit_Creation;
using Fire_Emblem_Empires.Unit_Management;


namespace Fire_Emblem_Empires_Tests.Unit_Unit_Tests
{
    [TestClass]
    public class UnitCreationUnitTests
    {
        // Some units to test
        Unit mercenaryOne = new Mercenary(Team.BLUE, 20, 16, 9, 8, 7, 6, true);
        Unit mercenaryTwo = new Mercenary(Team.RED);
        Unit mercenaryThree = new Mercenary(Team.GREEN);
        Unit fighterOne = new Fighter(Team.BLUE);
        Unit mageOne = new Mage(Team.RED);
        Unit healerOne = new Healer(Team.BLUE);
        Unit soldierOne = new Soldier(Team.GREEN);
        Unit soldierTwo = new Soldier(Team.BLUE, 20, 20, 5, 6, 5, 5, true);

        [TestMethod]
        public void OverloadedUnitHasCorrectMaxHealth()
        {
            byte expectedResult = 20;
            byte actualResult = mercenaryOne.m_MaxHealth;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OverloadedUnitHasCorrectCurrentHealth()
        {
            byte expectedResult = 16;
            byte actualResult = mercenaryOne.m_CurrentHealth;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OverloadedUnitHasCorrectAttack()
        {
            byte expectedResult = 9;
            byte actualResult = mercenaryOne.m_Attack;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OverloadedUnitHasCorrectSpeed()
        {
            byte expectedResult = 8;
            byte actualResult = mercenaryOne.m_Speed;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OverloadedUnitHasCorrectDefense()
        {
            byte expectedResult = 7;
            byte actualResult = mercenaryOne.m_Defense;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OverloadedUnitHasCorrectResistance()
        {
            byte expectedResult = 6;
            byte actualResult = mercenaryOne.m_Resistance;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OverloadedUnitHasCorrectJob()
        {
            Job expectedResult = Job.MERCENARY;
            Job actualResult = mercenaryOne.GetJob();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OverloadedUnitHasCorrectTeam()
        {
            Team expectedResult = Team.BLUE;
            Team actualResult = mercenaryOne.GetTeamColor();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OverloadedUnitHasCorrectMovementAssignment()
        {
            bool expectedResult = true;
            bool actualResult = mercenaryOne.CanMove();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void OverloadedUnitIsAlive()
        {
            bool expectedResult = true;
            bool actualResult = mercenaryOne.IsAlive();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void UnitCanNoLongerMove()
        {
            // Make sure the fighter can move
            bool expectedResult = true;
            bool actualResult = fighterOne.CanMove();
            Assert.AreEqual(expectedResult, actualResult);

            // Tell the fighter it is unable to move
            fighterOne.isNowUnableToMove();

            // Check if the fighter is unable to move
            expectedResult = false;
            actualResult = fighterOne.CanMove();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void UnitThatWasUnableToMoveCanNowMove()
        {

            // Make sure the mage can move
            bool expectedResult = true;
            bool actualResult = mageOne.CanMove();
            Assert.AreEqual(expectedResult, actualResult);

            // Tell the mage it is unable to move
            mageOne.isNowUnableToMove();

            // Make sure the mage cannot move
            expectedResult = false;
            actualResult = mageOne.CanMove();
            Assert.AreEqual(expectedResult, actualResult);

            // Tell the mage it is able to 
            mageOne.isNowAbleToMove();

            // check if the mage is unable to move
            expectedResult = true;
            actualResult = mageOne.CanMove();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void UnitCanDie()
        {
            //Make sure that the healer is alive
            bool expectedResult = true;
            bool actualResult = healerOne.IsAlive();
            Assert.AreEqual(expectedResult, actualResult);

            // Tell the healer it is dead
            healerOne.isNowDead();

            expectedResult = false;
            actualResult = healerOne.IsAlive();
            Assert.AreEqual(expectedResult, actualResult);
        }

        public void HealerIsAHealer()
        {
            Job expectedResult = Job.HEALER;
            Job actualResult = healerOne.GetJob();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void MageIsAMage()
        {
            Job expectedResult = Job.MAGE;
            Job actualResult = mageOne.GetJob();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void SoldierIsASoldier()
        {
            Job expectedResult = Job.SOLDIER;
            Job actualResult = soldierOne.GetJob();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void FighterIsAFighter()
        {
            Job expectedResult = Job.FIGHTER;
            Job actualResult = fighterOne.GetJob();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void MercenaryIsAMercenary()
        {
            Job expectedResult = Job.MERCENARY;
            Job actualResult = mercenaryTwo.GetJob();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void UnitCanBeAssignedToBlueTeam()
        {
            Team expectedResult = Team.BLUE;
            Team actualResult = fighterOne.GetTeamColor();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void UnitCanBeAssignedToRedTeam()
        {
            Team expectedResult = Team.RED;
            Team actualResult = mercenaryTwo.GetTeamColor();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void UnitCanBeAssignedToGreenTeam()
        {
            Team expectedResult = Team.GREEN;
            Team actualResult = mercenaryThree.GetTeamColor();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void UnitIsHealthyOnCreation()
        {
            byte expectedResult = healerOne.m_MaxHealth;
            byte actualResult   = healerOne.m_CurrentHealth;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void UnitMovementRangeIsCorrectlyCalculated()
        {
            byte expectedResult = 6;
            byte actualResult   = soldierTwo.m_MovementRange;
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
