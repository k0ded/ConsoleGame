using System;

namespace ConsoleGame
{
    public class Ogre : IEnemy
    {
        private Random myRandom;
        
        private string myName;
        private float myHealth;
        private float myMaxHealth;
        private float myDamage;
        private float myCockyness;
        private BattleAction? myLastAction;
        
        public string GetName => myName;
        public float GetHealth => myHealth;
        public float GetDamage => myDamage;
        public float GetCockyness => myCockyness;
        public BattleAction? AccessLastAction
        {
            get => myLastAction;
            set => myLastAction = value;
        }

        public void Damage(float aDamage)
        {
            myHealth -= aDamage;
        }

        public void Attack(Player aPlayer)
        {
            myLastAction = BattleAction.ATTACK;
            if (aPlayer.AccessLastAction == BattleAction.DEFEND)
                aPlayer.Damage(myDamage / 2);
            else
                aPlayer.Damage(myDamage);
        }

        public void Defend()
        {
            myLastAction = BattleAction.DEFEND;
            float tempHealAmount = (float) (30f * myRandom.NextDouble());
            myLastAction = BattleAction.DEFEND;
            myHealth = myHealth + tempHealAmount > myMaxHealth ? myMaxHealth : myHealth + tempHealAmount;
        }

        public void Insult(Player aPlayer)
        {
            myLastAction = BattleAction.INSULT;
            if(myCockyness > aPlayer.GetCockyness)
                aPlayer.Damage(aPlayer.GetHealth);
        }

        public Ogre(Random aRandom, string aName = "Ogre", float someHealth = 200f, float someDamage = 40, float someCockyness = 50)
        {
            myRandom = aRandom;
            myName = aName;
            myHealth = someHealth;
            myMaxHealth = someHealth;
            myDamage = someDamage;
            myCockyness = someCockyness;
        }
    }
}