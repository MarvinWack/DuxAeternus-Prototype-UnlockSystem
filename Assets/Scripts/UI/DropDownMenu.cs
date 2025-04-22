using Entities.Buildings;
using UnityEngine;
using UnityEngine.Serialization;

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
        
        public override void Show(int index, Vector3 position, PopUpCaller caller)
        {
            base.Show(index, position, caller);
            
            if(caller is IDropdownCaller dropdownCaller)
            {
                slot = dropdownCaller;
                content.Show(dropdownCaller.GetDropDownOptions());
            }
            
            else
                Debug.LogError("Caller is not a dropdown caller");
        }
        
        public override void Hide()
        {
            base.Hide();
            content.Hide();
        }

        private void HandleOptionClicked(int index)
        {
            if(slot.HandleOptionClicked(index))
            {
                Hide();
                blocker.gameObject.SetActive(false);
            }
        }
    }
}