using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Slot
{
    public class ExtendedText : MonoBehaviour, IDisplay, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TMP_Text text;
        
        public void SetText(string newText)
        {
            text.text = newText;
        }
        
        public event Action<Vector3> OnHoverStart;
        public event Action OnHoverEnd;
        

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHoverStart?.Invoke(transform.position);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnHoverEnd?.Invoke();
        }
    }
}