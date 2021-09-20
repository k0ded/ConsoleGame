using System;

namespace ConsoleGame
{
    public class Player
    {
        private Random myRandom;
        private string myName;
        private float myHealth;
        private float myMaxHealth;
        private BattleAction? myLastAction;
        
        private float myDamage;
        private float myCockyness;
        
        public string GetName => myName;
        public float GetHealth => myHealth;
        public float GetDamage => myDamage;
        public float GetCockyness => myCockyness;
        public BattleAction? AccessLastAction
        {
            get => myLastAction;
            set => myLastAction = value;
        }

        public Player(Random aRandom, int aMaxHealth = 100, int aMaxDamage = 100, int aMaxCockyness = 100)
        {
            myRandom = aRandom;

            var tempHealthFactor = aRandom.NextDouble();
            var tempDamageFactor = 1 - (aRandom.NextDouble() * tempHealthFactor);
            var tempCockynessFactor = 1 - (aRandom.NextDouble() * tempDamageFactor);

            myHealth = (int) (tempHealthFactor * aMaxHealth);
            myMaxHealth = myHealth;
            myDamage = (int) (tempDamageFactor * aMaxDamage);
            myCockyness = (int) (tempCockynessFactor * aMaxCockyness);
        }

        public void Damage(float aDamage)
        {
            myHealth -= aDamage;
        }
        
        public void Insult(IEnemy aEnemy)
        {
            myLastAction = BattleAction.INSULT;
            if (myCockyness > aEnemy.GetCockyness)
                aEnemy.Damage(aEnemy.GetHealth);
        }

        public void Defend()
        {
            float tempHealAmount = (float) (30f * myRandom.NextDouble());
            myLastAction = BattleAction.DEFEND;
            myHealth = myHealth + tempHealAmount > myMaxHealth ? myMaxHealth : myHealth + tempHealAmount;
        }

        public void Attack(IEnemy aEnemy)
        {
            myLastAction = BattleAction.ATTACK;
            if (aEnemy.AccessLastAction == BattleAction.DEFEND)
            {
                aEnemy.Damage(myDamage/2);
            }
            else
            {
                aEnemy.Damage(myDamage);
            }
        }
    }
}