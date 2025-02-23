using GamePrototype.Items.EconomicItems;
using GamePrototype.Utils;


// экипировка

namespace GamePrototype.Items.EquipItems
{
    // наследуется от Item
    public abstract class EquipItem : Item
    {
        private uint _durability; // представляет текущую прочность предмета
        private uint _maxDurability; //представляет максимальную прочность предмета


        public uint Durability
        {
            get => _durability; //Получение значения свойства перенаправляется к _durability. 
            protected set => _durability = value; //Установка значения свойства перенаправляется к  _durability. 
        }


        public override bool Stackable //Переопределяет свойство Stackable из базового класса Item.
            => false;  //Указывает, что надеваемые предметы нельзя складывать в одну ячейку.

        public abstract EquipSlot Slot { get; } // Свойство определяет, в какой слот экипировки можно положить этот предмет

        protected EquipItem
            (uint maxDurability,
            string name) : //Конструктор класса EquipItem.
                           //Принимает maxDurability (максимальная прочность) и name (имя предмета) в качестве параметров.
            base(name)    //Вызывает конструктор базового класса Item и передает ему имя предмета.
            => _maxDurability = maxDurability; //Присваивает значение параметра maxDurability приватному полю _maxDurability.



        //public void ReduceDurability(uint delta) //метод, который уменьшает прочность предмета на величину delta.
        //    => _durability -= delta; // Уменьшает значение приватного поля _durability на delta.

        public void ReduceDurability(uint delta) // Новая версия, чтобы задавать минимальное значение 0 для Durability
                                                 // и по итогу отнимать по 1 очку у брони при получении урона
        {
            if (Durability >= delta)
            {
                Durability -= delta;
            }
            else
            {
                Durability = 0;
            }
        }


        //Тернарный оператор, который гарантирует, что прочность не превысит максимальную.
        //Если добавление delta приведет к превышению _maxDurability, то _durability устанавливается равным _maxDurability,
        //в противном случае к _durability добавляется delta.
        public void Repair(uint delta) =>
            _durability += _durability + delta > _maxDurability // можно было записать через if
            ? _maxDurability // положительное условие
            : _durability + delta; // отрицательное условие



        //метод Repair для оружия.
        public void RepairWeapon(uint delta)
        {
            _durability += _durability + delta > _maxDurability // можно было записать через if
            ? _maxDurability // положительное условие
            : _durability + delta; // отрицательное условие
        }
    }
}


