using System;
using System.Collections.Generic;
using Entities.Buildings;
using Objects;
using Sirenix.OdinInspector;
using UI.MethodBlueprints;
using UnityEngine;

namespace UI.Slot
{
    public abstract class BaseSlot : SerializedMonoBehaviour, IDropdownCaller
    {
        public event IDropdownCaller.OptionSetHandler OptionSet;
        
        [SerializeField] protected ISlotContentSource slotContentSource;
        [SerializeField] protected DropDownMenu dropdownMenu; //in factory auslagern
        
        [SerializeField] protected List<IMethod> methodList;

        protected bool _isOptionSet;
        protected bool _isOptionLocked;

        [SerializeField] protected List<ExtendedButton> buttons;


        private void Awake()
        {
            SetupDropDownButtons();
            // SetupDropDownEvents();
        }

        private void SetupDropDownEvents()
        {
            throw new NotImplementedException();
        }

        private void SetupDropDownButtons()
        {
            foreach (var button in buttons)
            {
                if (button.CallDropDown)
                {
                    button.OnClick += HandleDropDownCalled;
                }
            }
        }

        private void HandleDropDownCalled(Vector3 position)
        {
            dropdownMenu.Show(position, this);
        }
        public abstract Dictionary<string, bool> GetDropDownOptions();
        public abstract bool HandleOptionClicked(int index);
    }
}