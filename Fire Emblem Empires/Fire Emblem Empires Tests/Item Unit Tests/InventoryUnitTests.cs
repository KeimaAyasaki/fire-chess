using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Fire_Emblem_Empires.Item_Creation;
using Fire_Emblem_Empires.Item_Management;

namespace Fire_Emblem_Empires_Tests.Item_Unit_Tests
{
    [TestClass]
    public class InventoryUnitTests
    {
        Inventory m_LargeItemInventoryOne = new Inventory(20);
        Inventory m_SmallItemInventoryOne = new Inventory(1);
        Inventory m_SmallItemInventoryTwo = new Inventory(1);

        Item ironSwordZero = new IronSword();
        Item ironSwordOne  = new IronSword();
        Item ironSwordTwo  = new IronSword();

        [TestInitialize]
        public void Initialize()
        {
            ironSwordOne.setDurability(14);
            ironSwordTwo.setDurability(14);
        }

        [TestMethod]
        public void InventoryCanAddItemsToInventory()
        {
            // Ensure that the item has been added to the inventory
            bool expectedMethodOneResult = true;
            bool actualMethodOneResult   = m_SmallItemInventoryOne.AddItemToInventory(ironSwordOne);
            Assert.AreEqual(expectedMethodOneResult, actualMethodOneResult);

            // Assure that the amount of items in the inventory has been updated
            byte expectedItemCount  = 1;
            byte actualItemCount    = m_SmallItemInventoryOne.itemCount;
            Assert.AreEqual(expectedItemCount, actualItemCount);

            // Ensure that the explicit item has been placed in the inventory, and not just the type of item
            Item expectedItemResult = ironSwordOne;
            Item unexpectedItemResult = ironSwordZero;
            Item actualItemResult;
            bool expectedMethodTwoResult = true;
            bool actualMethodTwoResult = m_SmallItemInventoryOne.GetItemFromInventory(ItemType.IRON_SWORD, out actualItemResult);

            Assert.AreEqual(expectedMethodTwoResult, actualMethodTwoResult);
            Assert.AreEqual(expectedItemResult, actualItemResult);
            Assert.AreNotEqual(unexpectedItemResult, actualItemResult);
        }

        [TestMethod]
        public void InventoryCannotAddMoreItemsThanTheMaximumAllowedAmountOfItemsToAnInventory()
        { 
            // Add an item to the inventory
            bool expectedMethodOneResult= true;
            bool actualMethodOneResult  = m_SmallItemInventoryOne.AddItemToInventory(ironSwordOne);
            Assert.AreEqual(expectedMethodOneResult, actualMethodOneResult);

            // Verify that the item count was added change
            byte expectedItemCountOne = 1;
            byte actualItemCountOne   = m_SmallItemInventoryOne.itemCount;
            Assert.AreEqual(expectedItemCountOne, actualItemCountOne);

            // Attempt to add one more than the max allowed items to the inventory
            bool expectedMethodTwoResult= false;
            bool actualMethodTwoResult  = m_SmallItemInventoryOne.AddItemToInventory(ironSwordZero);
            Assert.AreEqual(expectedMethodTwoResult, actualMethodTwoResult);

            // Verify that the item count was did not change
            byte expectedItemCountTwo = 1;
            byte actualItemCountTwo   = m_SmallItemInventoryOne.itemCount;
            Assert.AreEqual(expectedItemCountTwo, actualItemCountTwo);
        }

        [TestMethod]
        public void InventoryRemovingAnItemWithTheSameTypeDoesNotRemoveAnItemThatIsntExplicitlyTheSameItem()
        {
            // Add an item to the inventory
            bool expectedMethodOneResult = true;
            bool actualMethodOneResult = m_SmallItemInventoryOne.AddItemToInventory(ironSwordOne);
            Assert.AreEqual(expectedMethodOneResult, actualMethodOneResult);

            // Verify that the item count was added change
            byte expectedItemCountOne = 1;
            byte actualItemCountOne = m_SmallItemInventoryOne.itemCount;
            Assert.AreEqual(expectedItemCountOne, actualItemCountOne);

            // Ensure that a particular item has been removed from the inventory and not an item of similar type
            bool expectedMethodTwoResult = true;
            bool actualMethodTwoResult   = m_SmallItemInventoryOne.RemoveItemFromInventory(ironSwordZero);
            Assert.AreEqual(expectedMethodTwoResult, actualMethodTwoResult);
            

        }

