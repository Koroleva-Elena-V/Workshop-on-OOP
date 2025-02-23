using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

//Abstract UnitFactory
namespace GamePrototype.Utils
{
    public abstract class UnitFactory
    {
        public abstract Unit CreatePlayer(string name);
        public abstract Unit CreateGoblinEnemy();
    }

    public class EasyUnitFactory : UnitFactory //простой
    {
        public override Unit CreatePlayer(string name)
        {
            var player = new Player(name, 30, 30, 6);
            player.AddItemToInventory(new Weapon(10, 15, "Sword"));
            player.AddItemToInventory(new Armour(10, 15, "Armour"));
            player.AddItemToInventory(new HealthPotion("Potion"));
            return player;
        }

        public override Unit CreateGoblinEnemy()
        {
            return new Goblin(GameConstants.Goblin, 18, 18, 2);
        }
    }

    public class HardUnitFactory : UnitFactory //сложный
    {
        public override Unit CreatePlayer(string name)
        {
            var player = new Player(name, 20, 20, 4); 
            player.AddItemToInventory(new Weapon(7, 10, "Sword")); 
            player.AddItemToInventory(new Armour(7, 10, "Armour")); 
            return player;
        }

        public override Unit CreateGoblinEnemy()
        {
            return new Goblin(GameConstants.Goblin, 25, 25, 5); 
        }
    }
}