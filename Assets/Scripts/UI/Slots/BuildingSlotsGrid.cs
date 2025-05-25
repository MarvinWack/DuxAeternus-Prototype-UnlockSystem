using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using UI.MethodBlueprints;
using UI.Slot;
using UnityEngine;

public class BuildingSlotGrid : SlotGridBase
{
    [OdinSerialize] private Type _buildingType;
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private CreateBuildingMethod createBuildingMethod;
    [SerializeField] private UpgradeMethod upgradeMethod;
    [SerializeField] private ExtendedButton stylePreset;
    
    private List<BuildingSlot> _slots = new();
    
    private void Start()
    {
        Setup();
    }

    protected override void Setup()
    {
        createBuildingMethod.OnBuildingCreated += SetBuilding;

        for(int i = 0; i < _gameSettings.GetNumberOfBuildings(_buildingType); i++)
        {
            var instance = Instantiate(_slotPrefab, transform);
            instance.name = $"{_buildingType} slot {i}";
            
            var slot = instance.GetComponent<BuildingSlot>();
            slot.Setup(dropDownMenu);
            slot.DropDownCalled += CallDropDown;
            _slots.Add(slot);
        }
    }

    private void CallDropDown(Vector3 position, IMethodProvider building)
    {
        foreach (var slot in _slots)
        {
            slot.AcitvateBlocker();
        }
        
        if(building == null)
            dropDownMenu.Show(position, 
                createBuildingMethod.GetAllButtons(stylePreset));
        else
        {
            List<ExtendedButton> buttons = new();
            
            foreach (var method in building.GetMethods())
            {
                buttons.Add(method.InstantiateButton(building));
            }
            
            dropDownMenu.Show(position, buttons);
        }
    }

    //Pick next free slot
    private void SetBuilding(Building building)
    {
        _slots.Find(x => x.IsBuildingSet == false).SetBuilding(building, upgradeMethod.InstantiateLabel(building, "0"), dropDownMenu);
    }
}   
