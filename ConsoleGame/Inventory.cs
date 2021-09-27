using System;
using System.Collections.Generic;
using ConsoleGame.Items;

namespace ConsoleGame
{
    public class Inventory
    {
        private ItemStack[] myContents = new ItemStack[9];
        private Player myInventoryHolder;

        public int GetSize => myContents.Length;

        public Inventory(Player aPlayer, int aMaxSize)
        {
            myInventoryHolder = aPlayer;
            myContents = new ItemStack[aMaxSize];
        }
        
        public Inventory(Player aPlayer)
        {
            myInventoryHolder = aPlayer;
        }

        public void UseItem(int aSlot, IEnemy anEnemy)
        {
            int tempSlot = aSlot - 1;
            if (myContents.Length <= tempSlot || tempSlot < 0 || myContents[tempSlot] == null) return;
            myContents[tempSlot].GetItem.UseItem(myInventoryHolder, anEnemy);
            DecreaseItemsInSlot(tempSlot);
        }

        public bool TryAddItem(IItem anItem, int anAmount = 1)
        {
            if (anAmount == 0) return true;
            
            for (int tempIndex = 0; tempIndex < myContents.Length; tempIndex++)
            {
                if (myContents[tempIndex] == null)
                {
                    myContents[tempIndex] = new ItemStack(anAmount, anItem);
                    return true;
                }
                
                if (myContents[tempIndex].GetItem.GetID == anItem.GetID)
                {
                    myContents[tempIndex].AccessAmount += anAmount;
                    return true;
                }
            }
            return false;
        }
        
        public void AddItemToSlot(int aSlot, IItem anItem)
        {
            if (myContents[aSlot] == null)
            {
                myContents[aSlot] = new ItemStack(anItem);
            } else if (myContents[aSlot].GetItem.GetID == anItem.GetID)
            {
                myContents[aSlot].AccessAmount++;
            }
            
            throw new ArgumentException("Cannot change item using AddItemToSlot, Use SetItem!");
        }

        public void SetItem(int aSlot, ItemStack aStack)
        {
            myContents[aSlot] = aStack;
        }

        public void DecreaseItemsInSlot(int aSlot)
        {
            myContents[aSlot].AccessAmount -= 1;
            
            if (myContents[aSlot].AccessAmount == 0)
            {
                myContents[aSlot] = null;
            }
        }

        public IReadOnlyList<string> GetItemList()
        {
            var tempList = new List<string> {"Slot.".PadRight(6) + "Item".PadRight(Console.WindowWidth/6) + "| Amount".PadRight(6)};
            
            for (int tempIndex = 0; tempIndex < myContents.Length; tempIndex++)
            {
                var tempName = myContents[tempIndex] != null ? myContents[tempIndex].GetItem.GetName : "Empty";
                var tempAmount = myContents[tempIndex] != null ? myContents[tempIndex].AccessAmount.ToString() : "";
                tempList.Add($"{tempIndex+1}.".PadRight(6) + $"{tempName}".PadRight(Console.WindowWidth/6) + $"| {tempAmount}".PadRight(6));
            }

            return tempList;
        }
    }
}