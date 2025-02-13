using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public sealed class Armour : EquipItem 
    {
        private uint _defence;

        public Armour(uint defence, uint durability, string name) : base(durability, name)
        {
            Defence = defence;
            Durability = (int)durability; 
        }

        public uint Defence
        {
            get => _defence;
            set
            {
                _defence = Math.Clamp(value, 5, 50); 

            }
        }

        public int Durability { get; set; } 

        public override EquipSlot Slot => EquipSlot.Armour; 
    }
}
