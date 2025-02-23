using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public class Helmet : EquipItem
    {
        private uint _defence;
        public string HelmetName { get; set; }
        public uint Defence
        {
            get => _defence = 5;
            set
            {
                _defence = System.Math.Clamp(value, 0, 50);
            }
        }
        public int Durability { get; set; }
        public Helmet(uint defence, uint durability, string name) : base(durability, name)
        {
            HelmetName = name;
            Defence = defence;
            Durability = (int)durability;
        }
        public override EquipSlot Slot => EquipSlot.Helmet;
    }
}

