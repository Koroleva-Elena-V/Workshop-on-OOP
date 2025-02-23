using GamePrototype.Utils;

// оружие

namespace GamePrototype.Items.EquipItems
{
    public sealed class Weapon : EquipItem
    {
        public Weapon(uint damage, //урон
            uint durability,  //прочность
            string name) : //имя
            base(durability, name) //Вызывает конструктор из класса EquipItem, передавая ему durability и name.
        {
            Damage = damage;
            Durability = durability;
        }

        public uint Damage { get; } // свойство про урон, наносимый оружием

        public override EquipSlot Slot => EquipSlot.Weapon;
        //Указывает, что это оружие занимает слот созданный в вкладке EquipSlot.Weapon.
    }
}

