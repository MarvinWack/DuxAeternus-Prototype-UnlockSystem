using System.Linq;
using UI.MethodBlueprints;
using UI.Slot;
using UnityEngine;

namespace UI
{
    public class SelectItemSlot : MonoBehaviour
    {
        [SerializeField] private ExtendedButton callDropdownButton;

        private SelectItemMethod _method;
        private DropDownMenu _dropDownMenu;
        public void Setup(SelectItemMethod method, DropDownMenu dropDownMenu)
        {
            _dropDownMenu = dropDownMenu;
            
            _method = method;
            _method.OnItemSelected += callDropdownButton.SetText;

            callDropdownButton.OnClick += CallDropDown;
        }

        private void CallDropDown(Vector3 position)
        {
            _dropDownMenu.Show(position, _method.GetButtonForEachOption().Cast<ExtendedButton>().ToList());
        }
    }
}