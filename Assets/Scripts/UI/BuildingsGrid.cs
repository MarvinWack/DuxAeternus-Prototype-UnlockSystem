using System;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class BuildingsGrid : GridBase
    {
        public BuildingType BuildingType => buildingType;
        [SerializeField] BuildingType buildingType; //wack
        [SerializeField] private SlotFactory slotFactory;
        
        protected override void SetupButtonGrid()
        {
            // int numberOfSlots = buildingType switch
            // {
            //     BuildingType.Core => gameSettings.NumberOfCoreBuildings,
            //     BuildingType.Small => gameSettings.NumberOfSmallBuildings,
            //     BuildingType.Large => gameSettings.NumberOfLargeBuildings,
            //     _ => throw new Exception("Unknown BuildingType")
            // };
            //
            // for(int i = 0; i < numberOfSlots; i++)
            // {
            //     // InstantiateButton(i);
            //     
            // }
            
            slotFactory.CreateGrid(this);
        }
        
        private void InstantiateButton(int i)
        {
            var instance = Instantiate(slotButtonPrefab, transform);
            var buildingSlot = instance.transform.GetChild(0).AddComponent<BuildingSlot>();
            buildingSlot.Setup(islotContentSource, buildingType, instance.GetComponent<SlotButton>().Setup(dropDownMenu, buildingSlot, i));
            // instance.GetComponent<SlotButton>().Setup(dropDownMenu, buildingSlot);
            // SetLabelText("Empty Slot " + i, instance);
        }
    }
}