using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    //  Броня - Armour
    public sealed class Armour : EquipItem //наследуется от EquipItem.
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
                _defence = System.Math.Clamp(value, 5, 50); // Характеристики брони. Название, защита. Защита - % от урона, который
                //можно заблокировать. По - умолчанию, равно 5.Максимальное значение - 50.

            }
        }

        public int Durability { get; set; } // Прочность брони

        public override EquipSlot Slot => EquipSlot.Armour;
    }
}
