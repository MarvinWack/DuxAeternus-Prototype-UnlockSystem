using System;
using Entities.Buildings;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class SlotButton : Button
    {
        [SerializeField] private Image buttonImage;
        
        private Action<int, Vector3, IDropdownCaller> OnClick;
        private event Action<int, Vector3, IPopUpCaller> OnHoverStart;
        private event Action OnHoverEnd;
        private Action OnClickNoParams;
        
        private IDropdownCaller _dropdownCaller;
        private InfoWindowCaller _infoWindowCaller;
        private int _index;

        public void Setup(int index, DropDownMenu dropDownMenu, IDropdownCaller slot)
        {
            _index = index;
            OnClick += dropDownMenu.Show;

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
                OnHoverStart += infoWindow.Show;
                OnHoverEnd += infoWindow.Hide;
            }
            else
            {
                Debug.Log("Slot is not an InfoWindow caller");
            }
            
            SetupProgressVisualiser(slot);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            OnHoverStart?.Invoke(_index, eventData.position, _infoWindowCaller);
        }
        
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            OnHoverEnd?.Invoke();
        }

        private void SetupProgressVisualiser<T>(T slot)
        {
            Debug.Log("SetupProgressVisualiser");
            if (slot is not IProgressVisualiser progressVisualiser) return;
            Debug.Log(progressVisualiser.GetType().Name);
            progressVisualiser.OnUpgradeProgress += SetFillAmount;

            buttonImage = GetComponent<Image>();
            buttonImage.type = Image.Type.Filled;
            buttonImage.fillMethod = Image.FillMethod.Horizontal;
        }

        private void SetFillAmount(float fillAmount)
        {
            Debug.Log("setFillAmount");
            buttonImage.fillAmount = fillAmount;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            OnClick?.Invoke(_index, transform.position, _dropdownCaller);
            OnClickNoParams?.Invoke();
        }

        private void SetSlotName(string buildingName)
        {
            var textComponent = GetComponentInChildren(typeof(TMP_Text), false);
            if(textComponent is TMP_Text tmpText)
                tmpText.text = buildingName;
        }
    }
}