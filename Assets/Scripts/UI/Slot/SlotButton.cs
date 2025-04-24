using System;
using Entities.Buildings;
using Sirenix.OdinInspector;
using UI.Slot;
using UnityEngine;

namespace UI
{
    public class SlotButton : SerializedMonoBehaviour
    {
        [SerializeField] private ExtendedButton button;
        
        private Action<int, Vector3, IDropdownCaller> OnClick;
        private event Action<int, Vector3, IPopUpCaller> OnHoverStart;
        private event Action OnHoverEnd;
        private Action OnClickNoParams;
        
        private IDropdownCaller _dropdownCaller;
        private InfoWindowCaller _infoWindowCaller;
        private int _index;

        private void Setup()
        {
            // button.OnClick += HandleButtonClick;
        }

        private void HandleButtonClick()
        {
            // OnButtonClicked?.Invoke();
        }

        public void Setup(int index, DropDownMenu dropDownMenu, IDropdownCaller slot)
        {
            _index = index;
            OnClick += (index1, position, caller) => dropDownMenu.Show(position, caller);

            slot.OptionSet += SetSlotName;
            _dropdownCaller = slot;
            
            SetupProgressVisualiser(slot);
        }

        public void Setup(int index, InfoWindow infoWindow, IDirectCaller slot)
        {
            _index = index;
            OnClickNoParams += slot.HandleSlotClicked;
            
            slot.OnLabelChanged += SetSlotName;
            
            if(slot is InfoWindowCaller infoWindowCaller)
            {
                _infoWindowCaller = infoWindowCaller;
                OnHoverStart += (index1, position, caller) => infoWindow.Show(position, caller);
                OnHoverEnd += infoWindow.Hide;
            }
            else
            {
                Debug.Log("Slot is not an InfoWindow caller");
            }
            
            SetupProgressVisualiser(slot);
        }

        private void SetupProgressVisualiser<T>(T slot)
        {
            Debug.Log("SetupProgressVisualiser");
            if (slot is not IProgressVisualiser progressVisualiser) return;
            Debug.Log(progressVisualiser.GetType().Name);
            progressVisualiser.OnProgress += SetFillAmount;
        }

        private void SetFillAmount(float fillAmount)
        {
            Debug.Log("setFillAmount");
            button.SetFillAmount(fillAmount);
        }

        private void SetSlotName(string slotName)
        {
            button.SetText(slotName);
        }
    }
}