using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using UI.MethodBlueprints;
using UI.Slot;
using UnityEngine;

namespace UI
{
    public class TroopTypeSlot : BaseSlot
    {
        [OdinSerialize] private List<RecruitMethod> methodList;

        [SerializeField] private ExtendedText _nameText;
        [SerializeField] private ExtendedButton _recruitButton_1;
        [SerializeField] private ExtendedButton _recruitButton_3;
        [SerializeField] private ExtendedButton _recruitButton_10;
        
        private TroopType _troopType;

        private void Start()
        {
            _recruitButton_1.OnClickNoParamsTest += () => methodList[0].CallMethod(_troopType);
            _recruitButton_3.OnClickNoParamsTest += () => methodList[1].CallMethod(_troopType);
            _recruitButton_10.OnClickNoParamsTest += () => methodList[2].CallMethod(_troopType);
        }

        public void Setup(TroopType troopType, string typeName)
        {
            _troopType = troopType;
            _nameText.SetText(typeName);
        }
        
        public override Dictionary<string, bool> GetDropDownOptions()
        {
            var options = new Dictionary<string, bool>();
            
            foreach (var method in methodList)
                options.Add(method.name, true);
            
            return options;
        }

        public override bool HandleOptionClicked(int index)
        {
            if(index == -1)
                Debug.Log("index is -1");
            
            methodList[index].CallMethod(_troopType);
            return false;
        }
    }
}