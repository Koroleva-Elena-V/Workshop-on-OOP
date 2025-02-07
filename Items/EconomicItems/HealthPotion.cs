namespace GamePrototype.Items.EconomicItems
{
    //зелье здоровья
    public sealed class HealthPotion : EconomicItem //наследуется от EconomicItem.
    {
        public uint HealthRestore => 7; 
        //свойство указывающее количество здоровья, которое восстанавливает это зелье.
         //  => 7;: Указывает, что свойство всегда возвращает значение 7. Полная запись: get { return 7; }.



        public override bool Stackable => false; // Указывает, что зелья здоровья нельзя складывать

        public HealthPotion(string name) //Конструктор, который принимает параметр name типа string.
            : base(name) //Вызывает конструктор базового класса EconomicItem и передает ему имя зелья.
        {
        }

        
    }
}
