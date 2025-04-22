using System;
using System.Collections.Generic;
using Objects;
using Production.Items;
using Production.Storage;
using UI;
using UnityEngine;
using UnityEngine.Assertions;

public class Building : MonoBehaviour, ICustomer, IProgressSender, IUpgradable
{
    [InspectorButton("StartUpgrade")]
    public bool LevelUpButton;
    
    public event Action<int> OnUpgrade;
    public event Action<float> OnUpgradeProgress;

    public Action<ProductBlueprint, int> OnProduction;

    public event Action<Dictionary<ProductBlueprint, int>, PurchaseArgs> OnTryPurchase;

    public int Level => _level;

    public int _level;

    public bool IsUpgradeable => _level < _blueprint.MaxLevel;

    private BuildingBlueprint _blueprint;

    private bool _isUpgrading;

    private ushort _elapsedUpgradingTime;

    public bool _isProducing;

    public int _currentlyAccumulatedProduction;

    public void HandleUpgradeTick()
    {
        if (!_isUpgrading) return;
            
        _elapsedUpgradingTime++;
        OnUpgradeProgress?.Invoke((float)_elapsedUpgradingTime / _blueprint.UpgradeTime);
        
        if(_elapsedUpgradingTime >= _blueprint.UpgradeTime)
            Upgrade();
    }
    
    public bool StartUpgrade()
    {
        if (!IsUpgradeable)
        {
            Debug.Log("Building is not upgradeable");
            return false;
        }
        
        if (_isUpgrading)
        {
            Debug.Log("Is already upgrading");
            return false;
        }
        
        var PurchaseArgs = new PurchaseArgs();
        OnTryPurchase?.Invoke(_blueprint.Cost.Amount[_level], PurchaseArgs);

        if (!PurchaseArgs.IsValid)
        {
            Debug.Log("Not enough resources to upgrade");
            return false;
        }
        
        _isUpgrading = true;
        return true;
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
        
        OnProduction?.Invoke(_blueprint.ProducedProduct, CalculateProductionAmount());
    }

    private int CalculateProductionAmount() //todo: in storage auslagern, sobald Blueprint als Key dort Sinn macht
    {
        int buildingProduction = (int)_blueprint.ProductionAmount;
        
        int requiredProduction = (int)_blueprint.ProducedProduct.RequiredProductionAmount;
        
        _currentlyAccumulatedProduction += buildingProduction * _level;

        int completeUnits = _currentlyAccumulatedProduction / requiredProduction;
        
        _currentlyAccumulatedProduction -= completeUnits * requiredProduction;

        return completeUnits;
    }
}
