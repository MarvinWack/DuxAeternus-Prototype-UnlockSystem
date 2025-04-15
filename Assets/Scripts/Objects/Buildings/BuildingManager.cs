using System.Collections.Generic;
using Core;
using UnityEngine;

public abstract class BuildingManager : BaseObject, IUpgradeTickReceiver, IProductionTickReceiver
{
    [SerializeField] protected List<Building> buildings = new();

    protected BuildingBlueprint BuildingBlueprint => _objectBluePrint as BuildingBlueprint;
    private BuildingFactory _buildingFactory;

    private void Start()
    {
        Setup();
    }

    protected virtual void Setup()
    {
        _buildingFactory = FindObjectOfType<BuildingFactory>();
    }

    //todo: params für ui-message wenn nicht buildable
    public abstract Building TryCreateBuilding();
    
    protected Building CreateBuilding()
    {
        var building = _buildingFactory.CreateObject(BuildingBlueprint);
        building.OnUpgrade += HandleUpgrade;
        buildings.Add(building);
        building.transform.SetParent(transform);
        return building;
    }

    protected virtual bool CheckIfBuildingIsBuildable()
    {
        if (!_isUnlocked || !_isAvailable)
        {
            Debug.Log("building not unlocked/available");
            return false;
        }
        
        return true;
    }

    private void HandleUpgrade(int level)
    { 
        RaiseOnRequirementValueUpdatedEvent(level);
    }
    
    public override UnlockRequirements GetRequirements()
    {
        if(BuildingBlueprint == null ) Debug.LogWarning("No building blueprint found");
        return BuildingBlueprint.UnlockRequirements;
    }

    public void UpgradeTickHandler()
    {
        foreach (var building in buildings)
        {
            building.HandleUpgradeTick();
        }
    }

    public void ProductionTickHandler()
    {
        foreach (var building in buildings)
        {
            building.HandleProductionTick();
        }
    }
}
