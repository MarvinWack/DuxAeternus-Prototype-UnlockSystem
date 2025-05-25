using Sirenix.OdinInspector;
using UI;
using UnityEngine;

public abstract class SlotGridBase : SerializedMonoBehaviour
{
    [SerializeField] protected GameObject _slotPrefab;
    [SerializeField] protected DropDownMenu dropDownMenu;

    protected abstract void Setup();
}