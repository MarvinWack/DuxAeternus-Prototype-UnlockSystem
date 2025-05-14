using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UI;
using UI.MethodBlueprints;
using UI.Slot;
using UnityEngine;

public class BuildingSlotsGrid : SerializedMonoBehaviour
{
    [OdinSerialize] private Type _buildingType;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private DropDownMenu dropdownMenu;
    [SerializeField] private ResearchTree researchTree;
    [SerializeField] private CreateBuildingMethod createBuildingMethod;
    
    private List<BuildingSlot> _slots = new();
    
    private void Start()
    {
        for(int i = 0; i < _gameSettings.NumberOfSmallBuildings; i++)
        {
            var instance = Instantiate(_slotPrefab, transform);
            instance.name = $"{_buildingType} slot {i}";
            var slot = instance.GetComponent<BuildingSlot>();
            slot.Setup(_buildingType, dropdownMenu, researchTree);
            slot.DropDownCalled += CallDropDown;
            _slots.Add(slot);
        }
        
        createBuildingMethod.OnBuildingCreated += SetBuilding;
    }

    private void CallDropDown(Vector3 position, Building building)
    {
        if(building == null)
            dropdownMenu.Show(position, 
                createBuildingMethod.GetAllButtons());
        else
        {
            List<ExtendedButton> buttons = new();
            
            foreach (var method in building.GetMethods())
            {
                buttons.Add(method.InstantiateButton(building, method.GetName()));
            }
            
            dropdownMenu.Show(position, buttons);
        }
    }

    private void SetBuilding(Building building)
    {
        _slots.Find(x => x.IsBuildingSet == false).SetBuilding(building);
    }
}   
