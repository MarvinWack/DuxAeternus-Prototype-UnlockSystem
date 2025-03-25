using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;using UnityEngine;
using UnityEngine.Serialization;

public class ObjectBuilder : MonoBehaviour
{
    [SerializeField] private Building buildingPrefab;
    
    public Building CreateObject(BaseObject baseObject)
    {
        return baseObject.ObjectBluePrint.ObjectType switch
        {
            ObjectType.Building => CreateBuilding(baseObject),
            _ => throw new Exception("Invalid Object Type")
        };
    }

    private Building CreateBuilding(BaseObject baseObject)
    {
        var building = Instantiate(buildingPrefab);
 
        return building;
    }
}
