using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

//фабрика для игрока

namespace GamePrototype.Utils
{
    public class UnitFactoryDemo
    {
        //метод для создания игрока
        public static Unit CreatePlayer(string name)
        {
            var player = new Player(name, 30, 30, 6);
            player.AddItemToInventory(new Weapon(10, 15, "Sword")); //статы оружия
            player.AddItemToInventory(new Armour(10, 15, "Armour")); //статы брони
            player.AddItemToInventory(new HealthPotion("Potion")); //статы зелья здоровья
            return player;
        }

        //метод для создания гоблина
        public static Unit CreateGoblinEnemy() => new Goblin(GameConstants.Goblin, 18, 18, 2); //статы гоблина
    }
}
