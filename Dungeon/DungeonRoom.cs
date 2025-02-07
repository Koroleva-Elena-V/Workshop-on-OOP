using GamePrototype.Items.EconomicItems;
using GamePrototype.Units;

namespace GamePrototype.Dungeon
{
    public sealed class DungeonRoom //sealed: От этого класса нельзя унаследовать другие классы
    {      
        public readonly string Name; //имя комнаты подземелья
        public readonly Unit Enemy; //юнит (враг)
        public readonly Item Loot; //класс для игровых предметов
        public readonly Dictionary<Direction, DungeonRoom> Rooms = new(); //словарь для создания структуры подземелья, соединяя комнаты вместе
        public bool IsFinal => Rooms.Count == 0; //свойство определяет, является ли комната "финальной".


        // ниже 3 конструктора:
        public DungeonRoom(string name) => Name = name; 
           // 1.Пустой конструктор, который принимает только имя комнаты

        public DungeonRoom(string name, Unit enemy) 
            //2. Конструктор, который принимает имя комнаты (name) и врага (enemy).
            //Поле Loot будет иметь значение null по умолчанию.
        {
            Name = name;
            Enemy = enemy;
        }

        public DungeonRoom(string name, Item item) 
            //3. Конструктор, который принимает имя комнаты (name) и предмет добычи (item).
            //Поле Enemy будет иметь значение null по умолчанию.
        {
            Name = name;
            Loot = item;
        }




        public bool TrySetDirection(Direction direction, DungeonRoom room) 
        {
            if (Rooms.ContainsKey(direction))
            {
                Console.WriteLine($"Room {Name} already has room for {direction.ToString()}");
                return false;
            }
            Rooms.Add(direction, room);
            return true;
        }
    }
}
