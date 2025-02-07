using GamePrototype.Units;

namespace GamePrototype.Combat
{
    public sealed class CombatManager
    {
        private readonly Random _random = new(); //задаем поле рандома
        
        public Unit StartCombat(Unit player, Unit enemy) => PlayCombatRoutine(player, enemy); 
        //создаем поле запуска создания персонажей


        // как происходит бой
        private Unit PlayCombatRoutine(Unit player, Unit enemy)
        {
            Console.WriteLine(GetCombatString()); //получаем инфо о текущем состоянии боя, подробно расписано ниже - GetCombatString
            while (player.Health > 0 && enemy.Health > 0) // запускается проверка состояния здоровья с условиями
            {
                if (Enum.TryParse<RockPaperScissors>(Console.ReadLine(), out var rockPaperScissors)) 
                {
                    HandleCombatInput(player, enemy, rockPaperScissors);
                }
                else
                {
                    Console.WriteLine(GetCombatString());
                }
            }
            if (player.Health > 0 && enemy.Health == 0) 
            {
                return player;
            }
            else if (player.Health == 0 && enemy.Health > 0) 
            {
                return enemy;
            }

            return null;
        }


        // выводит подробное описание того, что должен сделать игрок
        private string GetCombatString() => $"Type {RockPaperScissors.Rock} = {(int)RockPaperScissors.Rock}" +
            $"or {RockPaperScissors.Paper} = {(int)RockPaperScissors.Paper}" +
            $"or {RockPaperScissors.Scissors} = {(int)RockPaperScissors.Scissors}";



        // ооочень подробно каждое действие боя
        private void HandleCombatInput(Unit player, Unit enemy, RockPaperScissors rockPaperScissors)
        {
            var enemyInput = (RockPaperScissors) _random.Next(1, 3); //ввод игрока = рандом НПС
            Console.WriteLine($"Result player = {rockPaperScissors} and enemy = {enemyInput}");
            switch (rockPaperScissors) 
            {
                // player hit
                case RockPaperScissors.Rock when enemyInput == RockPaperScissors.Scissors:
                    ApplyDamage(player, enemy);
                    break;
                case RockPaperScissors.Scissors when enemyInput == RockPaperScissors.Paper:
                    ApplyDamage(player, enemy);
                    break;
                case RockPaperScissors.Paper when enemyInput == RockPaperScissors.Rock:
                    ApplyDamage(player, enemy);
                    break;
                // enemy hit
                case RockPaperScissors.Scissors when enemyInput == RockPaperScissors.Rock:
                    ApplyDamage(enemy, player);
                    break;
                case RockPaperScissors.Paper when enemyInput == RockPaperScissors.Scissors:
                    ApplyDamage(enemy, player);
                    break;
                case RockPaperScissors.Rock when enemyInput == RockPaperScissors.Paper:
                    ApplyDamage(enemy, player);
                    break;
                default:
                    Console.WriteLine("Combatants tried to hit, but missed :(");
                    break;
            }
        }


        //метод вызывающий атакующего и защищающегося
        private void ApplyDamage(Unit attacker, Unit defender)
        {
            defender.ApplyDamage(attacker.GetUnitDamage()); // защищающийся получает урон от нападающего
            Console.WriteLine($"{attacker.Name} hits {defender.Name}. {defender.Name} health {defender.Health}/{defender.MaxHealth}");
            if (defender.Health == 0) 
            {
                Console.WriteLine($"{defender.Name} is dead!"); // если здоровье =0, то вывод смс о смерти
            }
        }
    }
}
