using System;
using System.Threading;
using ConsoleGame.Utils;

namespace ConsoleGame
{
    public class Player
    {
        private Random myRandom;
        private string myName;
        private float myHealth;
        private float myMaxHealth;
        private BattleAction? myLastAction;
        private Inventory myInventory;
        
        private float myDamage;
        private float myCockyness;
        
        public string GetName => myName;
        public float GetHealth => myHealth;
        public float GetDamage => myDamage;
        public float GetCockyness => myCockyness;
        public Inventory GetInventory => myInventory;

        public BattleAction? AccessLastAction
        {
            get => myLastAction;
            set => myLastAction = value;
        }

        public Player(Random aRandom, int aMaxHealth = 1000, int aMaxDamage = 100, int aMaxCockyness = 100)
        {
            myRandom = aRandom;

            var tempHealthFactor = aRandom.NextDouble();
            var tempDamageFactor = 1 - (aRandom.NextDouble() * tempHealthFactor);
            var tempCockynessFactor = 1 - (aRandom.NextDouble() * tempDamageFactor);

            myHealth = (int) (tempHealthFactor * aMaxHealth);
            myMaxHealth = myHealth*4;
            myDamage = (int) (tempDamageFactor * aMaxDamage);
            myCockyness = (int) (tempCockynessFactor * aMaxCockyness);
            myInventory = new Inventory(this, 5);
        }

        public void Damage(float aDamage)
        {
            myHealth -= aDamage;
        }

        public void Heal(float aAmount)
        {
            myHealth += aAmount;
            if (myHealth > myMaxHealth)
                myHealth = myMaxHealth;
        }
        
        public void Insult(IEnemy aEnemy)
        {
            myLastAction = BattleAction.INSULT;
            
            if (myCockyness > aEnemy.GetCockyness)
                aEnemy.Damage(aEnemy.GetHealth);
        }

        public void Defend()
        {
            myLastAction = BattleAction.DEFEND;
            
            float tempHealAmount = (float) (30f * myRandom.NextDouble());
            myHealth = myHealth + tempHealAmount > myMaxHealth ? myMaxHealth : myHealth + tempHealAmount;
        }

        public void Attack(IEnemy aEnemy)
        {
            myLastAction = BattleAction.ATTACK;
            var tempDamage = 
                aEnemy.AccessLastAction == BattleAction.DEFEND 
                ? myDamage / 2 
                : myDamage;
            aEnemy.Damage(tempDamage);
        }

        public void UseItems(IEnemy aEnemy)
        {
            Console.Clear();
            TextUtils.SendMessage("Which Item would you like to use?", TextType.CENTERED);
            TextUtils.SendMessage(myInventory.GetItemList(), TextType.INVENTORY);
            if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int tempResult) && tempResult > 0 && tempResult <= myInventory.GetSize)
            {
                myInventory.UseItem(tempResult, aEnemy);
            }
            else
            {
                UseItems(aEnemy);
            }
        }
    }
}