using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropDownContent : MonoBehaviour
{
    private delegate void ButtonClickHandler();
    public event Action<int> OnButtonClicked;
    
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private Vector2 cellSize = new();
    private byte numberOfButtons = 1;
    [SerializeField] private List<DropDownButton> buttons;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private List<ButtonClickHandler> menuOptions = new();
    [SerializeField] private List<int> disabledOptions = new();
    private void Awake()
    {
        // grid.cellSize = new Vector2(cellSize.x, cellSize.y);
        
        menuOptions.Add(UpgradeDebug);
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

    private void UpgradeDebug()
    {
        Debug.Log("Dropdown: Upgrade Building");
    }

    public void Show(Dictionary<string, bool> options)
    {
        gameObject.SetActive(true);
        
        numberOfButtons = (byte)options.Count;
        
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
}
