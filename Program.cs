using GamePrototype.Dungeon;
using GamePrototype.Game;
using GamePrototype.Utils;

namespace GamePrototype
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose difficulty: Easy or Hard");
            string difficulty = Console.ReadLine().ToLower();

            UnitFactory unitFactory;
            DungeonFactory dungeonFactory;

            if (difficulty == "hard")
            {
                unitFactory = new HardUnitFactory();
                dungeonFactory = new HardDungeonFactory(unitFactory);
            }
            else 
            {
                unitFactory = new EasyUnitFactory();
                dungeonFactory = new EasyDungeonFactory(unitFactory);
            }

            new GameLoop(dungeonFactory, unitFactory).StartGame();


        }
    }
}