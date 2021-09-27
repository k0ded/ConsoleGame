namespace ConsoleGame.Items
{
    public interface IItem
    {
        string GetName { get; }
        int GetID { get; }
        void UseItem(Player player, IEnemy enemy);
    }
}