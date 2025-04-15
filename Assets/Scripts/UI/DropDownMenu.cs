using System;
using System.Collections.Generic;
using Entities.Buildings;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class DropDownMenu : TMP_Dropdown
    {
        [SerializeField] private Slot buildingSlot;
        [SerializeField] private List<int> indexesToDisable = new();

        protected override void Awake()
        {
            base.Awake();
            buildingSlot = GetComponentInChildren<Slot>();
            
            // onValueChanged.AddListener(buildingSlot.HandleOptionClicked);
            onValueChanged.AddListener(HandleOptionClicked);
        }

        protected override void Start()
        {
            base.Start();

            SetLabelText("Empty Slot");
        }

        private void SetLabelText(string message)
        {
            var textComponent = GetComponentInChildren(typeof(TMP_Text), false);
            if(textComponent is TMP_Text tmpText)
                tmpText.text = message;
        }

        private void HandleOptionClicked(int index)
        {
            SetLabelText("test");
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            options.Clear();
            indexesToDisable.Clear();
            
            options.Add(new OptionData("none"));

            int index = 1;
            
            foreach (var option in buildingSlot.GetDropDownOptions())
            {
                options.Add(new OptionData(option.Key));
                if(option.Value == false)
                    indexesToDisable.Add(index);
                index++;
            }
            
            base.OnPointerClick(eventData);
            
            var dropDownList = GetComponentInChildren<Canvas>();

            Assert.IsTrue(dropDownList != null, $"Dropdown component doesn't exist: {dropDownList}");
            var toogles = dropDownList.GetComponentsInChildren<Toggle>(true);

            // if(!dropDownList) return;

            for(var i = 0; i < toogles.Length; i++) 
            {
                toogles[i].interactable = !indexesToDisable.Contains(i - 1);
            }

            // value = -1;
            SetLabelText("test");
        }

        private void EnableOption(int index, bool enable) 
        {
            if(enable) 
            {
                if(indexesToDisable.Contains(index)) 
                {
                    indexesToDisable.Remove(index);
                }
            }
            
            else 
            {
                if(!indexesToDisable.Contains(index)) 
                {
                    indexesToDisable.Add(index);
                }
            }

            var dropDownList = GetComponentInChildren<Canvas>();

            // If this returns null than the Dropdown was closed
            if(!dropDownList) return;
            
            var toogles = dropDownList.GetComponentsInChildren<Toggle>(true);
            toogles[index].interactable = enable;
        }
        
        public void EnableOption(string label, bool enable) 
        {
            var index = options.FindIndex(o => string.Equals(o.text, label));
        
            EnableOption(index, enable);
        }
        
    }
}