using GamePrototype.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Оружие дальнего действия

namespace GamePrototype.Items.EquipItems
{
    public class RangeWeapon : EquipItem
    {
        public uint Damage { get; set; }
        public RangeWeapon(uint damage, uint durability, string name) : base(durability, name)
        {
            Damage = damage;
        }
        public override EquipSlot Slot => EquipSlot.RangeWeapon;

        public void IncreaseDamage(uint amount)
        {
            Damage += amount;
        }
    }
}
