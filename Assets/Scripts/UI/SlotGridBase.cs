using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class SlotGridBase : SerializedMonoBehaviour
{
    [SerializeField] protected GameObject _slotPrefab;
    [FormerlySerializedAs("dropdownMenu")] [SerializeField] protected DropDownMenu dropDownMenu;

    protected abstract void Setup();
    // protected abstract void CallDropDown(Vector3 position, IMethodProvider building);
}