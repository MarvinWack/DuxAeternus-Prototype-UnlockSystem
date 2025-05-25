using System;
using System.Collections.Generic;
using Production.Items;
using Production.Storage;
using UI.MethodBlueprints;
using UnityEngine;
using UnityEngine.Serialization;

public class Building : MonoBehaviour, ICustomer, IUpgradeMethodProvider
{
    [InspectorButton("StartUpgrade")]
    public bool UpgradeButton;

    public Action<ProductBlueprint, int> OnProduction;
    
    [FormerlySerializedAs("MethodBlueprint")] public UpgradeMethod upgradeMethod;
    public event Action<Dictionary<ProductBlueprint, int>, PurchaseArgs> OnTryPurchase;
    public event Action<Dictionary<ProductBlueprint, int>, PurchaseArgs> CheckIfPurchaseValid;

    public event Action<int> OnUpgradeFinished;
    public event Action<float> OnUpgradeProgress;

    // [SerializeField] private MethodAssigner methodAssigner;
    
    public int Level => _level;
    public int _level;
    public bool IsUpgradeable => _level < _blueprint.MaxLevel;
    public bool _isProducing;
    public int _currentlyAccumulatedProduction;

    private BuildingBlueprint _blueprint;
    private bool _isUpgrading;
    private ushort _elapsedUpgradingTime;

    private Dictionary<Func<bool>, bool> _callableMethods = new();


    private void Awake()
    {
        upgradeMethod.RegisterMethodToCall(StartUpgrade, this);
        upgradeMethod.RegisterMethodEnableChecker(CheckIfUpgradePossible);
        
        // methodAssigner.HandleMethodProviderCreated(this);
    }
    
    public void HandleUpgradeTick()
    {
        if (!_isUpgrading) return;
            
        _elapsedUpgradingTime++;
        
        OnUpgradeProgress?.Invoke((float) _elapsedUpgradingTime / _blueprint.UpgradeTime);
        
        if(_elapsedUpgradingTime >= _blueprint.UpgradeTime)
            Upgrade();
    }

    public void HandleProductionTick()
    {
        if(!_isProducing) return;
        
        OnProduction?.Invoke(_blueprint.ProducedProduct, CalculateProductionAmount());
    }

    private void StartUpgrade()
    {
        _isUpgrading = CheckIfUpgradePossible() && TryPurchase();
    }

    private bool CheckIfUpgradePossible()
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
        
        CheckIfPurchaseValid?.Invoke(_blueprint.Cost.Amount[_level], PurchaseArgs);
        
        if (!PurchaseArgs.IsValid)
        {
            Debug.Log("Not enough resources to upgrade");
            return false;
        }
        
        return true;
    }

    private bool TryPurchase()
    {
        var PurchaseArgs = new PurchaseArgs();
        
        OnTryPurchase?.Invoke(_blueprint.Cost.Amount[_level], PurchaseArgs);
        
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
        OnUpgradeFinished?.Invoke(_level);
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
    
    public List<IMethod> GetMethods()
    {
        return new List<IMethod>{upgradeMethod};
    }

    public string GetName()
    {
        return _blueprint.name;
    }

    public bool DoesBelongToPlayer()
    {
        return true;
    }
}
