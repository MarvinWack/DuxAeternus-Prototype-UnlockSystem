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

    private void Awake()
    {
        buildingBuilderIguess = FindObjectOfType<BuildingBuilderIguess>();
    }

    protected void CreateBuildingDebug()
    {
        Assert.IsTrue(_isUnlocked);
        var building = buildingBuilderIguess.CreateObject(_objectBluePrint);
        building.OnLevelUp += HandleLevelUp;
        Buildings.Add(building);
    }

    private void HandleLevelUp(int level)
    { 
        Debug.Log($"Building is now level {level}");
        RaiseOnRequirementValueUpdatedEvent(level);
    }

    protected override void HandleTick()
    {
        
    }
}
