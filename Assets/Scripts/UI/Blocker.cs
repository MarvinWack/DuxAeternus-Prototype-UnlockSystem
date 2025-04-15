using UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Blocker : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private NewDropDown dropdown;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        dropdown.HideDropDownContent();
        gameObject.SetActive(false);
    }
}
