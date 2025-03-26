using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class BuildingManager : BaseObject
{
    [InspectorButton("CreateBuilding")]
    public bool CreateBuildingButton;
    
    [FormerlySerializedAs("objectBuilder")]
    [Space(20)]
    
    [SerializeField] private BuildingBuilderIguess buildingBuilderIguess;
    
    [SerializeField] private List<Building> buildings = new();
    
    private BuildingBlueprint Blueprint => _objectBluePrint as BuildingBlueprint;

    public override void SetData(ObjectBluePrint blueprint)
    {
        base.SetData(blueprint);
    }

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        buildingBuilderIguess = FindObjectOfType<BuildingBuilderIguess>();
        
        if(Blueprint.Type == BuildingType.Core)
            CreateBuilding();
    }

    private void CreateBuilding()
    {
        if(!CheckIfBuildingIsBuildable())
            return;
        
        Assert.IsTrue(_isUnlocked);
        var building = buildingBuilderIguess.CreateObject(Blueprint);
        building.transform.SetParent(transform);
        building.SetBlueprint(Blueprint);
        building.OnUpgrade += HandleUpgrade;
        buildings.Add(building);
    }

    private bool CheckIfBuildingIsBuildable()
    {
        if (!_isUnlocked)
        {
            Debug.Log("building not unlocked");
            return false;
        }
        
        if(Blueprint.Type == BuildingType.Core && buildings.Count != 0)
        {
            Debug.Log("Can't build more than one core building ");
            return false;
        }
        
        if(Blueprint.Type == BuildingType.Large && buildings.Count != 0)
        {
            Debug.Log("Can't build more than one large building");
            return false;
        }

        return true;
    }

    private void HandleUpgrade(int level)
    { 
        RaiseOnRequirementValueUpdatedEvent(level);
    }

    protected override void HandleTick()
    {
        foreach (var building in buildings)
        {
            building.HandleTick();
        }
    }

    public override UnlockRequirements GetRequirements()
    {
        return Blueprint.UnlockRequirements;
    }
}
