using System;
using Entities.Buildings;
using UI;
using UnityEngine;

namespace Objects.Buildings
{
    public class SmallBuildingsGrid : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private GameObject buildingDropDownPrefab;
        [SerializeField] private ResearchTree researchTree;

        private void Awake()
        {
            for(int i = 0; i < gameSettings.NumberOfSmallBuildings; i++)
            {
                var buildingDropDown = Instantiate(buildingDropDownPrefab, transform);
                buildingDropDown.GetComponentInChildren<Slot>().Setup(researchTree);
                buildingDropDown.name = $"BuildingDropDown_{i}";
            }
        }
    }
}