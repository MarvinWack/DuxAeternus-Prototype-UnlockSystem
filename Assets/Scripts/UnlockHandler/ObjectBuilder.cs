using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;using UnityEngine;
using Object = System.Object;

public class ObjectBuilder : MonoBehaviour, IRequirementBuilder, IUnlockableBuilder
{
    [InspectorButton("CreateBuildingDebug")]
    public bool CreateBuildingButton;
    
    [Space(20)]
    
    [SerializeField] private ObjectBluePrint DebugObjectType;
    
    public event IRequirementBuilder.RequirementCreated OnRequirementCreated;
    public event IUnlockableBuilder.UnlockableCreated OnUnlockableCreated;

    [SerializeField] private SerializedDictionary<ObjectBluePrint, BaseObject> objects = new();
    
    public BaseObject CreateObject(ObjectBluePrint objectType)
    {
        return objectType.ObjectType switch
        {
            ObjectType.Building => CreateBuilding(objectType),
            _ => throw new Exception("Invalid Object Type")
        };
    }

    private Building CreateBuilding(ObjectBluePrint objectType)
    {
        if (!objects.TryGetValue(objectType, out var objectPrefab))
        {
            throw new KeyNotFoundException($"No prefab found for object type: {objectType}");
        }

        var building = Instantiate(objectPrefab) as Building;
        if (building == null)
        {
            throw new InvalidCastException($"Prefab for {objectType} cannot be cast to Building type");
        }
        
        OnRequirementCreated?.Invoke(building);
        OnUnlockableCreated?.Invoke(building.GetEventHandler(), building.GetRequirements());
        
        return building;
    }
    
    private void CreateBuildingDebug()
    {
        CreateObject(DebugObjectType);
    }
}
