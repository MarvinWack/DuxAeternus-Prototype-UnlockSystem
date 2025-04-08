using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Entities.Buildings
{
    [Serializable]
    public class Slot : MonoBehaviour
    {
        [SerializeField] private ResearchTree researchTree;
        [SerializeField] private Building building;
        // [SerializeField] private DropDownMenu menu;

        [Header("Building menu options")]
        
        [SerializeField] private List<string> menuOptions;

        public Dictionary<string, bool> GetDropDownOptions()
        {
            return building ? GetBuildingMenuOptions() : GetBuildableOptions();
        }

        private Dictionary<string, bool> GetBuildableOptions()
        {
            var results = new Dictionary<string, bool>();
            var options = researchTree.GetBuildingsWithAvailabilityState();

            foreach (var option in options)
            {
                results.Add(option.Key.Name, option.Value);
            }
            
            return results;
        }

        private Dictionary<string, bool> GetBuildingMenuOptions()
        {
            var options = new Dictionary<string, bool>();
            
            //wo definieren?

            for (int i = 0; i < options.Count; i++)
            {
                options.Add(menuOptions[i], true);
            }
            
            return options;
        }

        public void HandleOptionClicked(int index)
        {
            if (building)
                CallBuildingMethod(index);

            else
                CallManagerMethod(index);
        }

        private void CallManagerMethod(int index)
        {
            //create and set building
        }

        private void CallBuildingMethod(int index)
        {
            //level up
        }

        public void Setup(ResearchTree tree)
        {
            researchTree = tree;
        }
    }
}