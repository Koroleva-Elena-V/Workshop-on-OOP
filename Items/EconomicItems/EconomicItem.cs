namespace GamePrototype.Items.EconomicItems
{
    public abstract class EconomicItem : Item
    {
        // конструктор получает имена из GameConstants и передает в Item
        protected EconomicItem(string name) : base(name) 
        {
        }
    }
}
