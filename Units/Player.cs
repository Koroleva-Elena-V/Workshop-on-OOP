using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;
using System.Text;

namespace GamePrototype.Units
{
    public sealed class Player : Unit
    {
        private readonly Dictionary<EquipSlot, EquipItem> _equipment = new();
        //Словарь _equipment, использующий EquipSlot в качестве ключа,


        public Player(string name, uint health, uint maxHealth, uint baseDamage) :
            base(name, health, maxHealth, baseDamage)
        {
        }


        // расчет урона
        public override uint GetUnitDamage()
        {
            // //проверяет, экипировано ли у юнита оружие и если да, пытается получить его Weapon
            if (_equipment.TryGetValue(EquipSlot.Weapon, out var mainWeaponItem) && mainWeaponItem is Weapon mainWeapon)
            {
                return BaseDamage + mainWeapon.Damage;
            }
            // Если Weapon не экипировано, проверяем наличие RangeWeapon.
            else if (_equipment.TryGetValue(EquipSlot.RangeWeapon, out var rangeWeaponItem) && rangeWeaponItem is RangeWeapon rangeWeapon)
            {
                return BaseDamage + rangeWeapon.Damage;
            }
            return BaseDamage; //возвращает сумму базового урона, если нема оружия

        }


        //Обработка завершения боя, обрабатывает экономические предметы (например, зелья здоровья) в конце боя
        public override void HandleCombatComplete()
        {
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] is EconomicItem economicItem)
                {
                    UseEconomicItem(economicItem);
                    Inventory.TryRemove(items[i]);
                }
            }
        }


        //Управление инвентарем: обрабатывает непосредственное экипирование предметов.
        //public override void AddItemToInventory(Item item)
        //{
        //    if (item is EquipItem equipItem && _equipment.TryAdd(equipItem.Slot, equipItem))
        //    {
        //        // Item was equipped
        //        return;
        //    }
        //    base.AddItemToInventory(item);
        //}


        public override void AddItemToInventory(Item item)
        {
            if (item is EquipItem equipItem)
            {
                if (_equipment.TryGetValue(equipItem.Slot, out var oldEquipment))
                {
                    // Спрашиваем игрока, хочет ли он заменить экипировку
                    Console.WriteLine($"You have received {equipItem.Name}. You already have {oldEquipment.Name} in slot {equipItem.Slot}. Replace? (Yes/No)");
                    string choice = Console.ReadLine();

                    if (choice.ToLower() == "yes")
                    {
                        // Заменяем экипировку
                        base.AddItemToInventory(oldEquipment); // Возвращаем старую экипировку в инвентарь
                        Console.WriteLine($"{oldEquipment.Name} was removed and returned to inventory.");
                        _equipment[equipItem.Slot] = equipItem; // Экипируем новую
                        Console.WriteLine($"Equipped {equipItem.Name} in slot{equipItem.Slot}");
                    }
                    else
                    {
                        // Не заменяем, просто добавляем новую в инвентарь
                        base.AddItemToInventory(equipItem);
                        Console.WriteLine($"{equipItem.Name} added to inventory.");
                    }
                }
                else
                {
                    // Слот экипировки пуст, сразу экипируем
                    _equipment[equipItem.Slot] = equipItem;
                    Console.WriteLine($"Equipped {equipItem.Name} in slot {equipItem.Slot}");
                }
            }
            else
            {
                base.AddItemToInventory(item);
            }
        }


        // использование предмета из инвентаря для восстановления здоровья или прочности оружия
        private void UseEconomicItem(EconomicItem economicItem)
        {
            if (economicItem is HealthPotion healthPotion)
            {
                Health += healthPotion.HealthRestore;
            }

            if (economicItem is Grindstone grindstone)
            {
                // Попытка получить экипированное оружие
                if (TryGetEquippedWeapon(out var weapon))
                {

                    weapon.RepairWeapon(grindstone.SharpeningStoneThatAddsStrength); // Используем метод Repair, чтобы увеличить Durability
                    Console.WriteLine($"You used {grindstone.Name} on your {weapon.Name} to increase durability.");

                }
            }
        }

        //Пробуем получить экипированное оружие
        public bool TryGetEquippedWeapon(out Weapon weapon)
        {
            if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is Weapon w)
            {
                weapon = w;
                return true;

            }

            weapon = null;
            return false;
        }


        //Снижение здоровья,  при получении урона, учитывает броню
        protected override uint CalculateAppliedDamage(uint damage)
        {
            if (_equipment.TryGetValue(EquipSlot.Armour, out var item) && item is Armour armour)
            {
                damage -= (uint)(damage * (armour.Defence / 100f));
                armour.ReduceDurability(1); // Броня теряет один пункт прочности
            }
            return damage;

        }


        //Обработчик получения повреждений
        protected override void DamageReceiveHandler()
        {
            if (_equipment.TryGetValue(EquipSlot.Armour, out var item) && item is Armour armour)
            {
                if (armour.Durability <= 0)
                {
                    Console.WriteLine($"{armour.Name} is broken!");
                    _equipment.Remove(EquipSlot.Armour);
                }
            }
        }

        protected override Armour GetArmour() //переопределяет метод из Unit, пробует использовать броник
        {
            if (_equipment.TryGetValue(EquipSlot.Armour, out var item) && item is Armour armour)
            {
                return armour; // использует броню
            }
            return null; //ее нет
        }


        //Предоставляет текстовую информацию по состоянию игрока
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
            return builder.ToString();
        }
    }
}

