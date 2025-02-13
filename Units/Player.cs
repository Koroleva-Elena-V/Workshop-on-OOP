using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;
using System.Text;

namespace GamePrototype.Units
{
    public sealed class Player : Unit
    {
        private readonly Dictionary<EquipSlot, EquipItem> _equipment = new();
       
        private const float ArmourPercentageDivisor = 100f; 

        public Player(string name, uint health, uint maxHealth, uint baseDamage) : 
            base(name, health, maxHealth, baseDamage)
        {            
        }
               
        public override uint GetUnitDamage()
        {
            EquipItem? item = null;
            if (_equipment.TryGetValue(EquipSlot.Weapon, out item) && item is Weapon weapon)
            {
                return BaseDamage + weapon.Damage;
            }

            if (_equipment.TryGetValue(EquipSlot.RangeWeapon, out item) && item is RangeWeapon rangeWeapon)
            {
                return BaseDamage + rangeWeapon.Damage;
            }
            return BaseDamage;
        }

               
        public override void HandleCombatComplete()
        {
            var items = Inventory.Items.ToList(); 
            foreach (var item in items)
            {
                if (item is EconomicItem economicItem)
                {
                    UseEconomicItem(economicItem);
                    Inventory.TryRemove(item);
                }
            }
        }

        public override void AddItemToInventory(Item item)
        {
            //if (item is EquipItem equipItem && _equipment.TryAdd(equipItem.Slot, equipItem)) 
            //{
            //    // Item was equipped
            //    return;
            //}
            //base.AddItemToInventory(item);

            if (item is EquipItem equipItem)
            {
                EquipItem(equipItem); 
                return;
            }
            base.AddItemToInventory(item);
        }

        
        private void UseEconomicItem(EconomicItem economicItem)
        {
            if (economicItem is HealthPotion healthPotion)
            {
                Health = Math.Min(Health + healthPotion.HealthRestore, MaxHealth);
            }
        }
        public bool EquipItem(EquipItem item)
        {
            if (_equipment.ContainsKey(item.Slot))
            {
                
                UnequipItem(item.Slot); 
            }

            if (_equipment.TryAdd(item.Slot, item))
            {
                Inventory.TryRemove(item); 
                
                Console.WriteLine($"Экипировано: {item.Name} в слот {item.Slot}"); 
                return true;
            }
            return false; 
        }

        public void UnequipItem(EquipSlot slot)
        {
            if (_equipment.TryGetValue(slot, out var item))
            {
                _equipment.Remove(slot);
                Inventory.TryAdd(item); 
                
                Console.WriteLine($"Снято: {item.Name} из слота {slot}"); 
            }
        }

        
        protected override uint CalculateAppliedDamage(uint damage)
        {
            if (_equipment.TryGetValue(EquipSlot.Armour, out var item) && item is Armour armour)
            {
                if (armour.Durability > 0)
                {
                    damage -= (uint)(damage * (armour.Defence / ArmourPercentageDivisor));
                                        
                    armour.ReduceDurability(1);
                                        
                    if (armour.Durability <= 0)
                    {
                        _equipment.Remove(EquipSlot.Armour);
                        Console.WriteLine("Armor is not active"); 
                    }
                }
                else
                {
                    _equipment.Remove(EquipSlot.Armour);
                }
            }
            if (_equipment.TryGetValue(EquipSlot.Helmet, out item) && item is Helmet helmet)
            {
                if (helmet.Durability > 0)
                {
                    damage -= (uint)(damage * (helmet.Defence / ArmourPercentageDivisor));
                    helmet.Durability--;
                    if (helmet.Durability <= 0)
                    {
                        _equipment.Remove(EquipSlot.Helmet);
                        Console.WriteLine("The helmet is not active"); 
                    }
                }
                else
                {
                    _equipment.Remove(EquipSlot.Helmet);
                }
            }
            return damage;
        }

        
        public bool UseGrindstone(Weapon weapon, Grindstone grindstone)
        {
            
            if (!Inventory.Items.Contains(grindstone))
            {
                Console.WriteLine("There is no whetstone in inventory"); 
                return false;
            }
                       
            weapon.Damage += 4;
            
            Inventory.TryRemove(grindstone);
            Console.WriteLine($"Grindstone {grindstone.Name} used on weapon {weapon.Name}, damage increased by 4!");
            return true;
        }

        
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(Name);
            builder.AppendLine($"Health {Health}/{MaxHealth}");
            builder.AppendLine("Loot:");
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++)
            {
                builder.AppendLine($"[{items[i].Name}] : {items[i].Amount}");
            }
            builder.AppendLine("Equipment:");
           
            foreach (var item in _equipment)
            {
                builder.AppendLine($"- {item.Value.Name} ({item.Key}) Durability: {item.Value.Durability}"); 
            }
            return builder.ToString();
        }
    }
}
