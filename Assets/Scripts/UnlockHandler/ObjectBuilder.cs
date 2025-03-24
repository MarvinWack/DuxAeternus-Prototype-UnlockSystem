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

    [SerializeField] private SerializedDictionary<ObjectBluePrint, Building> objects = new();
    
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
        if (!objects.TryGetValue(baseObject.ObjectBluePrint, out var objectPrefab))
        {
            throw new KeyNotFoundException($"No prefab found for object type: {baseObject}");
        }

        var building = Instantiate(objectPrefab);
        if (building == null)
        {
            throw new InvalidCastException($"Prefab for {baseObject} cannot be cast to Building type");
        }
        
        OnRequirementCreated?.Invoke(baseObject);
        OnUnlockableCreated?.Invoke(baseObject.GetEventHandler(), baseObject.GetRequirements());
        
        return building;
    }
}
