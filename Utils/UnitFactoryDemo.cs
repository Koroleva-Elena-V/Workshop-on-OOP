using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;


namespace GamePrototype.Utils
{
    public static class UnitFactoryDemo
    {
        public static Unit CreatePlayer(string name, Difficulty difficulty)
        {
            var player = new Player(name, 30, 30, 6);
            player.AddItemToInventory(new Weapon(10, 15, "Sword")); 
            player.AddItemToInventory(new Armour(10, 15, "Armour")); 
            player.AddItemToInventory(new HealthPotion("Potion", 7)); 
            return player;
        }

        public static Unit CreateGoblinEnemy(Difficulty difficulty)
        {
            uint health = difficulty == Difficulty.Easy ? 18u : 36u;
            uint damage = difficulty == Difficulty.Easy ? 2u : 4u; 
            return new Goblin("Goblin", health, health, damage);
        }

    }

}
