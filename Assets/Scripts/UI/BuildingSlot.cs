using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Slot
{
    public class BuildingSlot : TwoBehaviorsSlot
    {
        // [OdinSerialize] private List<UpgradeMethod> methodList;

        [SerializeField] private ExtendedButton buildingButton;
        
        private Type _buildingType;
        private Building _building;
        private List<BuildingManager> _buildingManagers;

        public void Setup(Type buildingType, DropDownMenu dropdown, ResearchTree researchTree)
        {
            _buildingType = buildingType;
            dropdownMenu = dropdown;
            slotContentSource = researchTree;
        }

        protected override Dictionary<string, bool> GetOptionSetMenu()
        {
            var options = new Dictionary<string, bool>();
            
            foreach (var method in methodList)
                options.Add(method.GetName(), _building.IsUpgradeable);
            
            return options;
        }

        protected override bool NoOptionSetBehavior(int index)
        {
            _building = _buildingManagers[index].TryCreateBuilding();
            
            if (_building == null)
            {
                Debug.Log("_building could not be built");
                return false;
            }
            
            _building.OnUpgradeProgress += buildingButton.SetFillAmount;
            _building.OnUpgradableStatusChanged += buildingButton.SetInteractable;
            
            buildingButton.SetText(_building.name);
            
            SetSlot();
            return true;
        }
        

        protected override bool OptionSetBehavior(int index)
        {
            if(index == -1)
                Debug.Log("index is -1");
            
            methodList[index].CallMethod(_building);
            return false;
        }

        protected override Dictionary<string, bool> GetOptionNotSetMenu()
        {
            var results = new Dictionary<string, bool>();
            
            _buildingManagers = slotContentSource.GetSlotItems(_buildingType).Cast<BuildingManager>().ToList();

            foreach (var option in _buildingManagers)
            {
                results.Add(option.GetName(), option.IsAvailable);
            }
            
            return results;
        }
    }
}