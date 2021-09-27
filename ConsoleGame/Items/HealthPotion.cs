namespace ConsoleGame.Items
{
    public class HealthPotion : IItem
    {
        public string GetName => "Health Potion";
        public int GetID => 1;

        public void UseItem(Player player, IEnemy enemy)
        {
            player.Heal(50);
        }
    }
}