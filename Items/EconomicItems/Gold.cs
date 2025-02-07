using GamePrototype.Utils;

namespace GamePrototype.Items.EconomicItems
{
    //золотишко
    public sealed class Gold : EconomicItem
    {
        public override bool Stackable => true; //складируется

        public Gold() : base(GameConstants.Gold) // просто конструктор хз для чего.. имя получает из вкладки EconomicItem?! 
        {            
        }       
    }
}
