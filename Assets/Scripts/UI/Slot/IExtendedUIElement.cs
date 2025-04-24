using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Slot
{
    public interface IExtendedUIElement
    {
        event Action<Vector3> OnClick;
        event Action<Vector3> OnHoverStart;
        event Action OnHoverEnd;
        void OnPointerEnter(PointerEventData eventData);
        void OnPointerExit(PointerEventData eventData);
        void OnPointerClick(PointerEventData eventData);
    }
}