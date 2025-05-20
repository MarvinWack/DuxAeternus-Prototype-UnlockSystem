using System.Collections.Generic;
using Entities.Buildings;
using UI.Slot;
using UnityEngine;

namespace UI
{
    public class DropDownMenu : PopUp
    {
        [SerializeField] private DropDownContent content;

        private IDropdownCaller caller;
        
        protected override void Setup()
        {
            base.Setup();
            content.OnButtonClicked += HandleOptionClicked;
        }
        
        public void Show(Vector3 position, List<ExtendedButton> options)
        {
            Hide();
            base.Show(position);

            content.Show(options);
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