using System;
using UnityEngine;

public class BuildingBuilderIguess : MonoBehaviour
{
    [SerializeField] private Building buildingPrefab;
    
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
        
        return building;
    }
}
