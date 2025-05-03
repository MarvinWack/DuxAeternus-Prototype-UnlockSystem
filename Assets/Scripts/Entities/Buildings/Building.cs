using System;
using System.Collections.Generic;
using Objects;
using Production.Items;
using Production.Storage;
using UI;
using UI.MethodBlueprints;
using UnityEngine;
using UnityEngine.Serialization;

public class Building : MonoBehaviour, ICustomer, IUpgradable, ICallableByUI, ICallReceiver
{
    [InspectorButton("StartUpgrade")]
    public bool UpgradeButton;

    public Action<ProductBlueprint, int> OnProduction;
    
    [FormerlySerializedAs("MethodBlueprint")] public UpgradeMethod upgradeMethod;
    public event Action<Dictionary<ProductBlueprint, int>, PurchaseArgs> OnTryPurchase;
    public event Action<Dictionary<ProductBlueprint, int>, PurchaseArgs> CheckIfPurchaseValid;

    public event Action<int> OnUpgrade;
    public event Action<float> OnUpgradeProgress;
    public event Action<bool> OnUpgradableStatusChanged;
    public event Action<Dictionary<Func<bool>, bool>> OnCallableMethodsChanged;
    
    public int Level => _level;
    public int _level;
    public bool IsUpgradeable => _level < _blueprint.MaxLevel;
    public bool _isProducing;
    public int _currentlyAccumulatedProduction;

    private BuildingBlueprint _blueprint;
    private bool _isUpgrading;
    private ushort _elapsedUpgradingTime;

    private Dictionary<Func<bool>, bool> _callableMethods = new();


    private void Start()
    {
        // SetupCallableMethods();
        upgradeMethod.RegisterReceiverHandler(StartUpgradeNoReturnValue);
    }

    private void SetupCallableMethods()
    {
        _callableMethods.Add(StartUpgrade, CheckIfUpgradePossible(false));
    }

    //handle tick-methoden in interfaces auslagern?
    public void HandleUpgradeTick()
    {
        if (!_isUpgrading) return;
            
        _elapsedUpgradingTime++;
        
        OnUpgradeProgress?.Invoke((float)_elapsedUpgradingTime / _blueprint.UpgradeTime);
        
        if(_elapsedUpgradingTime >= _blueprint.UpgradeTime)
            Upgrade();

        CheckIfCallableMethodsChanged();
    }

    public void HandleProductionTick()
    {
        if(!_isProducing) return;
        
        OnProduction?.Invoke(_blueprint.ProducedProduct, CalculateProductionAmount());
        
        CheckIfCallableMethodsChanged();
    }

    private void CheckIfCallableMethodsChanged()
    {
        if(_callableMethods.Count == 0) return;
        
        if (_callableMethods[StartUpgrade] != CheckIfUpgradePossible(false))
        {
            _callableMethods[StartUpgrade] = CheckIfUpgradePossible(false);
            OnCallableMethodsChanged?.Invoke(_callableMethods);
            OnUpgradableStatusChanged?.Invoke(_callableMethods[StartUpgrade]);
        }
    }

    public void StartUpgradeNoReturnValue()
    {
        StartUpgrade();
    }

    public bool StartUpgrade()
    {
        _isUpgrading = CheckIfUpgradePossible(true);

        return _isUpgrading;
    }

    private bool CheckIfUpgradePossible(bool tryPurchase)
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
        
        if(tryPurchase)
            OnTryPurchase?.Invoke(_blueprint.Cost.Amount[_level], PurchaseArgs);
        
        else
            CheckIfPurchaseValid?.Invoke(_blueprint.Cost.Amount[_level], PurchaseArgs);
        

        if (!PurchaseArgs.IsValid)
        {
            Debug.Log("Not enough resources to upgrade");
            return false;
        }
        
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

    // public List<Func<bool>> GetCallableMethods()

    // {

    //     return new List<Func<bool>>

    //     {

    //         StartUpgrade

    //     };

    // }
}
