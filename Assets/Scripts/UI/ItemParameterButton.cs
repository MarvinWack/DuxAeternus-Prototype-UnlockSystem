using Production.Items;
using UI.Slot;
using UnityEngine;

namespace UI
{
    public class ItemParameterButton : ExtendedButton
    {
        [SerializeField] private ItemBlueprint _parameter;
        
        public void SetParameter(ItemBlueprint parameter)
        {
            _parameter = parameter;
        }

        public ItemBlueprint GetParameter()
        {
            return _parameter;
        }
    }
}