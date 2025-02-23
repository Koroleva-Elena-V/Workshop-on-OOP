using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;


//базовый класс

namespace GamePrototype.Units
{
    public abstract class Unit
    {
        private const int INVENTORY_SIZE = 3; //размер инвентаря 
        private uint _health; //текущее здоровье
        private uint _maxHealth; //максимальное здоровье
        protected uint BaseDamage; //базовый урон
        protected Inventory Inventory; // сам инвентарь

        public string Name { get; private set; } //установка имени игрока


        public uint Health
        {
            get => _health; // свойство для получения текущего здоровья
            protected set => _health = value;
        }

        public uint MaxHealth => _maxHealth;// Публ. свойство только для чтения, возвращающее макс. здоровье.



        protected Unit(string name, uint health, uint maxHealth, uint baseDamage)
        //Конструктор класса, принимающий имя, здоровье, максимальное здоровье и базовый урон.
        {
            Name = name;
            _health = health;
            _maxHealth = maxHealth;
            BaseDamage = baseDamage;
            Inventory = new Inventory(INVENTORY_SIZE);
            //просто инициализирует поля и создает новый инвентарь
        }

        public void ApplyDamage(uint damage) //Метод для нанесения урона юниту
        {
            var damageApplied = CalculateAppliedDamage(damage); //damageApplied = нанесенный урон
            if (_health < damageApplied || (_health - damageApplied) <= 0)
            {
                _health = 0;
            }
            else
            {
                _health -= damageApplied;
            }

            DamageReceiveHandler();

            //Рассчитывает примененный урон с помощью абстрактного метода CalculateAppliedDamage.
            //  Уменьшает здоровье юнита, но не ниже нуля.
            //  Вызывает виртуальный метод DamageReceiveHandler для обработки получения урона
        }


        //Абстрактный метод для расчета фактически примененного урона (например, с учетом брони).
        //Реализован в наследных классах.
        protected abstract uint CalculateAppliedDamage(uint damage);


        //Виртуальный метод для обработки того, что будет делать юнит после получения урона
        //Д.б. переопределен в наследных классах.
        protected virtual void DamageReceiveHandler() { }


        // метод для получения урона, наносимого врагом.
        public abstract uint GetUnitDamage();


        // метод для обработки завершения боя, что юнит должен делать дальше.
        public abstract void HandleCombatComplete();


        //Метод для добавления предмета в инвентарь.
        //Д.б. переопределен в наследных классах.
        public virtual void AddItemToInventory(Item item)
        {
            if (!Inventory.TryAdd(item))
            {
                Console.WriteLine($"Inventory of {Name} is full"); //Выводит сообщение в консоль, если инвентарь полон.
            }
        }



        //Метод для добавления всех предметов из другого инвентаря в инвентарь текущего игрока.
        // Проходит по всем предметам в другом инвентаре и пытается добавить их в инвентарь игрока.
        //   Прекращает добавление, если инвентарь становится полным.
        public void AddItemsFromUnitToInventory(Unit unit)
        {
            for (int i = 0; i < unit.Inventory.Items.Count; i++)
            {
                if (!Inventory.TryAdd(unit.Inventory.Items[i]))
                {
                    //inventory is full
                    return;
                }
            }
        }

        protected virtual Armour GetArmour() //для получения текущей брони
        {
            return null;
        }

    }
}

