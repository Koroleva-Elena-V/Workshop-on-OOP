namespace GamePrototype.Items.EconomicItems
{
    
    //Точильный камень

    public sealed class Grindstone : EconomicItem
    {
        public override bool Stackable => false; //не складывается

        public Grindstone(string name) : base(name) 
        {
        }

        
    }
}
