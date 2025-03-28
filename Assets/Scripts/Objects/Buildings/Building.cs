using System;
using Production.Items;
using Production.Storage;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class Building : MonoBehaviour
{
    [InspectorButton("UpgradeDebug")]
    public bool LevelUpButton;
    
    public Action<int> OnUpgrade;
    public Action<ResourceType, int> OnProduction;

    public int Level => _level;

    public int _level;

    private BuildingBlueprint _blueprint;

    private bool _isUpgrading;
    private ushort _elapsedUpgradingTime;
    
    private bool _isProducing;
    
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
        Assert.IsTrue(Level < _blueprint.MaxLevel);
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
        
        OnProduction?.Invoke(_blueprint.ResourceBlueprint.ResourceType, _level);
        
        Debug.Log("Producing " + _blueprint.ResourceBlueprint.ResourceType );
    }
}
