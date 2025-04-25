using System;
using Entities.Buildings;
using UI.Slot;
using UnityEngine;

namespace UI
{
    public class SlotButton : MonoBehaviour
    {
        [SerializeField] private ExtendedButton button;
        
        private event Action<Vector3, IDropdownCaller> OnClick;
        private event Action<Vector3, IPopUpCaller> OnHoverStart;
        private event Action OnHoverEnd;
        private event Action OnClickNoParams;
        
        private IDropdownCaller _dropdownCaller;
        private InfoWindowCaller _infoWindowCaller;

        public void Setup(DropDownMenu dropDownMenu, IDropdownCaller slot)
        {
            OnClick += dropDownMenu.Show;

            slot.OptionSet += button.SetText;
            _dropdownCaller = slot;
            
            SetupEvents(slot);
        }

        public void Setup(InfoWindow infoWindow, IDirectCaller slot)
        {
            OnClickNoParams += slot.HandleSlotClicked;
            
            slot.OnLabelChanged += button.SetText;

            if (slot is not InfoWindowCaller infoWindowCaller) return;
            
            _infoWindowCaller = infoWindowCaller;
            
            OnHoverStart += infoWindow.Show;
            OnHoverEnd += infoWindow.Hide;
            
            SetupEvents(infoWindowCaller);
        }

        private void SetupEvents(IPopUpCaller slot)
        {
            SetupProgressVisualiser(slot);

            button.OnClick += HandleClick;
            button.OnHoverStart += HandleHoverStart;
            button.OnHoverEnd += HandleHoverEnd;
        }

        private void SetupProgressVisualiser(IPopUpCaller slot)
        {
            Debug.Log("SetupProgressVisualiser");
            if (slot is not IProgressVisualiser progressVisualiser) return;
            Debug.Log(progressVisualiser.GetType().Name);
            progressVisualiser.OnUpgradeProgress += button.SetFillAmount;
        }

        private void HandleClick(Vector3 position, bool callDropDown, int index)
        {
            if(callDropDown)
                OnClick?.Invoke(position, _dropdownCaller);
            
            else
                OnClickNoParams?.Invoke();
        }

        private void HandleHoverStart(Vector3 position, bool callInfoWindow)
        {
            if (callInfoWindow)
            {
                OnHoverStart?.Invoke(position, _infoWindowCaller);
            }
        }

        private void HandleHoverEnd()
        {
            //todo: check ob infoWindow überhaupt aufgerufen wurde nötig?
            OnHoverEnd?.Invoke();
        }
    }
}