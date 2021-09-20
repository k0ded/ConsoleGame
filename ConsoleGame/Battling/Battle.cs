using System;
using System.Collections.Generic;
using ConsoleGame.Utils;

namespace ConsoleGame
{
    public class Battle
    {
        private Player myPlayer;
        private IEnemy myEnemy;
        private bool myBattlingFlag;

        public Battle(Player aPlayer, IEnemy aEnemy)
        {
            myPlayer = aPlayer;
            myEnemy = aEnemy;
        }

        public void StartBattle()
        {
            TextUtils.SendMessage($"You've encountered an {myEnemy.GetName}!", TextType.HEADERBAR);
            myBattlingFlag = true;
            
            while (myBattlingFlag)
            {
                TextUtils.SendMessage(new []
                {
                    "What should we do about them?",
                    "1: Attack                    ",
                    "2: Defend                    ",
                    "3: Insult                    "
                }, TextType.CENTERED);

                DoPlayerBattleAction();

                if (myEnemy.GetHealth > 0 && myPlayer.GetHealth > 0)
                    myBattlingFlag = false;
            }
            
            Console.Clear();
            
            TextUtils.SendMessage(new []
            {
                "You won the battle!",
                "1: Continue        ",
                "2: Exit            "
            }, TextType.CENTERED);
            DoPlayerContinueAction();

            myPlayer.AccessLastAction = null;
            myEnemy.AccessLastAction = null;
        }

        private void DoPlayerContinueAction()
        {
            var tempKey = Console.ReadKey(true).Key;
            switch (tempKey)
            {
                case ConsoleKey.D1:
                    break;
                case ConsoleKey.D2:
                    Environment.Exit(1);
                    break;
                default:
                    DoPlayerContinueAction();
                    break;
            }
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
                default:
                    DoPlayerBattleAction();
                    break;
            }
        }
    }
}