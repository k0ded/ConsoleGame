namespace ConsoleGame.Items
{
    public class ItemStack
    {
        private IItem myItem;
        private int myAmount = 1;

        public ItemStack(int anAmount, IItem anItem)
        {
            myItem = anItem;
            myAmount = anAmount;
        }
        
        public ItemStack(IItem anItem)
        {
            myItem = anItem;
        }
        
        public IItem GetItem => myItem;
        public int AccessAmount
        {
            get => myAmount;
            set => myAmount = value;
        }
    }
}