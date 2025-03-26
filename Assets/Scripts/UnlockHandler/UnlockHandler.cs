using System;
using System.Collections.Generic;
using AreRequirementsFulfilled = System.Collections.Generic.List<System.Tuple<
    IUnlockable.RequirementFulfilledHandler, 
    System.Collections.Generic.Dictionary<ObjectBluePrint, (int requiredLevel, bool isFulfilled)>>>;

/// <summary>
/// Stores the requirements of IRequirements created by ResearchTree. Checks if all
/// requirements are met when a value is updated.
/// Invokes RequirementFulfilledHandler on IUnlockables if all requirements are met.
/// </summary>
public class UnlockHandler
{
    private AreRequirementsFulfilled _requirementsFulfilled = new();

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

    private Dictionary<ObjectBluePrint, (int requiredLevel, bool isFulfilled)> CreateDictionaryFromUnlockRequirements(UnlockRequirements unlockRequirements)
    {
        var dictionary = new Dictionary<ObjectBluePrint, (int requiredLevel, bool isFulfilled)>();
        
        foreach (var requirement in unlockRequirements.RequiredLevels)
        {
           dictionary.Add(requirement.Key, (requirement.Value, false)); 
        }
        
        return dictionary;
    }

    private void HandleRequirementValueUpdated(ObjectBluePrint objectBluePrint, int value)
    {
        SetRequirementToTrueIfFulfilled(objectBluePrint, value);
    }

    private void SetRequirementToTrueIfFulfilled(ObjectBluePrint objectBluePrint, int value)
    {
        if (!GetUnlockRequirementsByType(objectBluePrint, out AreRequirementsFulfilled results)) return;
        
        foreach (var tuple in results)
        {
            if(value >= tuple.Item2[objectBluePrint].Item1)
            {
                tuple.Item2[objectBluePrint] = (value, true);
                
                if (CheckIfAllRequirementsAreFulfilled(tuple.Item2))
                    tuple.Item1?.Invoke();
            }
        }
    }

    private bool CheckIfAllRequirementsAreFulfilled(Dictionary<ObjectBluePrint, (int requiredLevel, bool isFulfilled)> requirements)
    {
        foreach (var requirement in requirements)
        {
            if(!requirement.Value.Item2) return false;
        }

        return true;
    }

    private bool GetUnlockRequirementsByType(ObjectBluePrint objectBluePrint, out AreRequirementsFulfilled result)
    {
        result = _requirementsFulfilled.FindAll(x => x.Item2.ContainsKey(objectBluePrint));
        
        return result.Count != 0;
    }
}
