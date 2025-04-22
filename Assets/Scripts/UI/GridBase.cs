using TMPro;
using UnityEngine;

namespace UI
{
    public abstract class GridBase : MonoBehaviour
    {
        [SerializeField] protected GameSettings gameSettings;
        [SerializeField] protected GameObject slotButtonPrefab;
        [SerializeField] protected DropDownMenu dropDownMenu;
        [SerializeField] protected InfoWindow infoWindow;
        [SerializeField] protected ResearchTree researchTree;

        private void Awake()
        {
            SetupButtonGrid();
        }

        protected abstract void SetupButtonGrid();

        protected void SetLabelText(string message, GameObject slot)
        {
            var textComponent = slot.GetComponentInChildren(typeof(TMP_Text), false);
            if(textComponent is TMP_Text tmpText)
                tmpText.text = message;
        }
    }
}