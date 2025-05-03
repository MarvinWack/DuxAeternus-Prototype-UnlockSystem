// MODIFIED FROM https://stackoverflow.com/questions/55297626/disable-an-options-in-a-dropdown-unity

using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Dropdown))]
[DisallowMultipleComponent]
public class DropDownController : MonoBehaviour, IPointerClickHandler {
    [Tooltip("Indexes that should be ignored. Indexes are 0 based.")]
    public List<int> indexesToDisable = new List<int>();

    private TMP_Dropdown _dropdown;

    private void Awake() {
        _dropdown = GetComponent<TMP_Dropdown>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        var dropDownList = GetComponentInChildren<Canvas>();
        if(!dropDownList) return;

        // If the dropdown was opened find the options toggles
        var toogles = dropDownList.GetComponentsInChildren<Toggle>(true);

        for(var i = 0; i < toogles.Length; i++) {
            toogles[i].interactable = !indexesToDisable.Contains(i - 1);
        }
    }

    // Anytime change a value by index
    public void EnableOption(int index, bool enable) {
        if(enable) {
            // remove index from disabled list
            if(indexesToDisable.Contains(index)) {
                indexesToDisable.Remove(index);
            }
        } else {
            // add index to disabled list
            if(!indexesToDisable.Contains(index)) {
                indexesToDisable.Add(index);
            }
        }

        var dropDownList = GetComponentInChildren<Canvas>();

        // If this returns null than the Dropdown was closed
        if(!dropDownList) return;

        // If the dropdown was opened find the options toggles
        var toogles = dropDownList.GetComponentsInChildren<Toggle>(true);
        toogles[index].interactable = enable;
    }

    // Anytime change a value by string label
    public void EnableOption(string label, bool enable) {
        var index = _dropdown.options.FindIndex(o => string.Equals(o.text, label));
        
        EnableOption(index, enable);
    }
}