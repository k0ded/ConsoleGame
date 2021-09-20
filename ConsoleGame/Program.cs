using System;
using System.Collections.Generic;
using ConsoleGame.Utils;

namespace ConsoleGame
{
    internal class Program
    {

        public static void Main(string[] someArgs)
        {
            Player player = new Player(new Random());

            while (player.GetHealth > 0)
            {
                var tempBattle = new Battle(player,new Ogre(new Random()));
                tempBattle.StartBattle();
            }

            TextUtils.SendMessage("You have died!", TextType.CENTERED);
        }
    }
}