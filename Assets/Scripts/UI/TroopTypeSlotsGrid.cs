using System.Collections.Generic;
using Objects;
using Objects.TroopTypes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    public class TroopTypeSlotsGrid : SerializedMonoBehaviour
    {
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private DropDownMenu dropdownMenu;
        [SerializeField] private TroopTypeCreator trooptypeCreator;

        private List<GameObject> slots = new();
        private int _currentSlotIndex;
        private void Awake()
        {
            for (int i = 0; i < _gameSettings.NumberOfTroopTypes; i++)
            {
                slots.Add(Instantiate(_slotPrefab, transform)); 
            }
            
            trooptypeCreator.SlotContentAdded += SetupNewSlot;
        }

        private void SetupNewSlot(ISlotContent content)
        {
            if (content is not TroopType type)
            {
                Debug.LogError($"{name}: Slot content is not a troop type!");
                return;
            }
            
            slots[_currentSlotIndex].name = $"{type} slot {_currentSlotIndex}";
            slots[_currentSlotIndex].GetComponent<TroopTypeSlot>().Setup(type);
            _currentSlotIndex++;
        }
    }
}