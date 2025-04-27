using System;
using Entities.Buildings;
using UI.Slot;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Calls DropDownMenu and/or InfoWindow.
    /// </summary>
    public class SlotButton : MonoBehaviour
    {
        [SerializeField] private ExtendedButton button;
        
        private event Action<Vector3, IDropdownCaller> OnClick;
        private event Action<Vector3, IPopUpCaller> OnHoverStart;
        private event Action OnHoverEnd;
        private event Action OnClickNoParams;
        
        private IDropdownCaller _dropdownCaller;
        private InfoWindowCaller _infoWindowCaller;

        public SlotButton Setup(DropDownMenu dropDownMenu, IDropdownCaller slot, int i, string slotName = null)
        {
            OnClick += dropDownMenu.Show;

            slot.OptionSet += button.DisplayMessage;
            _dropdownCaller = slot;
            
            if(slot is IMessageForwarder forwarder)
                forwarder.OnMessageForwarded += button.DisplayMessage;
            
            SetupEvents(slot);
            
            button.DisplayMessage("No text chosen " + i);
            
            if(slotName != null)
            {
                button.DisplayMessage(slotName);
            }
            
            return this;
        }

        public SlotButton Setup(InfoWindow infoWindow, IDirectCaller slot, string slotName = null)
        {
            OnClickNoParams += slot.HandleSlotClicked;
            
            slot.OnLabelChanged += button.DisplayMessage;
            
            if(slot is IMessageForwarder forwarder)
                forwarder.OnMessageForwarded += button.DisplayMessage;

            if (slot is not InfoWindowCaller infoWindowCaller) return this;
            
            _infoWindowCaller = infoWindowCaller;
            
            OnHoverStart += infoWindow.Show;
            OnHoverEnd += infoWindow.Hide;
            
            
            //todo: entfernen?
            if(slotName != null)
            {
                button.DisplayMessage(slotName);
            }
            
            SetupEvents(infoWindowCaller);
            
            return this;
        }
        
        public void SetFillAmount(float fillAmount)
        {
            button.SetFillAmount(fillAmount);
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
            if (slot is not IProgressVisualiser progressVisualiser) return;
            
            progressVisualiser.OnUpgradeProgress += button.SetFillAmount;
        }

        private void HandleClick(Vector3 position, bool callDropDown, int index)
        {
            OnClick?.Invoke(position, _dropdownCaller);
            OnClickNoParams?.Invoke();
        }

        private void HandleHoverStart(Vector3 position, bool callInfoWindow)
        {
            if (_infoWindowCaller is not null)
            {
                OnHoverStart?.Invoke(position, _infoWindowCaller);
            }
        }

        private void HandleHoverEnd()
        {
            OnHoverEnd?.Invoke();
        }
    }
}