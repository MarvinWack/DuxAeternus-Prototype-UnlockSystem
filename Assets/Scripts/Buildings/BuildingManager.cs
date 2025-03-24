using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingManager : BaseObject
{
    [InspectorButton("CreateBuildingDebug")]
    public bool CreateBuildingButton;
    
    [Space(20)]
    
    [SerializeField] private ObjectBluePrint DebugObjectType;
    
    [SerializeField] private ObjectBuilder objectBuilder;
    
    [SerializeField] List<Building> Buildings = new();
    
    protected void CreateBuildingDebug()
    {
        var building = objectBuilder.CreateObject(this);
        building.OnLevelUp += HandleLevelUp;
        Buildings.Add(building);
    }
    
    protected void HandleLevelUp(int level)
    { 
        Debug.Log($"Building level up to {level}");
        RaiseOnRequirementValueUpdatedEvent(level);
    }

    protected override void HandleTick()
    {
        
    }
}
