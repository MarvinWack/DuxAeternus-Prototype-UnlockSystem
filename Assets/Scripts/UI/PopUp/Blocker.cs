using UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Blocker : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private DropDownMenu dropdown;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        dropdown.Hide();
        gameObject.SetActive(false);
    }
}
