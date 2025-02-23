//УДАЛЕНО, переделано в 2 класса DungeonFactory


//using GamePrototype.Dungeon;
//using GamePrototype.Items.EconomicItems;

////доп.класс, предоставляет методы для создания подземелий.

//namespace GamePrototype.Utils
//{
//    public static class DungeonBuilder
//    {
//        public static DungeonRoom BuildDungeon()  //строит новое подземелье
//        {
//            // создаются комнаты, в нужные добавляется монстр и золото
//            var enter = new DungeonRoom("Enter");
//            var monsterRoom = new DungeonRoom("Monster", UnitFactoryDemo.CreateGoblinEnemy());
//            var emptyRoom = new DungeonRoom("Empty");
//            var lootRoom = new DungeonRoom("Loot1", new Gold());
//            var lootStoneRoom = new DungeonRoom("Loot1", new Grindstone("Stone"));
//            var finalRoom = new DungeonRoom("Final", new Grindstone("Stone1"));

//            // направления
//            enter.TrySetDirection(Direction.Right, monsterRoom); //от входа направо монстр
//            enter.TrySetDirection(Direction.Left, emptyRoom); // налево - пусто

//            monsterRoom.TrySetDirection(Direction.Forward, lootRoom);//от монстра направо бонус
//            monsterRoom.TrySetDirection(Direction.Left, emptyRoom);// налево - пусто

//            emptyRoom.TrySetDirection(Direction.Forward, lootStoneRoom); // комната с камнем

//            lootRoom.TrySetDirection(Direction.Forward, finalRoom); // в финальную 
//            lootStoneRoom.TrySetDirection(Direction.Forward, finalRoom); // в финальную 

//            return enter; // возврат в первую комнату
//        }
//    }
//}
