using System.Collections.Generic;
using Objects.TroopTypes;
using UnityEngine;

namespace UI
{
    public class TroopTypeSlotsGrid : SlotGridBase
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private TroopTypeCreator trooptypeCreator;

        private List<TroopTypeSlot> slots = new();
        private int _currentSlotIndex;
        private void Awake()
        {
            Setup();
        }

        private void SetupNewSlot(TroopType type)
        {
            slots[_currentSlotIndex].name = $"{type} slot {_currentSlotIndex}";
            slots[_currentSlotIndex].GetComponent<TroopTypeSlot>().Setup(type);
            _currentSlotIndex++;
        }

        protected override void Setup()
        {
            for (int i = 0; i < _gameSettings.NumberOfTroopTypes; i++)
            {
                slots.Add(Instantiate(_slotPrefab, transform).GetComponent<TroopTypeSlot>()); 
            }
            
            trooptypeCreator.SlotContentAdded += SetupNewSlot;
        }
    }
}