using System;
using System.Collections.Generic;

public class ObjectBuilder : IRequirementBuilder, IUnlockableBuilder
{
    public event IRequirementBuilder.RequirementCreated OnRequirementCreated;
    public event IUnlockableBuilder.UnlockableCreated OnUnlockableCreated;
    
    private Dictionary<ObjectType, UnlockRequirements> _unlockRequirements;
    private Dictionary<ObjectType, ObjectBluePrint> _objectBluePrints;
    
    public BaseObject CreateObject(ObjectType objectType) //, SpecificVariationOfObjectType variationName)
    {
        return objectType switch
        {
            ObjectType.Building => CreateBuilding(objectType),
            //ObjectType.Item => CreateItem(objectType, SpecificVariationOfObjectType variationName)
            //...
            _ => throw new Exception("Invalid Object Type")
        };
    }

    private Building CreateBuilding(ObjectType objectType) //, SpecificVariationOfObjectType variationName)
    {
        var building = new Building(_unlockRequirements[objectType], _objectBluePrints[objectType]);
        
        OnRequirementCreated?.Invoke(building);
        OnUnlockableCreated?.Invoke(building.GetEventHandler(), building.GetRequirements());
        
        return building;
    }
}
