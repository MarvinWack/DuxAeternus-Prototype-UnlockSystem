using System;
using System.Collections.Generic;
using Objects;
using UI;
using UnityEngine;

namespace Entities.Buildings
{
    [Serializable]
    public class BuildingSlot : MonoBehaviour, IDropdownCaller, IProgressVisualiser
    { 
        public event IDropdownCaller.OptionSetHandler OptionSet;
        public event Action<float> OnUpgradeProgress;
        
        [SerializeField] private ResearchTree researchTree;
        [SerializeField] private ISlotItemSource researchTreeNEW;
        [SerializeField] private Building building;
        [SerializeField] private List<BuildingManager> buildingManagers = new();
        [SerializeField] private List<ISlotItem> buildingManagersNEW = new();
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

        public void Setup(ResearchTree tree, BuildingType buildingType)
        {
            menuOptionsList.Add(CallUpgrade);
            researchTree = tree;
            _buildingType = buildingType;
        }

        private Dictionary<string, bool> GetBuildableOptions()
        {
            var results = new Dictionary<string, bool>();

            switch (_buildingType)
            {
                case BuildingType.Small: buildingManagers = researchTree.GetBuildingManagersOfType(typeof(SmallBuildingManager));
                    break;
                case BuildingType.Core: buildingManagers = researchTree.GetBuildingManagersOfType(typeof(CoreBuildingManager));
                    break;
                case BuildingType.Large: buildingManagers = researchTree.GetBuildingManagersOfType(typeof(LargeBuildingManager));
                    break;
                default: throw new Exception($"Unsupported building type: {_buildingType}");
            }

            foreach (var option in buildingManagers)
            {
                results.Add(option.name, option.IsAvailable);
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
            building = buildingManagers[index].TryCreateBuilding();
            
            if (building == null)
            {
                Debug.Log("Building could not be built");
                return false;
            }
            
            building.OnUpgradeProgress += OnUpgradeProgress;
            
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