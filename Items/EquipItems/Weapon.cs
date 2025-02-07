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
            => Damage = damage; 


        public uint Damage { get; set;  } // свойство про урон, наносимый оружием (добавила от себя Set чтобы можно было добавить + урон от точильного камня)

        public override EquipSlot Slot => EquipSlot.Weapon; 
        //Указывает, что это оружие занимает слот созданный в вкладке EquipSlot.Weapon.
    }
}
