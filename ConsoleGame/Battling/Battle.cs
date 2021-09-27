using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleGame.Items;
using ConsoleGame.Utils;

namespace ConsoleGame
{
    public class Battle
    {
        private Player myPlayer;
        private IEnemy myEnemy;
        private bool myBattlingFlag;
        private Random myRandom = new Random();

        public Battle(Player aPlayer, IEnemy aEnemy)
        {
            myPlayer = aPlayer;
            myEnemy = aEnemy;
        }

        public void StartBattle()
        {
            myBattlingFlag = true;
            
            while (myBattlingFlag)
            {
                Console.Clear();
                TextUtils.SendMessage($"You've encountered an {myEnemy.GetName}!", TextType.HEADERBAR);
                TextUtils.SendMessage(new []
                {
                    "What should we do about them?",
                    "1: Attack                    ",
                    "2: Defend                    ",
                    "3: Insult                    ",
                    "4: Use Items                 ",
                    $"Player: {myPlayer.GetHealth}  -  {myEnemy.GetName}: {myEnemy.GetHealth}"
                }, TextType.CENTERED);

                DoPlayerBattleAction();
                
                if(myEnemy.GetHealth > 0)
                    myEnemy.DoBattleAction(myPlayer);

                if (myEnemy.GetHealth <= 0 || myPlayer.GetHealth <= 0)
                    myBattlingFlag = false;
            }
            
            Console.Clear();
            var myAmount = myRandom.Next(3);
            
            TextUtils.SendMessage(new []
            {
                "You won the battle!",
                $"You got {myAmount} Healing Potion(s)",
            }, TextType.CENTERED);
            var tempSuccess = myPlayer.GetInventory.TryAddItem(new HealthPotion(), myAmount);
            
            if(!tempSuccess)
                TextUtils.SendMessage("Your inventory is full!", TextType.INVENTORY);
            
            Thread.Sleep(1000);

            myPlayer.AccessLastAction = null;
            myEnemy.AccessLastAction = null;
        }

        private void DoPlayerBattleAction()
        {
            var tempKey = Console.ReadKey(true).Key;
            switch (tempKey)
            {
                case ConsoleKey.D1:
                    myPlayer.Attack(myEnemy);
                    break;
                case ConsoleKey.D2:
                    myPlayer.Defend();
                    break;
                case ConsoleKey.D3:
                    myPlayer.Insult(myEnemy);
                    break;
                case ConsoleKey.D4:
                    myPlayer.UseItems(myEnemy);
                    break;
                default:
                    DoPlayerBattleAction();
                    break;
            }
        }
        
    }
}