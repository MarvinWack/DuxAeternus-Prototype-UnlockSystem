using System;
using System.Collections.Generic;
using Entities.Buildings;
using Objects;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Slot
{
    public abstract class BaseSlot : SerializedMonoBehaviour, IDropdownCaller
    {
        public event IDropdownCaller.OptionSetHandler OptionSet;
        
        [SerializeField] protected ISlotContentSource slotContentSource;
        [SerializeField] protected DropDownMenu dropdownMenu; //in factory auslagern

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

        // protected abstract void CallDropDown(Vector3 position, ISlotContentSource source);

        public abstract Dictionary<string, bool> GetDropDownOptions();

        // protected abstract void HandleCallableMethodsChanged(Dictionary<Func<bool>, bool> callableMethods);
        public abstract bool HandleOptionClicked(int index);
    }
}