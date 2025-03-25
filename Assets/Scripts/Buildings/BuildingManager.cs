using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class BuildingManager : BaseObject
{
    [InspectorButton("CreateBuildingDebug")]
    public bool CreateBuildingButton;
    
    [FormerlySerializedAs("objectBuilder")]
    [Space(20)]
    
    [SerializeField] private BuildingBuilderIguess buildingBuilderIguess;
    
    [SerializeField] List<Building> Buildings = new();
    
    private BuildingBlueprint BuildingBlueprint => _objectBluePrint as BuildingBlueprint;

    private void Awake()
    {
        buildingBuilderIguess = FindObjectOfType<BuildingBuilderIguess>();
    }

    protected void CreateBuildingDebug()
    {
        Assert.IsTrue(_isUnlocked);
        var building = buildingBuilderIguess.CreateObject(BuildingBlueprint);
        building.OnLevelUp += HandleLevelUp;
        Buildings.Add(building);
    }

    private void HandleLevelUp(int level)
    { 
        RaiseOnRequirementValueUpdatedEvent(level);
    }

    protected override void HandleTick()
    {
        
    }

    public override UnlockRequirements GetRequirements()
    {
        return BuildingBlueprint.UnlockRequirements;
    }
}
