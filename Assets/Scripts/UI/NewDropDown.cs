using Entities.Buildings;
using UnityEngine;

namespace UI
{
    public class NewDropDown : MonoBehaviour
    {
        [SerializeField] private DropDownContent content;
        [SerializeField] private Slot slot;
        [SerializeField] private Blocker blocker;
        
        private void Awake()
        {
            content.OnButtonClicked += HandleOptionClicked;
            blocker.gameObject.SetActive(false);
            HideDropDownContent();
        }

        public void ShowDropDown(int index, Vector3 position, Slot slot)
        {
            blocker.gameObject.SetActive(true);
            gameObject.SetActive(true);
            transform.position = position;
            this.slot = slot;
            content.Show(slot.GetDropDownOptions());
        }
        
        public void HideDropDownContent()
        {
            gameObject.SetActive(false);
            content.Hide();
        }

        private void HandleOptionClicked(int index)
        {
            if(slot.HandleOptionClicked(index))
            {
                HideDropDownContent();
                blocker.gameObject.SetActive(false);
            }
        }
    }
}