using UI.MethodBlueprints;
using UI.Slot;
using UnityEngine;

namespace UI
{
    public class TroopTypeSlot : BaseSlot
    {
        [SerializeField] private ExtendedText _nameText;
        [SerializeField] private RecruitMethod recruitMethod;
        
        private TroopType _troopType;

        public void Setup(TroopType troopType)
        {
            _troopType = troopType;
            _nameText.SetText(troopType.name);

            if (_troopType == null)
            {
                Debug.LogError($"{name}: Troop type is null");
                return;
            }
   
            // recruitMethod.InstantiateButton(troopType, "+").transform.SetParent(transform.GetChild(1), false);
            foreach (var button in recruitMethod.GetButtonForEachOption())
            {
                button.transform.SetParent(transform.GetChild(1), false);
            }
        }
        
    }
}