        [TestMethod]
        public void InventoryCanRemoveItemsFromInventory()
        {
            // Add an item to the inventory
            bool expectedMethodOneResult = true;
            bool actualMethodOneResult = m_SmallItemInventoryOne.AddItemToInventory(ironSwordOne);
            Assert.AreEqual(expectedMethodOneResult, actualMethodOneResult);
            
            // Verify that the item count was added change
            byte expectedItemCountOne = 1;
            byte actualItemCountOne = m_SmallItemInventoryOne.itemCount;
            Assert.AreEqual(expectedItemCountOne, actualItemCountOne);

            // Ensure that a particular item can be removed from the inventory
            bool expectedMethodTwoResult = true;
            bool actualMethodTwoResult   = m_SmallItemInventoryOne.RemoveItemFromInventory(ironSwordOne);
            Assert.AreEqual(expectedMethodTwoResult, actualMethodTwoResult);

            // Verify that the item count did change
            byte expectedItemCountTwo = 0;
            byte actualItemCountTwo = m_SmallItemInventoryOne.itemCount;
            Assert.AreEqual(expectedItemCountTwo, actualItemCountTwo);
        }

        public void InventoryCanUseADurabilityOfAnItem()
        {
            // Add an item to the inventory and ensure that it is there
            bool expectedMethodOneResult = true;
            bool actualMethodOneResult   = m_SmallItemInventoryOne.AddItemToInventory(ironSwordOne);
            Assert.AreEqual(expectedMethodOneResult, actualMethodOneResult);

            // Assure that the amount of items in the inventory has been updated
            byte expectedItemCount = 1;
            byte actualItemCount = m_SmallItemInventoryOne.itemCount;
            Assert.AreEqual(expectedItemCount, actualItemCount);

            // Use durability on a particular item
            bool expectedMethodTwoResult = true;
            bool actualMethodTwoResult   = m_SmallItemInventoryOne.useDurability(ref ironSwordOne);
            Assert.AreEqual(expectedMethodTwoResult, actualMethodTwoResult);

            // Get the item and verify durability use
            Item item;
            bool expectedMethodThreeResult  = true;
            bool actualMethodThreeResult    = m_SmallItemInventoryOne.GetItemFromInventory(ItemType.IRON_SWORD, out item);
            byte expectedItemDurability = 13;
            byte actualItemDurabiltiy   = item.durability;

            Assert.AreEqual(expectedMethodThreeResult, actualMethodThreeResult);
            Assert.AreEqual(expectedItemDurability, actualItemDurabiltiy);
        }

        public void InventoryCannotUseTheDurabilityOfABrokenItemInsideOfTheInventory()
        {
            // Add an item to the inventory and ensure that it is there
            bool expectedMethodOneResult = true;
            bool actualMethodOneResult = m_SmallItemInventoryOne.AddItemToInventory(ironSwordZero);
            Assert.AreEqual(expectedMethodOneResult, actualMethodOneResult);

            // Assure that the amount of items in the inventory has been updated
            byte expectedItemCount = 1;
            byte actualItemCount = m_SmallItemInventoryOne.itemCount;
            Assert.AreEqual(expectedItemCount, actualItemCount);

            // Use durability on a particular item
            bool expectedMethodTwoResult = false;
            bool actualMethodTwoResult = m_SmallItemInventoryOne.useDurability(ref ironSwordZero);
            Assert.AreEqual(expectedMethodTwoResult, actualMethodTwoResult);

            // Get the item and verify durability use
            Item item;
            bool expectedMethodThreeResult = true;
            bool actualMethodThreeResult = m_SmallItemInventoryOne.GetItemFromInventory(ItemType.IRON_SWORD, out item);
            byte expectedItemDurability = 0;
            byte actualItemDurabiltiy = item.durability;

            Assert.AreEqual(expectedMethodThreeResult, actualMethodThreeResult);
            Assert.AreEqual(expectedItemDurability, actualItemDurabiltiy);
        }

        public void InventoriesWithTheSameItemsWithTheSameParameterrsAreEqual()
        {
            // Add an item to the inventory and ensure that it is there
            bool expectedMethodOneResult = true;
            bool actualMethodOneResult = m_SmallItemInventoryOne.AddItemToInventory(ironSwordOne);
            Assert.AreEqual(expectedMethodOneResult, actualMethodOneResult);

            // Assure that the amount of items in the inventory has been updated
            byte expectedItemCountOne = 1;
            byte actualItemCountOne = m_SmallItemInventoryOne.itemCount;
            Assert.AreEqual(expectedItemCountOne, actualItemCountOne);

            // Add an item to the inventory and ensure that it is there
            bool expectedMethodTwoResult = true;
            bool actualMethodTwoResult = m_SmallItemInventoryTwo.AddItemToInventory(ironSwordTwo);
            Assert.AreEqual(expectedMethodTwoResult, actualMethodTwoResult);

            // Assure that the amount of items in the inventory has been updated
            byte expectedItemCountTwo = 1;
            byte actualItemCountTwo = m_SmallItemInventoryTwo.itemCount;
            Assert.AreEqual(expectedItemCountTwo, actualItemCountTwo);

            // Compare the inventories
            bool expectedMethodThreeResult = true;
            bool actualMethodThreeResult = m_SmallItemInventoryOne.containsTheSameItemsAs(m_SmallItemInventoryTwo);
            Assert.AreEqual(expectedMethodThreeResult, actualMethodThreeResult);
        }
    }
}