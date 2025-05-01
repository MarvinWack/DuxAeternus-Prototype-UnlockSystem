using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Units;
using Objects;
using Production.Items;
using UI;
using UnityEngine;

public class TroopType : MonoBehaviour
{
    [InspectorButton("CreateUnit")] 
    public bool _CreateUnit;

    [InspectorButton("RecruitDebug")] 
    public bool _Recruit;
    
    [SerializeField] private ItemBlueprint weapon;
    [SerializeField] private ItemBlueprint armour;
    [SerializeField] private Unit unitPrefab;
    [SerializeField] private List<Unit> units;

    public int TotalAmount => units.Sum(unit => unit.Amount);
    public ItemBlueprint Weapon => weapon;
    public ItemBlueprint Armour => armour;

    private int AmountToRecruit = 100;

    public void Setup(ItemBlueprint firstItem, ItemBlueprint secondItem, string troopTypeName)
    {
        name = troopTypeName;
        weapon = firstItem;
        armour = secondItem;
        
        CreateUnit();
        // RecruitDebug();
    }

    private void CreateUnit()
    {
        var unit = Instantiate(unitPrefab, transform);
        unit.name = name + " unit";
        unit.Setup();
        units.Add(unit);
    }

    //todo: !!!
    private void RecruitDebug()
    {
        units[0].AddAmount(AmountToRecruit);
    }

    //todo: !!!
    public bool Recruit(int amount)
    {
        units[0].AddAmount(amount);
        return true;
    }

    public void SubstractLossesFromFirstUnit(int amount)
    {
        units[0].RemoveAmount(amount);
    }

    public bool CheckIfUnitsAvailableToFight()
    {
        return units.All(unit => unit.Amount > 0) && units.Count > 0;
    }

    // public string GetName()
    // {
    //     return gameObject.name;
    // }

    public bool IsAvailable()
    {
        return CheckIfUnitsAvailableToFight(); //&& CheckIfUnitsAvailableToRecruit();
    }
}
