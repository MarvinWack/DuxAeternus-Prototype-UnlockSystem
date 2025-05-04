using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Entities.Units;
using Objects;
using Production.Items;
using Production.Storage;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UI.MethodBlueprints;
using UnityEngine;

public class TroopType : SerializedMonoBehaviour, IMethodProvider, ISlotContent, ICustomer
{
    [InspectorButton("CreateUnit")] 
    public bool _CreateUnit;

    public event Action<Dictionary<ProductBlueprint, int>, PurchaseArgs> OnTryPurchase;
    public event Action<Dictionary<ProductBlueprint, int>, PurchaseArgs> CheckIfPurchaseValid;

    [OdinSerialize] private List<RecruitMethod> methodsList;
    
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
        
        CreateUnit();

        foreach (var method in methodsList)
        {
            method.RegisterMethodToCall(Recruit);
            method.RegisterMethodEnableChecker(CheckIfRecruitmentPossible);
        }
    }

    private void CreateUnit()
    {
        var unit = Instantiate(unitPrefab, transform);
        unit.name = name + " unit";
        unit.Setup();
        units.Add(unit);
    }
    
    private void Recruit(int amount)
    {
        if(TryPurchase(amount))
            //todo: !!!
            units[0].AddAmount(amount);
    }
    
    private bool CheckIfRecruitmentPossible(int amount)
    {
        var PurchaseArgs = new PurchaseArgs();
        
        CheckIfPurchaseValid?.Invoke(MultiplyDictionaryValues(Weapon.Cost.Amount[0], amount), PurchaseArgs);
        
        if (!PurchaseArgs.IsValid)
        {
            return false;
        }
        
        return true;
    }

    private bool TryPurchase(int amount)
    {
        var PurchaseArgs = new PurchaseArgs();
        
        OnTryPurchase?.Invoke(MultiplyDictionaryValues(Weapon.Cost.Amount[0], amount), PurchaseArgs);
        
        if (!PurchaseArgs.IsValid)
        {
            Debug.Log("Not enough resources to recruit");
            return false;
        }
        
        return true;
    }
    
    private Dictionary<ProductBlueprint, int> MultiplyDictionaryValues(SerializedDictionary<ProductBlueprint, int> original, int amount)
    {
        var result = new Dictionary<ProductBlueprint, int>();
        foreach (var cost in original)
        {
            result[cost.Key] = cost.Value * amount;
        }
        return result;
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
