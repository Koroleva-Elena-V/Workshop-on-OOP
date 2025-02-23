using GamePrototype.Items.EconomicItems;
using GamePrototype.Utils;

namespace GamePrototype.Dungeon
{
    public abstract class DungeonFactory
    {
        public abstract DungeonRoom BuildDungeon();
    }

    public class EasyDungeonFactory : DungeonFactory
    {
        private readonly UnitFactory _unitFactory;

        public EasyDungeonFactory(UnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
        }

        public override DungeonRoom BuildDungeon()
        {
            var enter = new DungeonRoom("Enter");
            var monsterRoom = new DungeonRoom("Monster", _unitFactory.CreateGoblinEnemy());
            var emptyRoom = new DungeonRoom("Empty");
            var lootRoom = new DungeonRoom("Loot1", new Gold());
            var lootStoneRoom = new DungeonRoom("Loot1", new Grindstone("Stone"));
            var finalRoom = new DungeonRoom("Final", new Grindstone("Stone1"));

            enter.TrySetDirection(Direction.Right, monsterRoom);
            enter.TrySetDirection(Direction.Left, emptyRoom);

            monsterRoom.TrySetDirection(Direction.Forward, lootRoom);
            monsterRoom.TrySetDirection(Direction.Left, emptyRoom);

            emptyRoom.TrySetDirection(Direction.Forward, lootStoneRoom);

            lootRoom.TrySetDirection(Direction.Forward, finalRoom);
            lootStoneRoom.TrySetDirection(Direction.Forward, finalRoom);

            return enter;
        }
    }

    public class HardDungeonFactory : DungeonFactory
    {
        private readonly UnitFactory _unitFactory;

        public HardDungeonFactory(UnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
        }

        public override DungeonRoom BuildDungeon()
        {
            var enter = new DungeonRoom("Enter");
            var monsterRoom = new DungeonRoom("Monster", _unitFactory.CreateGoblinEnemy());
            var monsterRoom2 = new DungeonRoom("Monster2", _unitFactory.CreateGoblinEnemy());
            var emptyRoom = new DungeonRoom("Empty");
            var lootRoom = new DungeonRoom("Loot1", new Gold());
            var lootStoneRoom = new DungeonRoom("Loot1", new Grindstone("Stone"));
            var finalRoom = new DungeonRoom("Final", new Grindstone("Stone1"));

            enter.TrySetDirection(Direction.Right, monsterRoom);
            enter.TrySetDirection(Direction.Left, monsterRoom2);

            monsterRoom.TrySetDirection(Direction.Forward, lootRoom);
            monsterRoom2.TrySetDirection(Direction.Forward, emptyRoom);

            emptyRoom.TrySetDirection(Direction.Forward, lootStoneRoom);

            lootRoom.TrySetDirection(Direction.Forward, finalRoom);
            lootStoneRoom.TrySetDirection(Direction.Forward, finalRoom);

            return enter;
        }
    }
}

