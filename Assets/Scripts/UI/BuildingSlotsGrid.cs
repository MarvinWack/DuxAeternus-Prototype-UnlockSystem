using System.Threading.Tasks;
using UI;
using UI.Slot;
using UnityEngine;

public class BuildingSlotsGrid : MonoBehaviour
{
    [SerializeField] private BuildingType _buildingType = BuildingType.Small;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private DropDownMenu dropdownMenu;
    [SerializeField] private ResearchTree researchTree;

    private async void Start()
    {
        await Task.Delay(1000);
        for(int i = 0; i < _gameSettings.NumberOfSmallBuildings; i++)
        {
            var slot = Instantiate(_slotPrefab, transform);
            slot.name = $"{_buildingType} slot {i}";
            slot.GetComponent<BuildingSlot>().Setup(_buildingType, dropdownMenu, researchTree);
        }
    }
}   
