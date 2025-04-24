using System;
using System.Collections.Generic;
using Entities.Buildings;
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

        public int Index;

        [Header("Settings")] 
        [SerializeField] protected bool callDropDown;
        [SerializeField] protected bool callInfoWindow;

        [Header("ButtonReferences")] [SerializeField]
        private Image buttonImage;

        [SerializeField] protected Button button;
        [SerializeField] protected TMP_Text buttonLabel;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(transform.position, callDropDown, Index);
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