using System;
using System.Collections.Generic;
using System.Linq;
using Objects;
using Objects.TroopTypes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    public class SlotFactory : SerializedMonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private GameObject buildingSlotPrefab;
        [SerializeField] private GameObject troopTypeSlotPrefab;
        
        [Header("References")]
        [SerializeField] private DropDownMenu dropDownMenu;
        [SerializeField] private InfoWindow infoWindow;
        [SerializeField] private ResearchTree researchTree;
        [SerializeField] private TroopTypeCreator troopTypeCreator;
        
        [SerializeField] GameSettings gameSettings;
        
        [SerializeField] List<TroopTypeSlot> slots = new();
        [SerializeField] List<ISlotContent> troopTypes = new();

        public void CreateGrid(GridBase grid)
        {
            switch (grid)
            {
                case TroopDesignerGrid troopDesignerGrid: InstantiateTroopDesignerGrid(troopDesignerGrid);
                    break;
                case BuildingsGrid buildingGrid: InstantiateBuildingGrid(buildingGrid);
                    break;
                case ResearchGrid researchGrid: InstantiateResearchGrid(researchGrid);
                    break;
                case ArmyGrid armyGrid: InstantiateArmyGrid(armyGrid);
                    break;
            }
        }

        private void InstantiateArmyGrid(ArmyGrid armyGrid)
        {
            //List<TroopTypeSlot> 
                slots = new();
            
            // List<ISlotContent> 
                troopTypes = Enumerable.Repeat<ISlotContent>(null, 10).ToList();
            troopTypes.InsertRange(0,troopTypeCreator.GetSlotItems(typeof(TroopType)));

            for (int i = 0; i < gameSettings.NumberOfTroopTypes; i++)
            {
                slots.Add(InstantiateArmyGridButton(armyGrid, troopTypes[i]));
            }   
            
            troopTypeCreator.SlotContentChanged += armyGrid.HandleSlotContentChanged;
            armyGrid.SetSlots(slots);
        }

        private TroopTypeSlot InstantiateArmyGridButton(ArmyGrid armyGrid, ISlotContent troopType = null)
        {
            var instance = Instantiate(troopTypeSlotPrefab, armyGrid.transform);
            var troopTypeSlot = instance.GetComponent<TroopTypeSlot>();
            
            if (troopType != null)
            {
                troopTypeSlot.Setup(troopType);
                instance.transform.GetChild(0).GetComponent<SlotButton>().Setup(infoWindow, troopTypeSlot, troopType.GetName());
            }
            else
            {
                instance.transform.GetChild(0).GetComponent<SlotButton>().Setup(infoWindow, troopTypeSlot, "No troop type created yet");
            }
            
            return troopTypeSlot;
        }

        private void InstantiateBuildingGrid(BuildingsGrid grid)
        {
            int numberOfSlots = grid.BuildingType switch
            {
                BuildingType.Core => gameSettings.NumberOfCoreBuildings,
                BuildingType.Small => gameSettings.NumberOfSmallBuildings,
                BuildingType.Large => gameSettings.NumberOfLargeBuildings,
                _ => throw new Exception("Unknown BuildingType")
            };
            
            for(int i = 0; i < numberOfSlots; i++)
            {
                InstantiateBuildingSlot(grid, i, "Empty Buildling Slot " + i);
            }
        }

        private void InstantiateBuildingSlot(BuildingsGrid grid, int i, string slotName)
        {
            var instance = Instantiate(buildingSlotPrefab, grid.transform);
            var buildingSlot = instance.GetComponent<BuildingSlot>();
                buildingSlot.Setup(researchTree, grid.BuildingType, 
                instance.transform.GetChild(0).GetComponent<SlotButton>().
                    Setup(dropDownMenu, buildingSlot, i, slotName));
        }

        private GameObject InstantiateSlot(Transform gridTransform)
        {
            return Instantiate(slotPrefab, gridTransform);
        }

        private void InstantiateTroopDesignerGrid(TroopDesignerGrid grid)
        {
            var slotNames = troopTypeCreator.GetItemSlots();
            grid.NumberOfItems = slotNames.Count;
            
            for (int i = 0; i < slotNames.Count; i++)
            {
                InstantiateTroopDesignerButton(grid, i, slotNames[i]);
            }
        }

        private void InstantiateTroopDesignerButton(TroopDesignerGrid grid, int index, string slotName)
        {
            var instance = InstantiateSlot(grid.transform);
            var troopCreatorSlot = instance.AddComponent<TroopCreatorSlot>();
            troopCreatorSlot.Setup(researchTree, troopTypeCreator, index, 
                instance.transform.GetChild(0).GetComponent<SlotButton>().Setup(dropDownMenu, troopCreatorSlot, index, slotName));
            
            troopCreatorSlot.ItemSelected += grid.HandleItemSelected;
        }

        private void InstantiateResearchGrid(ResearchGrid grid)
        {
            var techs = researchTree.GetSlotItems(typeof(Tech)).Cast<Tech>().ToList();
            
            for (int i = 0; i < techs.Count; i++)
            {
                InstantiateButton(grid, i, techs[i]);
            }
        }

        private void InstantiateButton(ResearchGrid grid, int i, Tech tech)
        {
            var instance = InstantiateSlot(grid.transform);
            var researchSlot = instance.AddComponent<ResearchSlot>();
            researchSlot.Setup(tech, i, instance.transform.GetChild(0).GetComponent<SlotButton>().Setup(infoWindow, researchSlot, tech.name));
        }
    }
}