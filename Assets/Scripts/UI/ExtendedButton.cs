using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Slot
{
    public class ExtendedButton : MonoBehaviour, IDisplay, IPointerEnterHandler, IPointerExitHandler,
        IPointerClickHandler
    {
        public event Action<Vector3, bool, int> OnClick;
        public event Action<Vector3, bool> OnHoverStart;
        public event Action OnHoverEnd;

        [Header("Settings")] 
        [SerializeField] private bool callDropDown;

        [SerializeField] private bool callInfoWindow;

        [Header("References")] 
        [SerializeField] private Image buttonImage;

        [SerializeField] private Button button;
        [SerializeField] private TMP_Text buttonLabel;
        
        private int _index;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(transform.position, callDropDown, _index);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHoverStart?.Invoke(transform.position, callInfoWindow);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnHoverEnd?.Invoke();
        }

        public void SetText(string newText)
        {
            buttonLabel.text = newText;
        }

        public void SetFillAmount(float fillAmount)
        {
            Debug.Log("setFillAmount");
            buttonImage.fillAmount = fillAmount;
        }
    }
}