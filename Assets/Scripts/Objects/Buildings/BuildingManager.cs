using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public abstract class BuildingManager : BaseObject
{
    [SerializeField] protected List<Building> buildings = new();

    protected BuildingBlueprint BuildingBlueprint => _objectBluePrint as BuildingBlueprint;
    private BuildingBuilderIguess buildingBuilderIguess;

    private void Start()
    {
        Setup();
    }

    protected virtual void Setup()
    {
        buildingBuilderIguess = FindObjectOfType<BuildingBuilderIguess>();
    }

    protected Building CreateBuilding()
    {
        // if(!CheckIfBuildingIsBuildable())
        //     return;
        
        // Assert.IsTrue(_isUnlocked);
        var building = buildingBuilderIguess.CreateObject(BuildingBlueprint);
        building.OnUpgrade += HandleUpgrade;
        buildings.Add(building);
        building.transform.SetParent(transform);
        return building;
    }

    protected virtual bool CheckIfBuildingIsBuildable()
    {
        if (!_isUnlocked)
        {
            Debug.Log("building not unlocked");
            return false;
        }
        
        // if(BuildingBlueprint.Type == BuildingType.Large && buildings.Count != 0)
        // {
        //     Debug.Log("Can't build more than one large building");
        //     return false;
        // }

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
        return BuildingBlueprint.UnlockRequirements;
    }
}
