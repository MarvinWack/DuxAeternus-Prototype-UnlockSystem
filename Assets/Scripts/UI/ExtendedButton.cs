using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Slot
{
    /// <summary>
    /// Button with Hover-events and methods for setting text and fill amount.
    /// </summary>
    
    //todo: als baseclass für dd-button
    [RequireComponent(typeof(Button))]
    public class ExtendedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
        IPointerClickHandler
    {
        public event Action OnClickNoParamsTest;
        public event Action<Vector3> OnClick;
        public event Action<Vector3, bool, int> OnClickIndex;
        public event Action<Vector3, bool> OnHoverStart;
        public event Action OnHoverEnd;

        public bool CallDropDown => callDropDown;
        public bool CallInfoWindow => callInfoWindow;
        
        [Header("Settings")] 
        [SerializeField] private bool callDropDown;
        [SerializeField] private bool callInfoWindow;

        [Header("References")]
        [SerializeField] private Button button;
        [SerializeField] private Image buttonImage;
        [SerializeField] private TMP_Text buttonLabel;
        
        private int _index;
        public bool _isInteractable = true;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isInteractable) return;
            
            OnClickIndex?.Invoke(transform.position, callDropDown, _index);
            OnClick?.Invoke(eventData.position);
            OnClickNoParamsTest?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isInteractable) return;
            
            OnHoverStart?.Invoke(transform.position, callInfoWindow);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isInteractable) return;
            
            OnHoverEnd?.Invoke();
        }

        public void SetText(string newText)
        {
            buttonLabel.SetText(newText);
        }

        public void SetFillAmount(float fillAmount)
        {
            buttonImage.fillAmount = fillAmount;
        }
        
        public void SetIndex(int index)
        {
            _index = index;
        }
        
        public void SetInteractable(bool interactable)
        {
            _isInteractable = interactable;
            button.interactable = interactable;
        }
    }
}