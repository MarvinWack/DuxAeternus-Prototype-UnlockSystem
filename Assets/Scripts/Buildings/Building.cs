using System;
using UnityEngine;
using UnityEngine.Assertions;

public class Building : MonoBehaviour
{
    [InspectorButton("UpgradeDebug")]
    public bool LevelUpButton;

    [Space(20)] 
    
    public Action<int> OnUpgrade;

    public int Level => _level;

    public int _level; //todo: make private

    private BuildingBlueprint _blueprint;

    private bool _isUpgrading;
    private ushort _elapsedUpgradingTime;

    public void HandleTick()
    {
        if (!_isUpgrading) return;
            
        _elapsedUpgradingTime++;
        
        if(_elapsedUpgradingTime >= _blueprint.UpgradeTime)
            Upgrade();
    }
    
    public void SetBlueprint(BuildingBlueprint blueprint)
    {
        _blueprint = blueprint;
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
}
