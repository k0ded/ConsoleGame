using System;
using System.Collections.Generic;
using ConsoleGame.Utils;

namespace ConsoleGame
{
    internal class Program
    {

        public static void Main(string[] someArgs)
        {
            Player tempPlayer = new Player(new Random());

            while (tempPlayer.GetHealth > 0)
            {
                var tempBattle = new Battle(tempPlayer,new Ogre(new Random()));
                tempBattle.StartBattle();
            }
            
            Console.Clear();

            TextUtils.SendMessage(new []
            {
                "You have died!",
                "1: Restart    ",
                "2: Exit       "
            }, TextType.CENTERED);
            DoPlayerRestartAction();
        }
        
        private static void DoPlayerRestartAction()
        {
            var tempKey = Console.ReadKey(true).Key;
            switch (tempKey)
            {
                case ConsoleKey.D1:
                    Main(null);
                    break;
                case ConsoleKey.D2:
                    Environment.Exit(1);
                    break;
                default:
                    DoPlayerRestartAction();
                    break;
            }
        }
    }
}