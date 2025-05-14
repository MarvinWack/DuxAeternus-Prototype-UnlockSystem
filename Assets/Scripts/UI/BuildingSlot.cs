using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Slot
{
    public class BuildingSlot : TwoBehaviorsSlot
    {
        public event Action<Vector3, Building> DropDownCalled;
        public bool IsBuildingSet => _building != null;
        
        [SerializeField] private ExtendedButton callDropdownButton;
        [SerializeField] private Building _building;
        
        private Type _buildingType;
        private List<BuildingManager> _buildingManagers;

        public void Setup(Type buildingType, DropDownMenu dropdown, ResearchTree researchTree)
        {
            _buildingType = buildingType;
            dropdownMenu = dropdown;
            // slotContentSource = researchTree;
            callDropdownButton.OnClick += CallDropDown;
        }

        public void SetBuilding(Building building)
        {
            foreach (var method in building.GetMethods())
            {
                // Destroy(callDropdownButton.gameObject);
                // callDropdownButton = method.InstantiateButton(building);
                // callDropdownButton.transform.SetParent(transform.GetChild(1), false);
            }
            callDropdownButton.SetText(building.name);
            _building = building;
        }

        private void CallDropDown(Vector3 position)
        {
            DropDownCalled?.Invoke(position, _building);
        }
        
        protected override Dictionary<string, bool> GetOptionSetMenu()
        {
            var options = new Dictionary<string, bool>();
            
            // foreach (var method in methodList)
            //     options.Add(method.GetName(), _building.IsUpgradeable);
            
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
            
            // foreach (var method in methodList)
            // {
            //     // method.InstantiateButton(_building).transform.SetParent(transform.GetChild(1), false);
            // }
            //
            // foreach (var button in buttons)
            // {
            //     Destroy(button.transform);
            // }
            
            // _building.OnUpgradeProgress += buildingButton.SetFillAmount;
            // _building.OnUpgradableStatusChanged += buildingButton.SetInteractable;
            
            // buildingButton.SetText(_building.name);
            
            SetSlot();
            return true;
        }
        

        protected override bool OptionSetBehavior(int index)
        {
            if(index == -1)
                Debug.Log("index is -1");
            
            // methodList[index].CallMethod(_building);
            return false;
        }

        protected override Dictionary<string, bool> GetOptionNotSetMenu()
        {
            var results = new Dictionary<string, bool>();
            
            //todo: abstracten mit genereic Get-DD-options-class?
            // _buildingManagers = slotContentSource.GetSlotItems(_buildingType).Cast<BuildingManager>().ToList();

            foreach (var option in _buildingManagers)
            {
                results.Add(option.GetName(), option.IsAvailable);
            }
            
            return results;
        }
    }
}