using System;
using Entities.Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class SlotButton : Button
    {
        [SerializeField] private int index;
        public Action<int, Vector3, Slot> OnClick;
        private Slot _slot;

        public void Setup(int index, NewDropDown dropDown, Slot slot)
        {
            this.index = index;
            OnClick += dropDown.ShowDropDown;
            slot.BuildingSet += SetBuildingName;
            _slot = slot;
        }

        private void SetBuildingName(string buildingName)
        {
            var textComponent = GetComponentInChildren(typeof(TMP_Text), false);
            if(textComponent is TMP_Text tmpText)
                tmpText.text = buildingName;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            OnClick?.Invoke(index, transform.position, _slot);
        }
    }
}