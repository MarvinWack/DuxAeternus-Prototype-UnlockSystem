using System;
using Production.Storage;
using UnityEngine;

public class BuildingFactory : MonoBehaviour
{
    [SerializeField] private Building buildingPrefab;
    [SerializeField] private Storage storage;
    
    public Building CreateObject(ObjectBluePrint bluePrint)
    {
        return bluePrint.ObjectType switch
        {
            ObjectType.Building => CreateBuilding(bluePrint),
            _ => throw new Exception("Invalid Object Type")
        };
    }

    private Building CreateBuilding(ObjectBluePrint bluePrint)
    {
        var building = Instantiate(buildingPrefab);
        building.name = bluePrint.name;
        
        building.OnProduction += storage.HandleProductionTick;
        
        return building;
    }
}
