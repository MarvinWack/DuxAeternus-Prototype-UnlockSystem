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
        
        public event Action OnDestruction;

        public bool CallDropDown => callDropDown;
        public bool CallInfoWindow => callInfoWindow;
        
        [Header("Settings")] 
        [SerializeField] private bool callDropDown;
        [SerializeField] private bool callInfoWindow;
        [SerializeField] private bool isInteractable = true;
        [SerializeField] private bool useFadeOnInteractableChanged = false;

        [Header("References")]
        [SerializeField] private Button button;
        [SerializeField] private Image buttonImage;
        [SerializeField] private TMP_Text buttonLabel;

        private int _index;

        private void Awake()
        {
            // button.interactable = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isInteractable) return;
            
            OnClickIndex?.Invoke(transform.position, callDropDown, _index);
            OnClick?.Invoke(eventData.position);
            OnClickNoParamsTest?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isInteractable) return;
            
            OnHoverStart?.Invoke(transform.position, callInfoWindow);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isInteractable) return;
            
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
            isInteractable = interactable;
            button.interactable = interactable;
        }

        public void UseFadeOnInteractableChanged(bool useFade)
        {
            ColorBlock colorBlock = button.colors;

            if (useFade)
                colorBlock.fadeDuration = 0.15f;
            else
                colorBlock.fadeDuration = 0f;
            
            button.colors = colorBlock;
        }

        private void OnDestroy()
        {
            OnDestruction?.Invoke();
        }

        // public void ApplyStyling(ExtendedButton buttonStyleToCopy)
        // {  
        //     if (buttonStyleToCopy == null)
        //     {
        //         Debug.LogWarning("ApplyStyling: buttonStyleToCopy is null");
        //         return;
        //     }
        //     
        //     // Copy RectTransform properties
        //     RectTransform sourceRect = buttonStyleToCopy.GetComponent<RectTransform>();
        //     RectTransform targetRect = GetComponent<RectTransform>();
        //
        //     sourceRect.rect.position = sourceRect.rect.position;
        //     
        //     if (sourceRect != null && targetRect != null)
        //     {
        //         targetRect.anchorMin = sourceRect.anchorMin;
        //         targetRect.anchorMax = sourceRect.anchorMax;
        //         targetRect.pivot = sourceRect.pivot;
        //         targetRect.sizeDelta = sourceRect.sizeDelta;
        //         targetRect.anchoredPosition = sourceRect.anchoredPosition;
        //     }            
        //     var buttonColors = buttonStyleToCopy.button.colors;
        //     button.colors = buttonColors;
        //
        //     var imageColors = buttonStyleToCopy.buttonImage.color;
        //     buttonImage.color = imageColors;
        //
        //     var labelColors = buttonStyleToCopy.buttonLabel.color;
        //     buttonLabel.color = labelColors;
        // }
    }
}