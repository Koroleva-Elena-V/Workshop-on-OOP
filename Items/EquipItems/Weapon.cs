using GamePrototype.Utils;


namespace GamePrototype.Items.EquipItems
{
    public sealed class Weapon : EquipItem
    {
        public Weapon(uint damage, 
            uint durability,  
            string name) : 
            base(durability, name) 
            => Damage = damage; 


        public uint Damage { get; set;  } 

        public override EquipSlot Slot => EquipSlot.Weapon;
        

        public void IncreaseDamage(uint amount) 
        {
            Damage += amount;
        }
    }
}
