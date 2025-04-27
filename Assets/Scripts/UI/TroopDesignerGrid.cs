using System;
using System.Collections.Generic;
using Objects.TroopTypes;
using Production.Items;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TroopDesignerGrid : GridBase
    {
        public int NumberOfItems;
        
        [SerializeField] private SlotFactory slotFactory;
        
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
            slotFactory.CreateGrid(this);
            
            selectedItems = new ItemBlueprint[NumberOfItems];
            
            
            // for (int i = 0; i < slotNames.Count; i++)
            // {
            //     // InstantiateButton(i, slotNames[i]);
            //     slotFactory.CreateSlot(this, i, slotNames[i]);
            // }
            
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


        // private void InstantiateButton(int i, string name)
        // {
        //     var instance = Instantiate(slotButtonPrefab, transform);
        //     var troopCreatorSlot = instance.transform.GetChild(0).AddComponent<TroopCreatorSlot>();
        //     troopCreatorSlot.Setup(islotContentSource, _troopTypeCreator, i);
        //     troopCreatorSlot.ItemSelected += HandleItemSelected;
        //     instance.GetComponent<SlotButton>().Setup(dropDownMenu, instance.GetComponentInChildren<TroopCreatorSlot>(), i);
        //     SetLabelText(name, instance);
        // }

        internal void HandleItemSelected(ItemBlueprint item, int index)
        {
            selectedItems[index] = item;

            if (_allSlotsSelected)
                createTroopTypeButton.interactable = true;
        }
    }
}