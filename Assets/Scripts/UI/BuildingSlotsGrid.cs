using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UI;
using UI.Slot;
using UnityEngine;

public class BuildingSlotsGrid : SerializedMonoBehaviour
{
    [OdinSerialize] private Type _buildingType;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private DropDownMenu dropdownMenu;
    [SerializeField] private ResearchTree researchTree;

    private void Start()
    {
        for(int i = 0; i < _gameSettings.NumberOfSmallBuildings; i++)
        {
            var slot = Instantiate(_slotPrefab, transform);
            slot.name = $"{_buildingType} slot {i}";
            slot.GetComponent<BuildingSlot>().Setup(_buildingType, dropdownMenu, researchTree);
        }
    }
}   
