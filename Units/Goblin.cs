namespace GamePrototype.Units
{

    // Гоблин НПС ВРАГ

    public sealed class Goblin : Unit
    {
        public Goblin(string name, uint health, uint maxHealth, uint baseDamage) :
            base(name, health, maxHealth, baseDamage)
        //public Goblin(string name, uint health, uint maxHealth, uint baseDamage): Конструктор класса Goblin.
        //Принимает параметры:  (имя) (здоровье) (максимальное здоровье) (базовый урон) 
        //  : base(name, health, maxHealth, baseDamage): Вызывает конструктор базового класса Unit,
        //  передавая ему значения параметров.
        {
        }

        public override uint GetUnitDamage() => BaseDamage;
        //Переопределяет абстрактный метод GetUnitDamage из базового класса Unit.
        // => BaseDamage;: Возвращает базовый урон гоблина.

        public override void HandleCombatComplete() => Health = MaxHealth;
        //Переопределяет абстрактный метод HandleCombatComplete из базового класса Unit.
        //=> Health = MaxHealth;: Восстанавливает полное здоровье гоблина после завершения боя.

        protected override uint CalculateAppliedDamage(uint damage) => damage;
        // Это означает, что у гоблина нет брони или сопротивления урону.

    }
}

