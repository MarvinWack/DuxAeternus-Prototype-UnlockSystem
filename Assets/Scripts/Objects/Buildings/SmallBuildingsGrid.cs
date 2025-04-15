using Entities.Buildings;
using TMPro;
using UI;
using UnityEngine;

namespace Objects.Buildings
{
    public class SmallBuildingsGrid : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private GameObject buildingSlotPrefab;
        [SerializeField] private NewDropDown dropDown;
        [SerializeField] private ResearchTree researchTree;
        

        private void Awake()
        {
            for(int i = 0; i < gameSettings.NumberOfSmallBuildings; i++)
            {
                var slotButton = Instantiate(buildingSlotPrefab, transform);
                slotButton.GetComponentInChildren<Slot>().Setup(researchTree);
                slotButton.GetComponent<SlotButton>().Setup(i, dropDown, slotButton.GetComponentInChildren<Slot>());
                slotButton.name = $"BuildingDropDown_{i}";
                SetLabelText("Empty Slot " + i, slotButton);
            }
        }
        
        private void SetLabelText(string message, GameObject slot)
        {
            var textComponent = slot.GetComponentInChildren(typeof(TMP_Text), false);
            if(textComponent is TMP_Text tmpText)
                tmpText.text = message;
        }
    }
}