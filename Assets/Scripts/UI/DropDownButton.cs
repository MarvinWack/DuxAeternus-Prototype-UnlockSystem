using System;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropDownButton : Button
{
    [SerializeField] private int index;
    // [SerializeField] private NewDropDown dropDown;
    public Action<int> OnClick;
    public void Setup(int index)
    {
        this.index = index;
    }
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        OnClick?.Invoke(index);
    }
}
