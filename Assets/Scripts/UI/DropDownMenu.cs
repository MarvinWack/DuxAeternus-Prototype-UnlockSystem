using System.Collections.Generic;
using Entities.Buildings;
using UI.Slot;
using UnityEngine;

namespace UI
{
    public class DropDownMenu : PopUp
    {
        [SerializeField] private DropDownContent content;

        private IDropdownCaller slot;
        
        protected override void Setup()
        {
            base.Setup();
            content.OnButtonClicked += HandleOptionClicked;
        }
        
        public void Show(Vector3 position, List<ExtendedButton> options)
        {
            base.Show(position);
            
            // if(caller is IDropdownCaller dropdownCaller)
            // {
            //     slot = dropdownCaller;
            content.Show(options);
            // }
            
            // else
            //     Debug.LogError("Caller is not a dropdown caller");
        }
        
        public override void Hide()
        {
            base.Hide();
            content.Hide();
        }

        private void HandleOptionClicked()
        {
            Hide();
            blocker.gameObject.SetActive(false);
        }
    }
}