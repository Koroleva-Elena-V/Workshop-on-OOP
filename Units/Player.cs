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


        public Player(string name, uint health, uint maxHealth, uint baseDamage) : 
            base(name, health, maxHealth, baseDamage)
        {            
        }


        // расчет урона
        public override uint GetUnitDamage()
        {
            if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is Weapon weapon) 
            {
                return BaseDamage + weapon.Damage;
            }
            return BaseDamage;
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
        public override void AddItemToInventory(Item item)
        {
            if (item is EquipItem equipItem && _equipment.TryAdd(equipItem.Slot, equipItem)) 
            {
                // Item was equipped
                return;
            }
            base.AddItemToInventory(item);
        }

        // использование предмета из инвентаря для восстановления здоровья
        private void UseEconomicItem(EconomicItem economicItem)
        {
            if (economicItem is HealthPotion healthPotion) 
            {
                Health += healthPotion.HealthRestore;
            }
        }


        //Снижение урона: учитывает броню
        protected override uint CalculateAppliedDamage(uint damage)
        {
            if (_equipment.TryGetValue(EquipSlot.Armour, out var item) && item is Armour armour)
            {
                // !!! Добавлено по заданию 1
                // Проверяем, есть ли прочность у брони
                if (armour.Durability > 0)
                {
                    damage -= (uint)(damage * (armour.Defence / 100f));

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
                    // Если прочности нет, то броня не защищает
                    _equipment.Remove(EquipSlot.Armour);
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
