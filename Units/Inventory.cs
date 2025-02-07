using GamePrototype.Items.EconomicItems;

namespace GamePrototype.Units
{
    public sealed class Inventory
    {
        private readonly uint _capacity; 
        //_capacity = емкость = максимальное количество предметов, которые может вместить инвентарь
        
        
        private readonly List<Item> _items = new List<Item>();
        //создаем новый пустой список, который хранит фактические предметы в инвентаре при создании экземпляра класса

        public IReadOnlyList<Item> Items => _items;
        //Это объявление свойства Items.


        public Inventory(uint capacity) //конструктор, который принимает количество элементов, которые может вместить инвентарь.
            => _capacity = capacity; //присваивает значение при создании нового объекта


        //метод для добавления предмета в инвентарь
        public bool TryAdd(Item item) 
        {
            if (_items.Count == _capacity) //Проверяет, заполнен ли инвентарь
                //_items.Count возвращает текущее количество предметов в инвентаре.
                //_capacity - это максимальная вместимость инвентаря.
            {
                return false; //инвентарь заполнен
            }
            
            _items.Add(item); //предмет item добавляется в список _items
            return true;
        }


        //метод, который удаляет предмет из инвентаря
        public bool TryRemove(Item item) // принимает предмет, который нужно удалить из инвентаря
        {
            if ( _items.Count == 0 || !_items.Contains(item)) //Проверяет, пуст ли инвентарь или не содержит ли он указанный предмет.
            {
                return false; //предмет не удалось удалить
            }
            _items.Remove(item); //предмет item удаляется из списка _items
            return true;
        }

    }
}
