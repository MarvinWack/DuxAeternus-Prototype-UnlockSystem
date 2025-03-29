using System.Collections.Generic;
using Production.Items;
using UnityEngine;

public class TroopType : MonoBehaviour
{
    [InspectorButton("CreateUnit")]
    public bool _CreateUnit;
    
    [SerializeField] private ItemBlueprint weapon;
    [SerializeField] private ItemBlueprint armour;
    [SerializeField] private Unit unitPrefab;
    [SerializeField] private List<Unit> units;

    public void Setup(ItemBlueprint firstItem, ItemBlueprint secondItem, string troopTypeName)
    {
        name = troopTypeName;
        weapon = firstItem;
        armour = secondItem;
    }

    public void CreateUnit()
    {
        var unit = Instantiate(unitPrefab, transform);
        unit.Setup(this);
        units.Add(unit);
    }
}
