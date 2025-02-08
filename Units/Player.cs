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
        //- это надежный подход к управлению экипированными предметами

        private const float ArmourPercentageDivisor = 100f; 

        public Player(string name, uint health, uint maxHealth, uint baseDamage) : 
            base(name, health, maxHealth, baseDamage)
        {            
        }


        // расчет урона
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



        //Обработка завершения боя, обрабатывает экономические предметы (например, зелья здоровья) в конце боя
        public override void HandleCombatComplete()
        {
            var items = Inventory.Items.ToList(); // Создаем копию списка
            foreach (var item in items)
            {
                if (item is EconomicItem economicItem)
                {
                    UseEconomicItem(economicItem);
                    Inventory.TryRemove(item);
                }
            }
        }


        //Управление инвентарем: обрабатывает непосредственное экипирование предметов.
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
                EquipItem(equipItem); // Use the explicit equip method.
                return;
            }
            base.AddItemToInventory(item);
        }

        // использование предмета из инвентаря для восстановления здоровья
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
                // Слот занят.  Обрабатывать соответственно (замена, удаление, ошибка)
                UnequipItem(item.Slot); // Пример: обмен предметами
            }

            if (_equipment.TryAdd(item.Slot, item))
            {
                Inventory.TryRemove(item); // Удалить из инвентаря, если экипировано.
                // Вызовите событие, если у вас есть система событий: OnItemEquipped?.Invoke(item);
                Console.WriteLine($"Экипировано: {item.Name} в слот {item.Slot}"); // Уведомление о замене
                return true;
            }
            return false; // Экипировать не удалось.
        }

        public void UnequipItem(EquipSlot slot)
        {
            if (_equipment.TryGetValue(slot, out var item))
            {
                _equipment.Remove(slot);
                Inventory.TryAdd(item); //Добавить в инвентарь, если он не экипирован.
                // Вызовите событие: OnItemUnequiped?.Invoke(item);
                Console.WriteLine($"Снято: {item.Name} из слота {slot}"); // Уведомление о снятии
            }
        }

        
        protected override uint CalculateAppliedDamage(uint damage)
        {
            if (_equipment.TryGetValue(EquipSlot.Armour, out var item) && item is Armour armour)
            {
                if (armour.Durability > 0)
                {
                    damage -= (uint)(damage * (armour.Defence / ArmourPercentageDivisor));

                    // Уменьшаем прочность брони
                    armour.Durability--;

                    // Если прочность брони закончилась, снимаем ее
                    if (armour.Durability <= 0)
                    {
                        _equipment.Remove(EquipSlot.Armour);
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
                    }
                }
                else
                {
                    _equipment.Remove(EquipSlot.Helmet);
                }
            }
            return damage;
        }

        //Метод для использования точильного камня
        public bool UseGrindstone(Weapon weapon, Grindstone grindstone)
        {
            if (weapon == null || grindstone == null)
            {
                Console.WriteLine("No weapon or whetstone specified"); //Не указано оружие или точильный камень
                return false;
            }

            //Проверка на наличие точильного камня в инвентаре
            if (!Inventory.Items.Contains(grindstone))
            {
                Console.WriteLine("There is no whetstone in inventory"); //В инвентаре нет точильного камня
                return false;
            }

            //Увеличение урона оружия (прочности)
            weapon.Damage += 4;

            //Удаление точильного камня из инвентаря
            Inventory.TryRemove(grindstone);
            Console.WriteLine($"Grindstone {grindstone.Name} used on weapon {weapon.Name}, damage increased by 4!");
            return true;
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
