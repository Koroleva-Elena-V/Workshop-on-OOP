using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    //  Броня - Armour
    public sealed class Armour : EquipItem //наследуется от EquipItem.
    {

        public Armour(uint defence, //защита
            uint durability, //прочность
            string name) : //название
            base(durability, name) //Вызывает конструктор базового класса EquipItem, передавая ему durability и name.
        {
            Defence = defence; //Присваивает значение параметра защиты(defence) свойству Defence.

        }
        public uint Defence { get; } // свойство защиты

        public override EquipSlot Slot //Переопределяет абстрактное свойство Slot из базового класса EquipItem.
            => EquipSlot.Armour; //Указывает, что этот предмет брони занимает слот EquipSlot.Armour.
    }
}

