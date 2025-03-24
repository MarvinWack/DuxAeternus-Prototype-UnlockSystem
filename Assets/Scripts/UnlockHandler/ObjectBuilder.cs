using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;using UnityEngine;

public class ObjectBuilder : MonoBehaviour, IRequirementBuilder, IUnlockableBuilder
{
    public event IRequirementBuilder.RequirementCreated OnRequirementCreated;
    public event IUnlockableBuilder.UnlockableCreated OnUnlockableCreated;

    [SerializeField] private SerializedDictionary<ObjectType, BaseObject> objects = new();
    
    public BaseObject CreateObject(ObjectType objectType) //, (SpecificVariationOfObjectType variationName)
    {
        return objectType switch
        {
            ObjectType.Building => CreateBuilding(objectType),
            //ObjectType.Item => CreateItem(objectType, SpecificVariationOfObjectType variationName)
            //...
            _ => throw new Exception("Invalid Object Type")
        };
    }

    private Building CreateBuilding(ObjectType objectType) //, (SpecificVariationOfObjectType variationName)
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
}
