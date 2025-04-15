using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Buildings
{
    [Serializable]
    public class Slot : MonoBehaviour
    {
        public Action<string> BuildingSet;
        [SerializeField] private ResearchTree researchTree;
        [SerializeField] private Building building;
        [SerializeField] private List<BuildingManager> buildingManagers = new();

        [Header("Building menu options")]
        
        private List<MenuOptionFunc> menuOptionsList = new();
        private delegate bool MenuOptionFunc();
        

        private void Awake()
        {
            menuOptionsList.Add(CallUpgrade);
        }

        public Dictionary<string, bool> GetDropDownOptions()
        {
            return building ? GetBuildingMenuOptions() : GetBuildableOptions();
        }

        private Dictionary<string, bool> GetBuildableOptions()
        {
            var results = new Dictionary<string, bool>();
            buildingManagers = researchTree.GetBuildingManagersOfType(typeof(BuildingManager));

            foreach (var option in buildingManagers)
            {
                results.Add(option.name, option.IsAvailable);
            }

            // buildableOptions.Clear();
            // buildableOptions.AddRange(results);
            
            return results;
        }

        private Dictionary<string, bool> GetBuildingMenuOptions()
        {
            var options = new Dictionary<string, bool>();
            
            options.Add("Upgrade", true);
            
            return options;
        }

        public bool HandleOptionClicked(int index)
        {
            if (building)
                return CallBuildingMethod(index);
            
            return CallManagerMethod(index);
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
            
            BuildingSet?.Invoke(building.name);
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
            building.Upgrade();
            return false;
        }

        public void Setup(ResearchTree tree)
        {
            researchTree = tree;
        }
    }
}