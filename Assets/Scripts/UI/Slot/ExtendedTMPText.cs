// using System;
// using TMPro;
// using UnityEngine;
// using UnityEngine.EventSystems;
//
// namespace UI.Slot
// {
//     public class ExtendedTMPText : TextMeshProUGUI, IExtendedUIElement, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
//     {
//         public event Action<Vector3> OnClick;
//         public event Action<Vector3> OnHoverStart;
//         public event Action OnHoverEnd;
//         
//         public void OnPointerEnter(PointerEventData eventData)
//         {
//             Debug.Log("extended pointer enter");
//         }
//
//         public void OnPointerExit(PointerEventData eventData)
//         {
//             Debug.Log("extenden pointer exit");
//         }
//
//         public void OnPointerClick(PointerEventData eventData)
//         {
//             Debug.Log("extenden pointer click!!");
//         }
//     }
// }