namespace GamePrototype.Items.EconomicItems
{
   
    public sealed class HealthPotion : EconomicItem 
    {

        public uint HealthRestore { get; set; }
        

        public override bool Stackable => false; 

        public HealthPotion(string name, uint healthRestore) 
            : base(name) 
        {
            HealthRestore = healthRestore;
        }

        
    }
}
