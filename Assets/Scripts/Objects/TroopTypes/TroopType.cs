using System;
using System.Collections.Generic;
using System.Linq;
using Production.Items;
using UnityEngine;

public class TroopType : MonoBehaviour
{
    [InspectorButton("CreateUnitDebug")] 
    public bool _CreateUnit;

    [InspectorButton("RecruitDebug")] 
    public bool _Recruit;
    
    private int AmountToRecruit = 100;
    
    [SerializeField] private ItemBlueprint weapon;
    [SerializeField] private ItemBlueprint armour;
    [SerializeField] private Unit unitPrefab;
    [SerializeField] private List<Unit> units;

    public int TotalAmount => units.Sum(unit => unit.Amount);
    public ItemBlueprint Weapon => weapon;
    public ItemBlueprint Armour => armour;

    public void Setup(ItemBlueprint firstItem, ItemBlueprint secondItem, string troopTypeName)
    {
        name = troopTypeName;
        weapon = firstItem;
        armour = secondItem;
        
        CreateUnitDebug();
        RecruitDebug();
    }

    private void CreateUnitDebug()
    {
        var unit = Instantiate(unitPrefab, transform);
        unit.name = name + " unit";
        unit.Setup(this);
        units.Add(unit);
    }

    //todo: !!!
    private void RecruitDebug()
    {
        units[0].AddAmount(AmountToRecruit);
    }

    //todo: !!!
    public void SubstractLossesFromFirstUnit(int amount)
    {
        units[0].RemoveAmount(amount);
    }

    public bool CheckIfUnitsAvailableToFight()
    {
        return units.All(unit => unit.Amount > 0);
    }
}
