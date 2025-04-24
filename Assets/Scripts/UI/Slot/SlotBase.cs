using System;
using System.Collections.Generic;
using Entities.Buildings;
using Objects;
using Sirenix.OdinInspector;
using UI.Slot;
using UnityEngine;

namespace UI
{
    public abstract class SlotBase : SerializedMonoBehaviour, IDropdownCaller, InfoWindowCaller
    {
        // public event Action<int> OnActionCalled;
        public event IDropdownCaller.OptionSetHandler OptionSet;

        // [SerializeField] private DropDownMenu dropDownMenu;
        // [SerializeField] private InfoWindow infoWindow;
        // [SerializeField] private List<ExtendedButton> buttons;
        [SerializeField] private ISlotContentSource source;

        // public void Setup()
        // {
            // foreach (var button in buttons)
            // {
            //     button.OnClick += HandleButtonClicked;
            //     button.OnHoverStart += HandleHoverStart;
            //     button.OnHoverEnd += HandleHoverEnd;
            // }
        // }

        // private void HandleHoverEnd()
        // {
        //     infoWindow.Hide();
        // }
        //
        // private void HandleHoverStart(Vector3 position, bool callInfoWindow)
        // {
        //     if(callInfoWindow)
        //         infoWindow.Show(position, this);
        // }
        //
        // private void HandleButtonClicked(Vector3 position, bool callDropDown, int index)
        // {
        //     if(callDropDown)
        //         dropDownMenu.Show(position, this);
        //     
        //     // else
        //     //     OnActionCalled?.Invoke(index);
        //         
        // }

        public abstract Dictionary<string, bool> GetDropDownOptions();

        public abstract bool HandleOptionClicked(int index);

        public abstract string GetInfo();
        
    }
}