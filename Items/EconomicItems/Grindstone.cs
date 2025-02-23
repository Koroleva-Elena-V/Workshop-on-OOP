namespace GamePrototype.Items.EconomicItems
{

    //Точильный камень

    public sealed class Grindstone : EconomicItem
    {
        public uint SharpeningStoneThatAddsStrength => 4;
        public override bool Stackable => false; //не складывается

        public Grindstone(string name) : base(name) //передает заданное название
        {
        }


    }
}

