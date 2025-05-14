using System;
using System.Collections.Generic;
using TMPro;
using UI.Slot;
using UnityEngine;
using UnityEngine.UI;

public class DropDownContent : MonoBehaviour
{
    public event Action OnButtonClicked;
    
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private List<ExtendedButton> buttons;
    [SerializeField] private GameObject buttonPrefab;

    private Transform methodTransform;

    private void Awake()
    {
        //todo: beim beenden des Spiels springt die Pos von Content auf ~117
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0f);
    }

    public void Show(List<ExtendedButton> options)
    {
        gameObject.SetActive(true);
        
        // foreach (var button in buttons)
        // {
        //     Destroy(button.gameObject);
        // }
        
        buttons.Clear();

        // int index = 0;
        
        foreach (var option in options)
        {
            option.transform.SetParent(transform);
            option.OnClickNoParamsTest += OnButtonClicked;
        }
        grid.CalculateLayoutInputVertical();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void InstantiateButton(string label, bool enable, int index)
    {
        // var instance = Instantiate(buttonPrefab, transform);
        // instance.name = label;
        // instance.GetComponentInChildren<TextMeshProUGUI>().text = label;
        //
        // var button = instance.GetComponent<DropDownButton>();
        // button.Setup(index);
        // // buttons.Add(button);
        // // button.OnClick += HandleButtonClicked;
        // button.interactable = enable;
    }

    private void HandleButtonClicked(int index)
    {
        // OnButtonClicked?.Invoke(index);
    }
}
