using System;
using System.Collections.Generic;
using Production.Items;
using Production.Storage;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class Building : MonoBehaviour, ICustomer
{
    [InspectorButton("UpgradeDebug")]
    public bool LevelUpButton;
    
    public Action<int> OnUpgrade;
    public Action<ResourceType, int> OnProduction;
    public event Action<Dictionary<ResourceType, int>, PurchaseArgs> OnTryPurchase;

    public int _level;

    private BuildingBlueprint _blueprint;

    private bool _isUpgrading;
    private ushort _elapsedUpgradingTime;
    
    public bool _isProducing;
    public int _currentlyAccumulatedProduction;
    
    public void HandleUpgradeTick()
    {
        if (!_isUpgrading) return;
            
        _elapsedUpgradingTime++;
        
        if(_elapsedUpgradingTime >= _blueprint.UpgradeTime)
            Upgrade();
    }
    
    public void SetBlueprint(BuildingBlueprint blueprint)
    {
        _blueprint = blueprint;
        
        _isProducing = _blueprint.ProductionType switch
        {
            ProductionType.OnDemand => false,
            ProductionType.Continuous => true,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void UpgradeDebug()
    {
        Assert.IsTrue(_level < _blueprint.MaxLevel);
        
        var PurchaseArgs = new PurchaseArgs();
        OnTryPurchase?.Invoke(_blueprint.Cost.Amount[_level], PurchaseArgs);

        if (!PurchaseArgs.IsValid)
            return;
        
        _isUpgrading = true;
    }

    private void Upgrade()
    {   
        _level++;
        _isUpgrading = false;
        _elapsedUpgradingTime = 0;
        OnUpgrade?.Invoke(_level);
    }

    public void HandleProductionTick()
    {
        if(!_isProducing) return;
        
        OnProduction?.Invoke(_blueprint.ProducedResource.ResourceType, CalculateProductionAmount());
    }

    private int CalculateProductionAmount() //todo: in storage auslagern, sobald Blueprint als Key dort Sinn macht
    {
        int buildingProduction = (int)_blueprint.ProductionAmount;
        
        int requiredProduction = (int)_blueprint.ProducedResource.RequiredProductionAmount;
        
        _currentlyAccumulatedProduction += buildingProduction * _level;

        int completeUnits = _currentlyAccumulatedProduction / requiredProduction;
        
        _currentlyAccumulatedProduction -= completeUnits * requiredProduction;

        return completeUnits;
    }
}
