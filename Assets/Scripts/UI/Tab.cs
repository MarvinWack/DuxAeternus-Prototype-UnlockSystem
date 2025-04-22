using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Tab : MonoBehaviour
{
    public Action<Tab> OnSelected;
    
    [SerializeField] private Button button;
    
    private CanvasGroup canvasGroup;
    
    public void Setup()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        button.onClick.AddListener(HandleTabButtonClicked);
        Hide();
    }

    private void HandleTabButtonClicked()
    {
        OnSelected?.Invoke(this);
    }


    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
