using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropDownContent : MonoBehaviour
{
    public event Action<int> OnButtonClicked;
    
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private List<DropDownButton> buttons;
    [SerializeField] private GameObject buttonPrefab;

    private void Awake()
    {
        //todo: beim beenden des Spiels springt die Pos von Content auf ~117
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0f);
    }

    public void Show(Dictionary<string, bool> options)
    {
        gameObject.SetActive(true);
        
        foreach (var button in buttons)
        {
            Destroy(button.gameObject);
        }
        
        buttons.Clear();

        int index = 0;
        
        foreach (var option in options)
        {
            InstantiateButton(option.Key, option.Value, index);
            
            index++;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void InstantiateButton(string label, bool enable, int index)
    {
        var instance = Instantiate(buttonPrefab, transform);
        instance.name = label;
        instance.GetComponentInChildren<TextMeshProUGUI>().text = label;

        var button = instance.GetComponent<DropDownButton>();
        button.Setup(index);
        buttons.Add(button);
        button.OnClick += HandleButtonClicked;
        button.interactable = enable;
    }

    private void HandleButtonClicked(int index)
    {
        OnButtonClicked?.Invoke(index);
    }
}
