using System;
using System.Collections.Generic;
using Entities.Buildings;
using Objects;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class BuildingSlot : MonoBehaviour, IDropdownCaller, IProgressVisualiser
    { 
        public event IDropdownCaller.OptionSetHandler OptionSet;
        public event Action<float> OnUpgradeProgress;
        
        private ISlotContentSource researchTreeNEW;
        [SerializeField] private Building building;
        [SerializeField] private List<ISlotContent> buildingManagers = new();

        private SlotButton _slotButton;
        private BuildingType _buildingType;
        private List<MenuOptionFunc> menuOptionsList = new();
        private delegate bool MenuOptionFunc();

        public Dictionary<string, bool> GetDropDownOptions()
        {
            return building ? GetBuildingMenuOptions() : GetBuildableOptions();
        }

        public bool HandleOptionClicked(int index)
        {
            if (building)
                return CallBuildingMethod(index);
            
            return CallManagerMethod(index);
        }

        public void Setup(ISlotContentSource source, BuildingType buildingType, SlotButton slotButton)
        {
            menuOptionsList.Add(CallUpgrade);
            researchTreeNEW = source;
            _buildingType = buildingType;
            _slotButton = slotButton;
        }

        private Dictionary<string, bool> GetBuildableOptions()
        {
            var results = new Dictionary<string, bool>();

            switch (_buildingType)
            {
                case BuildingType.Small: buildingManagers = researchTreeNEW.GetSlotItems(typeof(SmallBuildingManager));
                    break;
                case BuildingType.Core: buildingManagers = researchTreeNEW.GetSlotItems(typeof(CoreBuildingManager));
                    break;
                case BuildingType.Large: buildingManagers = researchTreeNEW.GetSlotItems(typeof(LargeBuildingManager));
                    break;
                default: throw new Exception($"Unsupported building type: {_buildingType}");
            }

            foreach (var option in buildingManagers)
            {
                results.Add(option.GetName(), option.IsAvailable());
            }
            
            return results;
        }

        private Dictionary<string, bool> GetBuildingMenuOptions()
        {
            var options = new Dictionary<string, bool>();
            
            options.Add("Level " + building.Level, building.IsUpgradeable);
            
            return options;
        }

        //todo: safe bezüglich index?

        private bool CallManagerMethod(int index)
        {
            building = ((BuildingManager)buildingManagers[index]).TryCreateBuilding();
            
            if (building == null)
            {
                Debug.Log("Building could not be built");
                return false;
            }
            
            building.OnUpgradeProgress += _slotButton.SetFillAmount;
            
            OptionSet?.Invoke(building.name);
            return true;
        }

        private bool CallBuildingMethod(int index)
        {
            if(index == -1)
                Debug.Log("index is -1");
            
            menuOptionsList[index].Invoke();
            return false;
        }

        private bool CallUpgrade()
        {
            building.StartUpgrade();
            return false;
        }
    }
}