using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Units;
using Objects;
using Production.Items;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UI;
using UI.MethodBlueprints;
using UnityEngine;

public class TroopType : SerializedMonoBehaviour, ICallReceiver
{
    [InspectorButton("CreateUnit")] 
    public bool _CreateUnit;

    [InspectorButton("RecruitDebug")] 
    public bool _Recruit;

    [OdinSerialize] private List<RecruitMethod> methodsList;
    
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

        foreach (var method in methodsList)
        {
            method.RegisterReceiverHandler(Recruit);
        }
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
    private void Recruit(int amount)
    {
        units[0].AddAmount(amount);
    }

    public void SubstractLossesFromFirstUnit(int amount)
    {
        units[0].RemoveAmount(amount);
    }

    public bool CheckIfUnitsAvailableToFight()
    {
        return units.All(unit => unit.Amount > 0) && units.Count > 0;
    }

    public bool IsAvailable()
    {
        return CheckIfUnitsAvailableToFight(); //&& CheckIfUnitsAvailableToRecruit();
    }
}
