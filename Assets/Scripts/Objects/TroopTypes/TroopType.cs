using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Units;
using Objects;
using Production.Items;
using UI;
using UnityEngine;

public class TroopType : MonoBehaviour, ISlotContent, IUpgradable, IMessageForwarder
{
    [InspectorButton("CreateUnitDebug")] 
    public bool _CreateUnit;

    [InspectorButton("RecruitDebug")] 
    public bool _Recruit;

    public event Action<string> OnMessageForwarded;
    public event Action<int> OnUpgrade;
    public event Action<float> OnUpgradeProgress;

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
        
        CreateUnitDebug();
        RecruitDebug();
    }

    private void CreateUnitDebug()
    {
        var unit = Instantiate(unitPrefab, transform);
        unit.name = name + " unit";
        unit.Setup(this);
        units.Add(unit);
        unit.OnMessageSent += OnMessageForwarded;
    }

    //todo: !!!
    private void RecruitDebug()
    {
        units[0].AddAmount(AmountToRecruit);
        OnUpgrade?.Invoke(units[0].Amount);
    }

    //todo: !!!
    public void SubstractLossesFromFirstUnit(int amount)
    {
        units[0].RemoveAmount(amount);
        OnUpgrade?.Invoke(units[0].Amount);
    }

    public bool CheckIfUnitsAvailableToFight()
    {
        return units.All(unit => unit.Amount > 0);
    }

    public bool CallSlotAction()
    {
        RecruitDebug();
        
        return true;
    }

    public bool Recruit(int amount)
    {
        units[0].AddAmount(amount);
        return true;
    }

    public string GetName()
    {
        return gameObject.name;
    }

    public bool IsAvailable()
    {
        return CheckIfUnitsAvailableToFight(); //&& CheckIfUnitsAvailableToRecruit();
    }
    
    public bool StartUpgrade()
    {
        throw new NotImplementedException();
    }
}
