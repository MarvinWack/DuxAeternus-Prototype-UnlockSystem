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
    
    [RequireComponent(typeof(Button))]
    public class ExtendedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
        IPointerClickHandler
    {
        public event Action OnClickNoParams;
        public event Action<Vector3> OnClick;
        public event Action<Vector3, bool> OnHoverStart;
        public event Action OnHoverEnd;
        
        public event Action OnDestruction;
        
        [Header("Settings")] 
        [SerializeField] private bool callDropDown;
        [SerializeField] private bool callInfoWindow;
        [SerializeField] private bool isInteractable = true;

        [Header("References")]
        [SerializeField] private Button button;
        [SerializeField] private Image buttonImage;
        [SerializeField] private TMP_Text buttonLabel;

        private int _index;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isInteractable) return;
            
            OnClick?.Invoke(eventData.position);
            OnClickNoParams?.Invoke();
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
    }
}