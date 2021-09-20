namespace ConsoleGame
{
    public interface IEnemy
    {
        string GetName { get; }
        
        float GetHealth { get; }
        float GetDamage { get; }
        float GetCockyness { get; }
        BattleAction? AccessLastAction { get; set; }

        void Damage(float aDamage);
        
        void Attack(Player aPlayer);
        void Defend();
        void Insult(Player aPlayer);
    }
}