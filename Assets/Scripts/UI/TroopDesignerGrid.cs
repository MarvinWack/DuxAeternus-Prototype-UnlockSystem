using System;
using System.Collections.Generic;
using Objects.TroopTypes;
using Production.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TroopDesignerGrid : GridBase
    {
        [SerializeField] private List<String> slotNames;
        [SerializeField] private ItemBlueprint[] selectedItems;
        [SerializeField] private TroopTypeCreator _troopTypeCreator;
        [SerializeField] private Button createTroopTypeButton;
        [SerializeField] private TMP_InputField nameInputField;

        private string _troopTypeName;

        private bool _allSlotsSelected => Array.TrueForAll(selectedItems, item => item != null) &&
                                          _troopTypeName != null;
        
        private void CreateTroopType()
        {
            _troopTypeCreator.CreateTroopType(selectedItems[0], selectedItems[1], _troopTypeName);
        }
        protected override void SetupButtonGrid()
        {
            slotNames = _troopTypeCreator.GetItemSlots();
            selectedItems = new ItemBlueprint[slotNames.Count];
            
            for (int i = 0; i < slotNames.Count; i++)
            {
                InstantiateButton(i, slotNames[i]);
            }
            
            createTroopTypeButton.interactable = false;
            createTroopTypeButton.onClick.AddListener(CreateTroopType);
            nameInputField.onValueChanged.AddListener(HandleNameInput);
        }

        private void HandleNameInput(string input)
        {
            _troopTypeName = input;
            
            if(_allSlotsSelected)
                createTroopTypeButton.interactable = true;
        }


        private void InstantiateButton(int i, string name)
        {
            var slotButton = Instantiate(slotButtonPrefab, transform);
            slotButton.GetComponentInChildren<TroopCreatorSlot>().Setup(researchTree, _troopTypeCreator, i);
            slotButton.GetComponentInChildren<TroopCreatorSlot>().ItemSelected += HandleItemSelected;
            slotButton.GetComponent<SlotButton>().Setup(i, dropDownMenu, slotButton.GetComponentInChildren<TroopCreatorSlot>());
            SetLabelText(name, slotButton);
        }

        private void HandleItemSelected(ItemBlueprint item, int index)
        {
            selectedItems[index] = item;

            if (_allSlotsSelected)
                createTroopTypeButton.interactable = true;
        }
    }
}