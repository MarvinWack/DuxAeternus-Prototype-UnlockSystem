using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingManager : BaseObject
{
    [InspectorButton("CreateBuildingDebug")]
    public bool CreateBuildingButton;
    
    [Space(20)]
    
    [SerializeField] private ObjectBuilder objectBuilder;
    
    [SerializeField] List<Building> Buildings = new();

    private void Awake()
    {
        objectBuilder = FindObjectOfType<ObjectBuilder>();
    }

    protected void CreateBuildingDebug()
    {
        var building = objectBuilder.CreateObject(this);
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
