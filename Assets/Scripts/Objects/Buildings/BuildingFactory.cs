using System;
using Production.Storage;
using UnityEngine;

public class BuildingFactory : MonoBehaviour
{
    [SerializeField] private Building buildingPrefab;
    // [SerializeField] private ResourceStorage resourceStorage;
    // [SerializeField] private ItemStorage itemStorage;
    [SerializeField] private StorageAssigner _storageAssigner;
    
    public Building CreateObject(ObjectBluePrint bluePrint)
    {
        return bluePrint.ObjectType switch
        {
            ObjectType.Building => CreateBuilding(bluePrint as BuildingBlueprint),
            _ => throw new Exception("Invalid Object Type")
        };
    }

    private Building CreateBuilding(BuildingBlueprint bluePrint)
    {
        var building = Instantiate(buildingPrefab);
        building.name = bluePrint.name;
        
        _storageAssigner.AssignBuildingToStorage(building, bluePrint);
        
        // building.OnProduction += resourceStorage.HandleProductionTick;
        // building.OnTryPurchase += resourceStorage.HandleTryToPurchase;
        
        return building;
    }
}
