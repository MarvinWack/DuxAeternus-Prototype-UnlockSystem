using System.Collections.Generic;
using Entities.Buildings;
using UnityEngine;

namespace UI.Slot
{
    public class CallDropDownButton : ExtendedButton, IDropdownCaller
    {
        public event IDropdownCaller.OptionSetHandler OptionSet;
        
        private DropDownMenu _dropDownMenu;
        private SlotBase _slot;
        
        public void Setup(SlotBase slot)
        {
            callDropDown = true;
            callInfoWindow = false;

            _slot = slot;

            OnClick += HandleButtonClicked;
        }
        
        private void HandleButtonClicked(Vector3 position, bool dropDown, int index)
        {
            _dropDownMenu.Show(position, this);
        }
    
        public Dictionary<string, bool> GetDropDownOptions()
        {
            return _slot.GetDropDownOptions();
        }

        public bool HandleOptionClicked(int index)
        {
            _slot.HandleOptionClicked(index);
            return true;
        }
    }
}