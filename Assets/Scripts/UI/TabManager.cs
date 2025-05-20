using System.Linq;
using UI;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    [SerializeField] private DropDownMenu dropDownMenu;
    
    private Tab currentTab;
    
    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        var tabs = GetComponentsInChildren<Tab>();

        foreach (var tab in tabs)
        {
            tab.OnSelected += HandleTabSelection;
            tab.Setup();
        }
        
        currentTab = tabs.First();
        
        currentTab.Show();
    }

    private void HandleTabSelection(Tab tab)
    {
        currentTab.Hide();
        currentTab = tab;
        currentTab.Show();
        
        dropDownMenu.Hide();
    }
}
