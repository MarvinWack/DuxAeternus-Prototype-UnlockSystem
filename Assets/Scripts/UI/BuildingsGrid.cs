using System;
using Entities.Buildings;
using UnityEngine;

namespace UI
{
    public class BuildingsGrid : GridBase
    {
        [SerializeField] BuildingType buildingType; //wack
        
        protected override void SetupButtonGrid()
        {
            int numberOfSlots = buildingType switch
            {
                BuildingType.Core => gameSettings.NumberOfCoreBuildings,
                BuildingType.Small => gameSettings.NumberOfSmallBuildings,
                BuildingType.Large => gameSettings.NumberOfLargeBuildings,
                _ => throw new Exception("Unknown BuildingType")
            };
            
            for(int i = 0; i < numberOfSlots; i++)
            {
                InstantiateButton(i);
            }
        }

        //(abstract) factory wegen versch. parameter wie buildingtype?
        private void InstantiateButton(int i)
        {
            var slotButton = Instantiate(slotButtonPrefab, transform);
            slotButton.GetComponentInChildren<BuildingSlot>().Setup(researchTree, buildingType);
            slotButton.GetComponent<SlotButton>().Setup(i, dropDownMenu, slotButton.GetComponentInChildren<BuildingSlot>());
            SetLabelText("Empty Slot " + i, slotButton);
        }
    }
}