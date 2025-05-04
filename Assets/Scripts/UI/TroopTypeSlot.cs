using System.Collections.Generic;
using UI.Slot;
using UnityEngine;

namespace UI
{
    public class TroopTypeSlot : BaseSlot
    {
        [SerializeField] private ExtendedText _nameText;
        
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
            
            foreach (var method in methodList)
            {
                method.InstantiateButton(_troopType).transform.SetParent(transform.GetChild(1), false);
            }
        }
        
        public override Dictionary<string, bool> GetDropDownOptions()
        {
            var options = new Dictionary<string, bool>();
            
            foreach (var method in methodList)
                options.Add(method.GetName(), true);
            
            return options;
        }

        public override bool HandleOptionClicked(int index)
        {
            if(index == -1)
                Debug.Log("index is -1");
            
            // methodList[index].CallMethod(_troopType);
            return false;
        }
    }
}