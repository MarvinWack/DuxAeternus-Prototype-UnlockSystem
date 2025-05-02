using System.Collections.Generic;
using System.Threading.Tasks;
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

        private void Start()
        {
            List<GameObject> slots = new List<GameObject>();
            
            for (int i = 0; i < _gameSettings.NumberOfTroopTypes; i++)
            {
                slots.Add(Instantiate(_slotPrefab, transform)); 
            }

            int index = 0;
            foreach (var type in trooptypeCreator.TroopTypes)
            {
                slots[index].name = $"{type} slot {index}";
                slots[index].GetComponent<TroopTypeSlot>().Setup(type, type.name);
            }
            
        }
    }
}