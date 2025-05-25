using UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Blocker : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private DropDownMenu dropdownMenu;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        dropdownMenu.Hide();
        gameObject.SetActive(false);
    }

    public void SetDropDown(DropDownMenu dropdown)
    {
        dropdownMenu = dropdown;
    }
}
