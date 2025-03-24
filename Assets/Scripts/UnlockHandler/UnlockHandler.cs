using System;
using System.Collections.Generic;

using AreRequirementsFulfilled = System.Collections.Generic.List<System.Tuple<
    IUnlockable.RequirementFulfilledHandler, 
    System.Collections.Generic.Dictionary<ObjectType, (int requiredLevel, bool isFulfilled)>>>;

public class UnlockHandler
{
    private AreRequirementsFulfilled _requirementsFulfilled;
    
    public UnlockHandler(ObjectBuilder objectBuilder)
    {
        objectBuilder.OnRequirementCreated += HandleRequirementCreated;
        objectBuilder.OnUnlockableCreated += HandleUnlockableCreated;
    }

    public void RegisterUnlockableBuilder(IUnlockableBuilder builder)
    {
        builder.OnUnlockableCreated += HandleUnlockableCreated;
    }
    
    public void RegisterRequirementBuilder(IRequirementBuilder builder)
    {
        builder.OnRequirementCreated += HandleRequirementCreated;
    }

    private void HandleRequirementCreated(IRequirement requirement)
    {
        requirement.OnRequirementValueUpdated += HandleRequirementValueUpdated;
    }

    private void HandleUnlockableCreated(IUnlockable.RequirementFulfilledHandler handler, UnlockRequirements unlockRequirements)
    {
        _requirementsFulfilled.Add(Tuple.Create(handler, CreateDictionaryFromUnlockRequirements(unlockRequirements)));
    }

    private Dictionary<ObjectType, (int requiredLevel, bool isFulfilled)> CreateDictionaryFromUnlockRequirements(UnlockRequirements unlockRequirements)
    {
        var dictionary = new Dictionary<ObjectType, (int requiredLevel, bool isFulfilled)>();
        
        foreach (var requirement in unlockRequirements.RequiredLevels)
        {
           dictionary.Add(requirement.Key.ObjectType, (requirement.Value, false)); 
        }
        
        return dictionary;
    }

    private void HandleRequirementValueUpdated(ObjectType objectType, int value)
    {
        switch (objectType)
        {
            case ObjectType.Building:
                CheckRequiredLevelsForUnlock(objectType, value);
                break;
        }
    }

    private void CheckRequiredLevelsForUnlock(ObjectType objectType, int value)
    {
        if (!GetUnlockRequirementsByType(objectType, out AreRequirementsFulfilled results)) return;
        
        foreach (var tuple in results)
        {
            if(value >= tuple.Item2[objectType].Item1)
            {
                tuple.Item2[objectType] = (value, true);
                
                if (CheckIfAllRequirementsAreFulfilled(tuple.Item2))
                    tuple.Item1?.Invoke();
            }
        }
    }

    private bool CheckIfAllRequirementsAreFulfilled(Dictionary<ObjectType, (int requiredLevel, bool isFulfilled)> requirements)
    {
        foreach (var requirement in requirements)
        {
            if(!requirement.Value.Item2) return false;
        }

        return true;
    }

    private bool GetUnlockRequirementsByType(ObjectType objectType, out AreRequirementsFulfilled result)
    {
        result = _requirementsFulfilled.FindAll(x => x.Item2.ContainsKey(objectType));
        
        return result.Count != 0;
    }
}
